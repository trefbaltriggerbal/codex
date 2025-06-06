# codex
This repository contains a simple Linear Congruential Generator (LCG) sample
written in C#. The `LinearCongruentGenerator.CLI` project exposes a small command line
interface.

Running the app without arguments starts an interactive CLI.

By default the ANSI C parameters are used: multiplier `1103515245`, addition
`12345` and modulus `2^31`. The next sections detail how to install the
required SDK, build and run the application, and execute the accompanying test
suite. A list of basic CLI commands is included under **Running the CLI**.


## Installing the .NET 8 SDK

To build and run the sample you need the .NET 8 SDK. On Ubuntu the SDK can be
installed with:

```bash
wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-8.0
```

## Building the solution

Use `dotnet` to build the solution file:

```bash
dotnet build HelloWorldApp.sln
```

## Running the CLI

Once built you can start the interactive command line with:

```bash
dotnet run --project LinearCongruentGenerator.CLI
```

### Basic commands

```
short            # generate one random value
fast <n>         # jump ahead n steps using the fast algorithm
set m|a|c <val>  # set multiplier, addition or modulus
get m|a|c|seed   # display current parameters
seed <val>       # set the seed value
exit             # quit the CLI
```

If a supplied parameter is invalid when using the `set` command, the CLI
displays a warning in yellow. The previous valid value is shown so you can
manually restore it with another `set` command. Until a valid value is
provided, the generator is not rebuilt and its output may no longer be
reliable.

## Running tests

After a successful build, execute the test suite with:

```bash
dotnet test HelloWorldApp.sln --verbosity normal
```

## Publishing a single Windows executable

To generate a single Windows `.exe` without additional files, run:

```bash
dotnet publish LinearCongruentGenerator.CLI -c Release -r win-x64 \
  --self-contained true \
  -p:PublishSingleFile=true \
  -p:PublishTrimmed=true \
  -p:PublishAot=true
```

The executable is written to `LinearCongruentGenerator.CLI/bin/Release/net8.0/win-x64/publish/`.

Using `PublishTrimmed` removes unused framework code while `PublishAot` compiles
the project ahead-of-time to a native image. These options considerably reduce
the final file size at the cost of longer build times.


## Creating Releases

1. Update the version in `LinearCongruentGenerator/LinearCongruentGenerator.csproj`.
2. Commit the change and create a git tag:

```bash
git tag -a v1.0.0 -m "Release v1.0.0"
git push origin v1.0.0
```

3. Draft a new release on GitHub using the pushed tag.


## Packaging for NuGet

Use `dotnet pack` to create a NuGet package from the library project:

```bash
dotnet pack LinearCongruentGenerator/LinearCongruentGenerator.csproj --configuration Release
```

The generated `.nupkg` is found in `LinearCongruentGenerator/bin/Release`.

Running `dotnet pack` does **not** publish the package. You must push the
`.nupkg` file to a registry to make it available.

### Publishing to NuGet.org

Publish the package to the public NuGet feed using an API key from nuget.org:

```bash
dotnet nuget push LinearCongruentGenerator/bin/Release/*.nupkg \
  --source https://api.nuget.org/v3/index.json --api-key <NUGET_API_KEY>
```

Once uploaded you can reference it in other projects:

```bash
dotnet add package LinearCongruentGenerator --version 1.0.0
```

Replace the version number with the published version.

### Publishing to GitHub Packages

Authenticate to GitHub Packages and push the package with the `dotnet` CLI:

```bash
dotnet nuget add source --username <USER> --password <TOKEN> \
  --store-password-in-clear-text --name github \
  "https://nuget.pkg.github.com/<OWNER>/index.json"
dotnet nuget push LinearCongruentGenerator/bin/Release/*.nupkg \
  --source github --api-key <TOKEN>
```

Replace `<USER>`, `<TOKEN>` and `<OWNER>` with your account details. In GitHub
Actions you can use `${{ secrets.GITHUB_TOKEN }}` instead of a personal access
token.


## Deployment with GitHub Actions

An example workflow is provided in `.github/workflows/deploy.yml`. It builds the solution and publishes the CLI whenever a tag starting with `v` is pushed.
