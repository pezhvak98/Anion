
# Anion - Onion Architecture Class Library Auto Generator

A **C# Console Application** for generating Class Libraries based on the **Onion Architecture** and automatically adding them to your `.sln` file. This tool simplifies project setup and enforces a clean architectural structure. 
**Anion** is derived from the combination of the words **"Auto"** and **"Onion"** and refers to the automated construction of the onion architecture structure in Asp.NET Core.



## Table of Contents

- [About](#about)
- [Features](#features)
- [Prerequisites](#prerequisites)
- [Setup and Usage](#setup-and-usage)
- [Uninstall](#uninstall)
- [Layers](#layers)



## About

This project automates the creation of **Onion Architecture** layers for your .NET solution. It creates Class Library projects, adds references between them, and integrates them into your `.sln` file. The generated structure supports scalable, maintainable, and testable applications.



## Features

- Automatically generates the following layers:
  - `Common`
  - `Data`
  - `Entities`
  - `IoCConfig`
  - `Services`
  - `ViewModels`
- Adds project references between layers based on dependency rules.
- Seamlessly integrates all Class Libraries into the existing solution file.



## Prerequisites

Before using this tool, ensure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download) (minimum version 5.0 - Built it on 8.0)
	```bash
	#verify using 
	dontnet --version
	```
- Visual Studio or another IDE for .NET development
- A `.sln` file in your project's root directory


## Setup and Usage

### 1. Clone the Repository

```bash
git clone https://github.com/pezhvak98/Anion.git
cd Anion
```
### 2. Build the Project

Open the solution file in Visual Studio or build using the CLI:
```bash
dotnet build
```
### 3. Generate Release package
Use this command to release package to use it Globally in your system :
```bash
dotnet pack -c Release
```
### 4. Generate Release package
After the package is released, a folder named `bin` is created in the same path as the main package file in the `Release` folder. You should use that file to install the package:
```bash
dotnet tool install --global --add-source ./bin/Release Anion 
```
### 5. How to use
After building the project through Visual Studio, either using CMD outside of Visual Studio or the Package Manager Console in Visual Studio, by running the following command, all the built files and references are added to each.

> **Note**: When you run this command, a new window will open and give you a warning about reloading the solution. You must click the Reload button after the tool finishes. If you click it while the tool is running, Visual Studio will not be able to automatically recognize the new files and you will have to manually add each of the projects from the path **`Right click on Solution > Add > Existing Project > Select project`**.
![Reload Alert ](https://github.com/pezhvak98/Anion/blob/main/readme-assets/reload-alert.png)
``` bash
Anion --Generate
```
Example Output:
``` bash
Created: ../YourProject.Common
Created: ../YourProject.Data
...
Onion architecture Class Libraries created and added to the solution successfully.
IMPORTANT: Please click 'Reload' in Visual Studio when prompted to apply changes.

```
## Uninstall
If for any reason you decide to remove this tool, you can remove it globally by simply running the following command:
``` bash
dotnet tool uninstall --global Anion 
```
## Layers

The following Class Library layers will be created:

1.  **Common**  
    Shared components like constants, enums, and utility classes.
    
2.  **Data**  
    Data access layer for interacting with databases.
    
3.  **Entities**  
    Defines the domain entities.
    
4.  **IocConfig**  
    Handles Dependency Injection configurations.
    
5.  **Services**  
    Contains business logic services.
    
6.  **ViewModels**  
    Contains data models used for presentation.
---
Inspired by the principles of **Onion Architecture in .Net Core**.

