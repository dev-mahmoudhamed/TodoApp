# TodoApp

A simple todo app built with ABP Framework.

## Setup Instructions

1. Clone the repository
   ```bash
   git clone https://github.com/dev-mahmoudhamed/TodoApp
   cd TodoApp
   ```

2. Set TodoApp.HttpApi.Host as startup project

   * Open TodoApp.sln in Visual Studio
   * Right-click on TodoApp.HttpApi.Host project in Solution Explorer
   * Select "Set as Startup Project"
   * In Visual Studio, select the TodoApp.HttpApi.Host instead of IIS Express in the run button drop-down list
   * Start the application (F5 or click Start button)

3. Setup and run Angular frontend
   ```bash
   cd angular
   abp generate-proxy -t ng
   ng serve
   ```

Access the application at `http://localhost:4200`
