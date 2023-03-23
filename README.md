# Run UiPath Job Remotely
A C# program that remotely executes a UiPath process on a Windows machine using WMI.

## Requirements
- Windows OS with WMI enabled
- UiPath Studio or Robot installed on the remote machine
- System.Management 7.0.0 NuGet package

## Usage
- Edit the variables in the Program.cs file to match your UiPath process name, host name, domain name, username and password.
- Build and run the program using `dotnet run` or your preferred IDE.
- The program will connect to the remote machine using WMI and invoke the UiPath Robot to execute the specified process.
- The program will print the result of the execution to the console.

## Limitations
- The program only works on Windows OS with WMI enabled.
- The program only supports NTLM authentication for WMI connection.
- The program assumes that UiPath Robot is installed in the default location on the remote machine.

## What is WMI?
- [About WMI](https://learn.microsoft.com/en-us/windows/win32/wmisdk/about-wmi)

## License

[MIT](https://github.com/seymenbahtiyar/Run_UiPath_Job_Remotely/blob/main/LICENSE)
