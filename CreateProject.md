# Creating a New Project

## 1 Create Web Project

1) Open a command prompt in the 'src' directory
2) run: dotnet new react --name wwwMain --output wwwMain
3) Open './src/wwwMain/wwwMain.csproj' in Visual Studio
4) Press 'save' and save hte solution file 'book-depo.sln' in the './src' directory

## 2 Create Vue SPA Project

1) Delete the directory ./src/wwwMain/ClientApp
2) Open up a commandline in the directory ./src/wwwMain
3) Install the [Vue cli](https://cli.vuejs.org/guide/installation.html)
4) Run 'vue ui'
5) Navigate to the 'Project Manager'
6) Click on the 'Create' button
7) Navigate to the ./src/wwwMain directory
8) Click 'Create new project here'
9) Type project name 'clientapp'
10) Untick git repo
11) Click 'Next'
12) Click 'Manual' then 'Create Project'
13) Add 'Typescript', 'Use config files' then click 'Next'
14) Use Vue 2.x, ESLint + AirBnb config
15) Create and wait for project to be created
16) Go into the project configuration and set the output directory to be '../wwwroot' as well as Disable Production Source Maps
17) Then click 'Save changes'

## 3 Configure the Web project to work with Vue

1) Open the NuGet manager for the project
2) Browse for 'VueCliMiddleware' and install it. See [here](https://github.com/EEParker/aspnetcore-vueclimiddleware) for configuration details
3) create a 'wwwroot' folder
4) Alter startup.cs with the following:
```
line 23: configuration.RootPath = "wwwroot"
insert at line 49:
				endpoints.MapToVueCliProxy(
					"{*path}",
					new SpaOptions { SourcePath = "clientapp" },
					npmScript: (System.Diagnostics.Debugger.IsAttached) ? "serve" : null,
					regex: "Compiled successfully",
					forceKill: true
				);
```
5) To prevent SSL issues with the npm proxy debuggin must be down without SSL. Edit launchSettings.json and remove the https part of the 'applicationUrl' entry
6) Modify clientapp/vue.config.js by adding the following to the module.exports:
```
  devServer: {
    host: '127.0.0.1'
  }
```
7) Run the application and you should see the default vue project page come up
