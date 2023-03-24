// NuGet Package : System.Management --version 7.0.0

using System;
using System.Management;
using System.Runtime.Versioning;

namespace RemoteRun
{
    [SupportedOSPlatform("windows")]
    class Program
    {
        static void Main(string[] args)
        {
            var UiPathProcessName = "UiPathProcessNameWithoutEnvironmentName";
            var hostName = "hostName";
            var domainName = "domainName";
            var username = "username";
            var password = "password";

            // Define the connection options to connect to the remote server
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate; // Use the identity of the caller
            options.EnablePrivileges = true; // Enable all privileges for the connection
            options.Username = username; // Specify the username for the connection
            options.Password = password; // Specify the password for the connection
            options.Authority = "ntlmdomain:" + domainName; // Specify the domain name for NTLM authentication 

            // Define the management scope to access WMI objects on the remote server
            var managementPath = $"\\\\{hostName}\\root\\cimv2"; // Specify the namespace path for WMI classes
            ManagementScope scope = new ManagementScope(managementPath, options); // Create a management scope object with the specified path and options

            // Connect to the remote server using the management scope object
            scope.Connect();    

            var command =
                @$"C:\Program Files\UiPath\Studio\UiRobot.exe execute --process {UiPathProcessName}"; // Define the command to execute on the remote server using UiPath Robot
            ManagementClass processClass = new ManagementClass(
                scope,
                new ManagementPath("Win32_Process"),
                null
            ); // Create a management class object for Win32_Process class in the specified scope
            ManagementBaseObject inParams = processClass.GetMethodParameters("Create"); // Get an input parameter object for Create method of Win32_Process class
            inParams["CommandLine"] = command; // Set the CommandLine property of the input parameter object to the command string
            ManagementBaseObject outParams = processClass.InvokeMethod("Create", inParams, null); // Invoke the Create method of Win32_Process class using the input and output parameter objects

            // Check the return value of the Create method invocation
            uint returnValue = (uint)outParams["ReturnValue"]; // Get the ReturnValue property of the output parameter object as an unsigned integer
            if (returnValue == 0) // If ReturnValue is zero, then success
                Console.WriteLine("Job started successfully."); // Write a success message to console output
            else // If ReturnValue is non-zero, then failure
                Console.WriteLine("Job failed to start. Error code: " + returnValue); // Write a failure message with error code to console output
        }
    }
}
