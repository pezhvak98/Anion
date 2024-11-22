// Developed By Pezhvak ;)

using System;
using System.Diagnostics;
using System.IO;

class Program
{
	static void Main(string[] args)
	{
		if (args.Length == 0 || args[0] != "--Generate")
		{
			Console.WriteLine("Usage: Anion --Generate");
			return;
		}

		// Get the current directory (project root)
		string projectPath = Directory.GetCurrentDirectory();

		// Get the project name from the directory name
		string projectName = new DirectoryInfo(projectPath).Name;

		// Find the solution file in the root directory
		string solutionFile = Directory.GetFiles(projectPath, "*.sln").FirstOrDefault();
		if (string.IsNullOrEmpty(solutionFile))
		{
			Console.WriteLine("Error: No solution file found in the current directory.");
			return;
		}

		// Create Class Library projects at the same level as the Web project
		CreateClassLibraries(projectPath, projectName, solutionFile);

		// Add project references
		AddReferences(projectPath, projectName);

		// Inform the user to reload the solution
		Console.ForegroundColor = ConsoleColor.Green; // Set text color to green
		Console.WriteLine("Onion architecture Class Libraries created and added to the solution successfully.");
		Console.WriteLine("IMPORTANT: Please click 'Reload' in Visual Studio when prompted to apply changes.");
		Console.ResetColor(); // Reset text color to default
	}

	static void CreateClassLibraries(string projectPath, string projectName, string solutionFile)
	{
		// Define the class library layers
		string[] layers = {
			$"{projectName}.Common",
			$"{projectName}.Data",
			$"{projectName}.Entities",
			$"{projectName}.IocConfig",
			$"{projectName}.Services",
			$"{projectName}.ViewModels"
		};

		foreach (var layerName in layers)
		{
			// Create each class library at the same level as the main project
			string layerPath = Path.Combine(new DirectoryInfo(projectPath).Parent.FullName, layerName);
			Directory.CreateDirectory(layerPath);

			var startInfo = new ProcessStartInfo
			{
				FileName = "dotnet",
				Arguments = $"new classlib -n {layerName} -o \"{layerPath}\"",
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};

			using (var process = Process.Start(startInfo))
			{
				process.WaitForExit();
				Console.WriteLine($"Created: {layerPath}");
			}

			// Add the created class library to the solution
			AddToSolution(solutionFile, layerPath);
		}
	}

	static void AddToSolution(string solutionFile, string projectPath)
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "dotnet",
			Arguments = $"sln \"{solutionFile}\" add \"{projectPath}\\{new DirectoryInfo(projectPath).Name}.csproj\"",
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		using (var process = Process.Start(startInfo))
		{
			process.WaitForExit();
			Console.WriteLine($"Added to solution: {projectPath}");
		}
	}

	static void AddReferences(string projectPath, string projectName)
	{
		// Define paths for the projects
		string commonPath = Path.Combine(new DirectoryInfo(projectPath).Parent.FullName, $"{projectName}.Common");
		string dataPath = Path.Combine(new DirectoryInfo(projectPath).Parent.FullName, $"{projectName}.Data");
		string entitiesPath = Path.Combine(new DirectoryInfo(projectPath).Parent.FullName, $"{projectName}.Entities");
		string iocConfigPath = Path.Combine(new DirectoryInfo(projectPath).Parent.FullName, $"{projectName}.IocConfig");
		string servicesPath = Path.Combine(new DirectoryInfo(projectPath).Parent.FullName, $"{projectName}.Services");
		string viewModelsPath = Path.Combine(new DirectoryInfo(projectPath).Parent.FullName, $"{projectName}.ViewModels");
		string webPath = projectPath;

		// Add references based on the required dependencies
		RunDotnetCommand($"add \"{webPath}\\{new DirectoryInfo(webPath).Name}.csproj\" reference \"{commonPath}\\{new DirectoryInfo(commonPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{webPath}\\{new DirectoryInfo(webPath).Name}.csproj\" reference \"{dataPath}\\{new DirectoryInfo(dataPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{webPath}\\{new DirectoryInfo(webPath).Name}.csproj\" reference \"{iocConfigPath}\\{new DirectoryInfo(iocConfigPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{dataPath}\\{new DirectoryInfo(dataPath).Name}.csproj\" reference \"{entitiesPath}\\{new DirectoryInfo(entitiesPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{dataPath}\\{new DirectoryInfo(dataPath).Name}.csproj\" reference \"{viewModelsPath}\\{new DirectoryInfo(viewModelsPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{iocConfigPath}\\{new DirectoryInfo(iocConfigPath).Name}.csproj\" reference \"{dataPath}\\{new DirectoryInfo(dataPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{iocConfigPath}\\{new DirectoryInfo(iocConfigPath).Name}.csproj\" reference \"{servicesPath}\\{new DirectoryInfo(servicesPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{servicesPath}\\{new DirectoryInfo(servicesPath).Name}.csproj\" reference \"{commonPath}\\{new DirectoryInfo(commonPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{servicesPath}\\{new DirectoryInfo(servicesPath).Name}.csproj\" reference \"{dataPath}\\{new DirectoryInfo(dataPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{viewModelsPath}\\{new DirectoryInfo(viewModelsPath).Name}.csproj\" reference \"{commonPath}\\{new DirectoryInfo(commonPath).Name}.csproj\"");
		RunDotnetCommand($"add \"{viewModelsPath}\\{new DirectoryInfo(viewModelsPath).Name}.csproj\" reference \"{entitiesPath}\\{new DirectoryInfo(entitiesPath).Name}.csproj\"");
	}

	static void RunDotnetCommand(string arguments)
	{
		var startInfo = new ProcessStartInfo
		{
			FileName = "dotnet",
			Arguments = arguments,
			RedirectStandardOutput = true,
			UseShellExecute = false,
			CreateNoWindow = true
		};

		using (var process = Process.Start(startInfo))
		{
			process.WaitForExit();
			Console.WriteLine($"Executed: dotnet {arguments}");
		}
	}
}
