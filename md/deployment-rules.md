# Deployment rules #

*Last update : 09/08/2016*

## Pre-requisite ##

To understand the poinf of the **deployment** rules, you must first read the page on the [the deployment here](#/deployment).

## When are those rules applied? ##

The **deployment rules** are applied each time you compile a progress file and during a deployment. 

*Yes, the rules are applied when you compile a single file with `CTRL+F1`!*

## How to define a rule ##

Go to the `SET` > `DEPLOYMENT` interface and follow the instructions. At the moment, a text file holds the rules (one rule per line), a user-friendly interface might come later.

### Two types of rule ### 

Two type of rules exist :

- Transfer rules : they define when / where / how a file should be deployed
- Filter rules : they define which files are eligible to the deployment

### Composition of a filter rule ### 

Each transfer rule as 5 components :

- The deployment step : integer (a rule is always defined for a particular step)
- The application name filter : If the application name of your current environment matches this filter (you can use wildcards), the rule can apply 
- The application suffix filter : If the application suffix of your current environment matches this filter (you can use wildcards), the rule can apply 
- Rule type : `+` / `` (or `Include` / `Exclude`) decide if the files matching the *source path pattern* below are included or excluded from the deployment
- The source path pattern : when deploying, if a file matches this pattern (you can use wildcards), the rule can apply

### Composition of a transfer rule ### 

Each transfer rule as 7 components :

- The deployment step : integer (a rule is always defined for a particular step)
- The application name filter : If the application name of your current environment matches this filter (you can use wildcards), the rule can apply 
- The application suffix filter : If the application suffix of your current environment matches this filter (you can use wildcards), the rule can apply 
- The deployment type : `Move` / `Copy` / `Prolib` (the file will be added to a progress library .pl) / `Ftp` (the file will be sent to an ftp server) / `Ftp`
- Execute further rules : `yes` / `no` : yes if more rules can be applied after this one, no to stop at this rule
- The source path pattern : when deploying, if a file matches this pattern (you can use wildcards), the rule can apply
- The deployment target : It can either be an absolute path or a relative one; If relative, it will be relative to the deployment base directory set for your current environment


### Rules of the rules ###

The following rules are applied during a deployment, work around them to get exactly what you need :

**Rules sorting (from most important to less important) :**

- exact application name first
- longer application name filter first
- exact application suffix first
- longer application suffix filter first
- rules with *execute further rules* = `yes` first
- `Prolib` before `Zip` before `Ftp` before `Copy` before `Move`
- rules defined first, first (line number in the file)

**Other rules :**

- A file can have several rules applied to it; however, the first `Move` rule encountered will be the last rule applied
- If no rules can be applied to a file, then the file will be `Moved` to the deployment base directory by default
- For the `Prolib` type, you can set the relative folder inside the .pl in which to move a file : `mylib.pl\subfolder1\sub2\`, the files will be added to the Pro-library with the given relative path inside it (`subfolder1\sub2\`)
- Same rule for `Zip`
- For step 0, if the environment is set to `compile next to source` then the *.r will be moved next to the source and no transfer rules will apply