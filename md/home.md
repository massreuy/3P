# Home page #

*Last update of this page : 07/08/2016*

***


## Content of this page ##

+ [About this project](#about-this-project)
+ [Key features](#key-features)
+ [A final word](#a-final-word)


***


## About this project ##

The **OpenEdge** **A**dvanced **B**usiness **L**anguage, or [OpenEdge ABL](https://www.progress.com/openedge) (formerly known as **Progress 4GL**) is a fourth-generation programming language which uses an English-like syntax. Applications developed with this language are portable across computing systems, it uses its own integrated relational database and programming tool which is called the "appbuilder".

Progress Programmers Pal (3P), is a **[notepad++](https://notepad-plus-plus.org/ "Notepad++ home page")** plug-in designed to help writing **ABL code**,  its use is governed by [GPL License](http://www.gnu.org/copyleft/gpl.html).

3P transforms notepad++ into an ABL code editor, providing :

* a powerful auto-completion
* tool-tips on every words
* a code explorer to quickly navigate through your code
* a file explorer to easily access all your sources
* more than 50 options to better suit your needs
* the ability to run/compile and even **[PROLINT](http://www.oehive.org/book/export/html/223)** your source file with an in-line visualization of errors
* and so much more!

If you are not fond of the appbuilder and looking for a fast and efficient **ABL editor**, look no further!


***


## Key features  ##

This paragraph briefly presents the main features of 3P (the list is not exhaustive).


### Overview ###

The screenshot below gives you a quick overview of 3P's features :

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/home_overview.png)


### Auto-completion ###

A key feature of 3P is the built in auto-completion window. Fully integrated with notepad++, it smooths the developer work by suggesting the best match according to his intention :

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/419a86a4-8d48-11e5-9155-c062659551dd.png)

*[Learn more about this feature here](#/autocompletion)*


### Tool-Tips ###

Tool-tips are another important part of 3P, they provide information on a word. You can activate them by simply hovering a word with your cursor :

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/3b6b3e8e-8d54-11e5-8162-297dcb0f4c5c.png)

*[Learn more about this feature here](#/tooltips)*


### Code explorer ###

The code explorer is your best friend when it comes to... exploring the code.

> *thanks captain!*

It acts as an improved `function list`, well known to the notepad++ users. It displays the structure of the program and provides a quick way to jump from a code's portion to another (left click an item to get redirected to it) :

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/home_page_codeexplorer.png)

*[Learn more about this feature here](#/code_explorer)*


### File explorer ###

You can easily browse the files of your local directory and/or propath with the file explorer. A powerful search tool has been added to make sure you can quickly find the file you are looking for :

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/home_page_fileexplorer.png)

*[Learn more about this feature here](#/file_explorer)*


### Check syntax and visualize errors ###

With 3P, you can visualize the **compilation errors** (or PROLINT errors like in the screenshot below) directly into notepad++ :

![image](content_images/2016-08-09_193841.png)


### Adaptability ###

3P has tons of options to help you in your work and to better suit your project needs. Beside all the *classic* options that could be expected from a code editor, 3P also has *4GL progress* oriented options ; like the ability to define several environments, to choose the way prowin is launched, the way database are connected and so on... Below is one of the option page of the application :

![image](content_images/2016-08-09_194022.png)

*[Learn how to set an environment here](#/set_environment)*


### Lighting fast compilation ###

Do you have a large application with a lot of programs that you often need to recompile? If so, you will love the `MASS COMPILER` feature of 3P. It uses a multi-threaded compilation that makes it **A LOT** faster than the built-in application compiler (how much faster depends on your computer but you can bet on a 2 digit number). You can compile an entire directory recursively (with filter options!) in one click and get a nice and interactive report to quickly fix the errors.

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/home_page_compiler.png)

*[Learn more about the mass compiler here](#/mass_compiler)*


### Syntax highlighting ###

3P has an embedded syntax highlighter, several themes are available and switchable at will :

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/a84d092e-8d45-11e5-87c6-830d40460e14.png)


### And more!? ###

See what 3P can bring you by *[installing](#/installation)* it! It only takes a few seconds ;)

You can also browse this website through the navigation menu on your right to check out more features of 3P.

*[Learn more here](#/learn_more)*

***


## A final word ##

One thing the AppBuilder has that 3P doesn't, is the [graphical interface](https://documentation.progress.com/output/ua/OpenEdge_latest/index.html#page/gsstu/overview-of-the-openedge-appbuilder.html) to modify your .w files. You can do it by code, obviously, but i admit that it's not a simple task.

Personally, i use 3P as a **complement** to the AppBuilder, designing interfaces with the graphical tool and switching to notepad++ to modify the core behavior.

Also, at the moment (29/02/2016), 3P does not parse .cls files. But don't worry, this feature will come in a (near) future release.
