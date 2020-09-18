# book-depo

This project is for a website to manage a home library.

It showcases the following technologies:

* ASP.NET Core 3.1
* SPA using the Vue 2 framework
* Vue Composition API
* Typescript
* Kubernetes via K3d

## Development Requirements

* Visual Studio 2019
* [NodeJs](https://nodejs.org/en/download/)
* Linux to run the dev stack. [Configuration Details](./dev/README.md)

## Running the Application in Visual Studio

1) First run the dev stack to bring the application service up. See [Readme](./dev/README.md)
2) Open a command prompt in directory './src/wwwMain/clientapp'
3) Run: npm install
4) Open the solution file in Visual Studio './src/book-depo.sln'
5) Alter './src/wwwMain/appsetting.json' to point the required services to your running dev stack
