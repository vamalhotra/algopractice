using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmptyProject
{
    class Program
    {
        public static void PutYAMLFrontMatter(string rootDir = "./posts")
        {
            //var dir = Path.Combine(rootDir, "posts");
            var dir = rootDir;
            var allMdFiles = GetAllMarkdownFiles(dir);

            //layout: post
            //title:  "Welcome to Jekyll!"
            //date: 2016 - 08 - 31 17:35:19 + 0000
            //categories: jekyll update
            var defaultFrontMatter = "---";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += "layout: post";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += "title: {0}";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += "preview: {1}";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += "date: {2}";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += "categories: {3}";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += "lcid: {4}";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += "---";
            defaultFrontMatter += Environment.NewLine;
            defaultFrontMatter += Environment.NewLine;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

            foreach (var f in allMdFiles)
            {
                var text = File.ReadAllText(f);
                if (text.TrimStart().StartsWith("---"))
                {
                    //Console.WriteLine("FM already set for " + f);
                    //continue;
                    //Remove existing front matter
                    var ix1 = text.IndexOf("---");
                    var ix2 = text.IndexOf("---", ix1 + 3);
                    text = text.Substring(ix2 + 3).TrimStart();
                }

                //Find first header
                int titleStart = 0, titleEnd = 0;
                while (titleStart < text.Length && text[titleStart] != '#') ++titleStart;
                ++titleStart; //jump over #

                //there's no #
                if (titleStart >= text.Length)
                {
                    titleStart = 0;
                }
                titleEnd = titleStart + 1;

                //point end to newline char
                while (titleEnd < text.Length && text[titleEnd] != '\n' && text[titleEnd] != '\r')
                {
                    ++titleEnd;
                }

                //Limit length of title
                if (titleEnd - titleStart > 200 || titleEnd >= text.Length)
                {
                    titleEnd = titleStart + Math.Min(200, text.Length - titleStart);
                }

                //title excluding newline char
                var title = text.Substring(titleStart, titleEnd - titleStart + 1).Trim();

                int numStart = 0, numEnd = 0;
                while (numStart < title.Length && !Char.IsNumber(title[numStart]))
                {
                    ++numStart;
                }
                if (numStart < title.Length)
                {
                    numEnd = numStart + 1;
                    while (numEnd < title.Length && Char.IsNumber(title[numEnd]))
                    {
                        ++numEnd;
                    }
                }

                var lcid = "";
                if(numEnd < title.Length && numEnd > numStart)
                {
                    lcid = title.Substring(numStart, numEnd - numStart);
                    title = title.Substring(numEnd+1).Trim();
                }

                //Curly braces are treated as variables in front matter
                title = textInfo.ToTitleCase(title).Replace("{", "").Replace("}", "");

                var date1 = File.GetCreationTime(f);
                var date2 = File.GetLastWriteTime(f);
                var minDate = date1 > date2 ? date2 : date1;
                var date = minDate.ToString("yyyy-MM-dd HH:mm:ss") + " +0000";
                var category = Path.GetDirectoryName(f).Split('/', '\\')
                    .Last().ToLower();

                //take preview as 140 chars following title
                var preview =
                    text.Substring(titleEnd + 1, Math.Min(140, text.Length - titleEnd - 1)).Trim();
                //remove any non-digit/letter to avoid jekyll build errors
                preview = 
                    string.Join("", preview.Replace(Environment.NewLine, " ")
                    .Where(x => Char.IsLetterOrDigit(x) || x == ' '));

                text = string.Format(defaultFrontMatter, title, preview, date, category, lcid) + text;

                File.WriteAllText(f, text);
                File.SetCreationTime(f, minDate);
                File.SetLastWriteTime(f, minDate);
            }
        }

        public static void RenameFiles(string rootDir = "./posts")
        {
            var dir = rootDir;
            var allMdFiles = GetAllMarkdownFiles(dir);

            var dateRegex = new Regex(@"[\d]{4}-[\d]{2}-[\d]{2}.*", RegexOptions.Compiled);

            foreach (var f in allMdFiles)
            {
                if (dateRegex.IsMatch(f)) continue;
                var dt = File.GetCreationTime(f);
                var path = Path.GetDirectoryName(f);
                var filename = Path.GetFileName(f);
                filename = filename.Replace(' ', '-');
                filename = $"{dt.ToString("yyyy-MM-dd")}-{filename}";

                var filePath = Path.Combine(path, filename);
                File.Move(f, filePath);
            }
        }

        private static IEnumerable<string> GetAllMarkdownFiles(string dir)
        {
            var allMdFiles =
                Directory.EnumerateFiles(dir, "*.md", SearchOption.AllDirectories)
                .Union(Directory.EnumerateFiles(dir, "*.markdown", SearchOption.AllDirectories));
            return allMdFiles;
        }

        private const string SearchIndexFilename = "-search-index.md";
        static void PrintIndex(Dictionary<string, Dictionary<string, int>> tokensIndex, string dir)
        {
            var today = DateTime.Now.AddDays(-1);
            var filepath = today.ToString("yyyy-MM-dd") + SearchIndexFilename;
            filepath = Path.Combine(dir, filepath);

            using (var sw = new StreamWriter(filepath))
            {
                var defaultFrontMatter = "---";
                defaultFrontMatter += Environment.NewLine;
                defaultFrontMatter += "layout: post";
                defaultFrontMatter += Environment.NewLine;
                defaultFrontMatter += "title: {0}";
                defaultFrontMatter += Environment.NewLine;
                defaultFrontMatter += "date: {1}";
                defaultFrontMatter += Environment.NewLine;
                defaultFrontMatter += "---";
                defaultFrontMatter += Environment.NewLine;

                var date = today.ToString("yyyy-MM-dd HH:mm:ss") + " +0000";
                var fmData = string.Format(defaultFrontMatter, "Search Index", date);
                sw.WriteLine(fmData);

                foreach (var kvp in tokensIndex.OrderBy(x => x.Key))
                {
                    var token = kvp.Key;
                    sw.WriteLine(token);
                    sw.WriteLine();
                    foreach (var f in kvp.Value.OrderByDescending(x => x.Value).Take(10))
                    {
                        //[Link Text] ({% post_url 2010-09-08-welcome-to-jekyll %})
                        sw.WriteLine(string.Format("- [{0}]({{% post_url {0} %}})", f.Key));
                    }
                    sw.WriteLine();
                }
            }
        }

        private static string Normalize(string s)
        {
            return s.ToLower();
        }

        static Dictionary<string, Dictionary<string, int>> GenerateIndex(string rootDir = "./posts")
        {
            var dir = rootDir;
            var allMdFiles = GetAllMarkdownFiles(dir);

            var allTokens = new Dictionary<string, Dictionary<string, int>>();
            foreach (var f in allMdFiles)
            {
                //referencing self causes build error
                if (f.Contains(SearchIndexFilename)) continue;

                var filename = Path.GetFileNameWithoutExtension(f);
                var text = File.ReadAllText(f);
                //var tokens = text.Split(new char[] { ' ', '.', '?', '-', ':',  }, StringSplitOptions.RemoveEmptyEntries);
                var tokens = Regex.Split(text, @"[^\w]").Select(x => Normalize(x));
                foreach (var token in tokens)
                {
                    if (token.Length < 2) continue;

                    Dictionary<string, int> tokenEntry;
                    if (!allTokens.TryGetValue(token, out tokenEntry))
                    {
                        tokenEntry = new Dictionary<string, int>();
                        allTokens[token] = tokenEntry;
                    }

                    if (!tokenEntry.ContainsKey(filename))
                    {
                        tokenEntry.Add(filename, 1);
                    }
                    else
                    {
                        tokenEntry[filename] += 1;
                    }
                }
            }
            return allTokens;
        }

        static void Main(string[] args)
        {
            var dir = @"C:\Users\*\source\repos\algopractice\_posts\";
            PutYAMLFrontMatter(dir);
            //RenameFiles(dir);
            //var index = GenerateIndex(dir);
            //PrintIndex(index, dir);
        }
    }
}
