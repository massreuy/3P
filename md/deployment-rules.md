# Deployment rules #

*Last update : 06/08/2016*

**WORK IN PROGRESS, THIS IS NOT THE FINAL DOCUMENTATION**

## What is it? ##

They are used during a deployment, see [the deployment page](#/deployment) for more info.

## When are those rules applied? ##

Those **deployment rules** are applied each time you compile a progress file, whether it is a single file compilation `CTRL+F1` or through the `MASS COMPILER`.

For non progress files, you can trigger the deployment with the `ACTIONS` > `DEPLOY` interface.

## How to define a rule ##

Go to the `SET` > `DEPLOYMENT` interface and follow the instructions. At the moment, a text file holds the rules (one rule per line), a user-friendly interface might come later.

### Two types of rule ### 

- Transfer rules
- Filter rules

### Composition of a transfer rule ### 

Each transfer rule as 7 components :

- The deployment step
- The application name filter : If the application name of your current environment matches this filter (you can use wildcards), the rule can apply 
- The application suffix filter : If the application suffix of your current environment matches this filter (you can use wildcards), the rule can apply 
- The source path pattern : when deploying, if a file matches this pattern (you can use wildcards), the rule can apply
- The deployment type : Move, Copy, Prolib (the file will be added to a progress library .pl), Ftp (the file will be sent to an ftp server)
- Yes/no : yes if more rules can be applied after this one, no to stop at this rule
- The deployment target : It can either be an absolute path or a relative one; in the second case, it will be relative to the deployment base directory set for your current environment

### Rules of the rules ###

The following rules are applied during a deployment, work around them to get exactly what you need :

**Rules sorting (from most important to less important) :**

- Comparing two rules, if one has a more precise application name filter than the other, it will be applied first (a filter is more precise than another if it is longer)
- if one has a more precise application suffix filter than the other, it will be applied first
- A rule with a deployment type equals to `Move` is less prioritary than a rule with another type
- if one is defined before the other (line number in the configuration file), it will be applied first

**Other rules :**

- A file can have several rules applied to it; however, the first `Move` rule encountered will be the last rule applied
- If no rules can be applied to a file, then the file will be `Moved` to the deployment base directory by default
- For the `Prolib` type, you can set the relative folder inside the .pl in which to move a file : `mylib.pl\subfolder1\sub2\`, the files will be added to the Pro-library with the given relative path inside it (`subfolder1\sub2\`)

### Example of rules ###

My current environment is :

- Application name : `CoolApp`
- Application suffix : `v1`

The rules defined are :

```
*	*	*	Move	trash\
CoolApp	*	*others\*	Move	others\
CoolApp	v1	*src_program\*	Move	r-code\
CoolApp	v1	*src_program\*	Copy	C:\backup\r-code\
CoolApp	v1	*src_class\*	Prolib	prolib\mylib.pl
CoolApp	v1	*images\*	Prolib	prolib\imagelib.pl
CoolApp	v1	*images-sub\*	Prolib	prolib\imagelib.pl\subfolder\
C??l*	v1	*ideas\*	Move	ideas\
```

After sorting :

```
CoolApp	v1	*src_program\*	Copy	C:\backup\r-code\
CoolApp	v1	*src_class\*	Prolib	prolib\mylib.pl
CoolApp	v1	*images\*	Prolib	prolib\imagelib.pl
CoolApp	v1	*images-sub\*	Prolib	prolib\imagelib.pl\subfolder\
CoolApp	v1	*src_program\*	Move	r-code\
CoolApp	*	*others\*	Move	others\
C??l*	v1	*ideas\*	Move	ideas\
*	*	*	Move	trash\
```

*All the rules in this example matches my current environment so they will all apply.*

