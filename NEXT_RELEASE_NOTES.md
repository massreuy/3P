
**Improvements :**

- New options for the deployment :
  - You can now send files to a .cab file (works like zip rules)
  - Changed the behavior of the `display parser errors on save`, the notification now allows you to disable the feature for the current file only
  - Extended the possibilities of the deployer :
    - CopyFolder
	- DeleteFolder

**Fixed issues :**

- #160, #161 : problems with prototype synchronization in include files
- #162 : no fields available on the autocompletion for buffers
- Problem with progress v9x : issue with the use of too many streams in the 3P progress program, v9 limits the max stream to 5 -> corrected to only use 1 stream...
- Fields display all the flags
- Change the behavior of the prototype update, it now occurs AFTER the file is saved, which is safer in case of a problem