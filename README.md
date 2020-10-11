# Readme

This repo contains problems/solutions from across the web (leetcode, geeks4geeks etc)

## Adding new post

- Go to _posts directory, choose a directory and follow existing format/naming convention to add new post. 
- If problem can belong to multiple folders, choose most appropriate folder (say dp provides optimal solution, choose dp even if problem can be solved using different techniques). However, your choice here really does not matter for jekyll, see next. This directory structure is just to partition into meaningful sub-folders and can be useful in future.
- Take care to set frontmatter variables - layout, title, preview, date, categories, lcid. lcid can be empty if it's not leetcode problem. `categories` is important because it is the one that determines actually category on website. This can be either space separate list or yaml list

## Adding new category

- Add .md file in _category folder. That's it!
- We're not using any plugin for category. Refer to [this article](https://kylewbanks.com/blog/creating-category-pages-in-jekyll-without-plugins) for details

## Regenerating FrontMatter or renaming files

- Refer to csutils\Program.cs on how to regenerate FrontMatter or rename files or generate search index

## Creating Similar Site

- Run "jekyll new ." in new directory
- Run jekyll build and verify basic site working.
- Add _layout etc to new site as appropriate
- Backup of minima layout is available in root dir `minima-2.5.1.7z`

## General Jekyll tips:

1. Capitalize filter is used to upper case category title. 
   - https://jekyllrb.com/docs/liquid/filters/
2. You can comment using
   - {% comment %}
   - {% endcomment %}
3. Navigational lists can be built in many ways. Refer [doc](https://jekyllrb.com/tutorials/navigation/#scenario-5-using-a-page-variable-to-select-the-yaml-list)
4. You can inspect variables when jekyll compiler runs using [Octopress debugger](https://github.com/octopress/debugger). You need to install gem if not already done. Put `{%debug%}` after the line where variable is assigned value and then use `scopes` command to see its value. You can use `continue` to step through the loop. Refer to official doc page.
5. You can add contact form using [this](https://www.andrewmunsell.com/blog/formingo-free-html-form-processing/) . Putting email is probably the easiest way to let people reach you, so this is low pri.
6. You can do block comments using `ctrl + /`
7. Url structure is controlled using` permalink` in `_config.yml` Refer [this article](https://www.digitalocean.com/community/tutorials/how-to-control-urls-and-links-in-jekyll)
8. Convert existing site to jekyll using [this](https://jekyllrb.com/tutorials/convert-site-to-jekyll/)