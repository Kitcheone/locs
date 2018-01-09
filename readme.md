[![Build status](https://ci.appveyor.com/api/projects/status/qny8scc8pcmslwvp?svg=true)](https://ci.appveyor.com/project/Kitcheone/locs)

# Lunch Ordering Collation System
Built in Angular and ASP.NET Core.

# Local development
## Pre-requisites
 * VS2017 and dot net core 2.0
 * SQLExpress
 * Node & npm

## Setup
To setup on your local machine:
 * Clone repo
 * Open backend solution in VS, and run the `Locs.Api.Database` project. Choose option 1, to build the database on your local machine.
 * Run the `Locs.Api.Web` solution. Confirm it's working by browsing to [http://localhost:62417/swagger](http://localhost:62417/swagger)
 * Open bash in the `\frontend` directory. Run:
  * `npm install`
  * `ng serve`

The site should now be accessible on [http://localhost:4200](http://localhost:4200).

# CI and CD
Handled by Cake and AppVeyor

# Roadmap
* Azure AD integration
