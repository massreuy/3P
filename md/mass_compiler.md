# Mass compiler #

*Last update of this page : 15/06/2016*


## About this feature ##

Do you have a large application with a lot of programs that you often need to recompile? If so, you will love the `MASS COMPILER` feature of 3P. It uses a multi-threaded compilation that makes it **A LOT** faster than the built-in application compiler (how much faster depends on your computer but you can bet on a 2 digit number). You can compile an entire directory recursively (with filter options!) in one click and get a nice and interactive report to quickly fix the errors.

![image](https://raw.githubusercontent.com/jcaillon/3P/gh-pages/content_images/home_page_compiler.png)


## How to use it ##

You can access the `MASS COMPILER` page through the main windows (`ALT+SPACE` by default) then `ACTIONS` > `MASS COMPILER`.

1. Set your source directory that contains the program to compile (by default, it will be the directory you entered in the [set environment page](#/set_environment) as your project local directory)

2. Set the options of the compilation (don't forget to use the tooltips to understand each option!)

3. Click `Start the compilation`

4. Done!


## Notes about the compilation ##

In order to be **that** fast, 3P starts several processes of the Progress application (Prowin.exe / Prowin32.exe). It starts *X* processes for each core on your computer (an option that you can set in the page).

This implies that if you are using a connected database to compile your programs, the database must be able to accept as many connections as the number of processes started!

It also implies that if you are connected to said database in single-user mono, 3P will start the `MASS COMPILATION` in a single process and you will lose all the benefits of this feature!

*Hint : by default, a database is started to handle 20 users, you can increase this number by adding the following options to your `proserve` command : `-n 80` where 80 is the max number of users*