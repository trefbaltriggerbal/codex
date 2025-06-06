# AGENT Instructions

To build and test the .NET projects in this repository:
1. Install the .NET 8 SDK. On Ubuntu, run:
   ```bash
   wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
   sudo dpkg -i packages-microsoft-prod.deb
   sudo apt-get update
   sudo apt-get install -y dotnet-sdk-8.0
   ```
2. Build the solution:
   ```bash
   dotnet build HelloWorldApp.sln
   ```
3. Run the solution (interactive CLI):
   ```bash
   dotnet run --project LinearCongruentGenerator.CLI
   ```
4. Run tests after building:
   ```bash
   dotnet test HelloWorldApp.sln --verbosity normal
   ```
Only attempt running the application or tests if the build step succeeds.
