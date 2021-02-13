![.NET Core](https://github.com/stize/infrastructure/workflows/.NET%20Core/badge.svg)

# Stize Infrastructure as Code

The aim of this project is provide automated infrastructure provisioning for Stize , simplifying the platform setup and speeding up development and deploying times by packing up reusable, modular Infrastructure configurations.

# Contributing

If you are an internal contributor, please use Gitflow to work on features. You will need to submit a pull request in order to push your changes to main.

If you are a third-party, please fork the repository and submit your pull request with the required changes.

# Using Stize.Infrastructure

Create a new pulumi azure-csharp project as usual:

```bash
mkdir new-pulumi-stize
cd new-pulumi-stize
pulumi new azure-csharp
This command will walk you through creating a new Pulumi project.

Enter a value or leave blank to accept the (default), and press <ENTER>.
Press ^C at any time to quit.

project name: (new-pulumi-stize) 
project description: (A minimal Azure C# Pulumi program) 
Created project 'new-pulumi-stize'

...

Saved config
...

Your new project is ready to go! ✨

To perform an initial deployment, run 'pulumi up'
```

Then, add the Stize package repository to your local nuget configuration, if is still not present in your local system (you can check it with `dotnet package source list`):

```bash    
dotnet nuget add source https://nuget.pkg.github.com/stize/index.json --name stize --username ${GITHUB_USER} --password ${GITHUB_PAT} --store-password-in-clear-text
```

Learn [here](https://docs.github.com/en/github/authenticating-to-github/creating-a-personal-access-toke) how to create your Github Personal Access Token (PAT). 

> This PAT only needs `read:packages` permission.

And add the Stize.Infrastructure.Azure package to your project:

```bash
dotnet add new-pulumi-stize.csproj package Stize.Infrastructure.Azure --prerelease
```

Use Stize.Infrastructure in your Stack:

```csharp
using Pulumi;
using Pulumi.Azure.Storage;
using Stize.Infrastructure.Azure;

class MyStack : Stack
{
    public MyStack()
    {        
        // Create an Azure Resource Group using Stize.Infrastructure
        var resourceGroup = new ResourceGroupBuilder("rg1")
            .Name("rg1")
            .Location("uksouth")
            .Build();

        // Create an Azure Storage Account using usual Pulumi 
        var storageAccount = new Account("storage", new AccountArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AccountReplicationType = "LRS",
            AccountTier = "Standard"
        });

        // Export the connection string for the storage account
        this.ConnectionString = storageAccount.PrimaryConnectionString;
    }

    [Output]
    public Output<string> ConnectionString { get; set; }
}
```

And run Pulumi:

```bash
pulumi preview
Enter your passphrase to unlock config/secrets
    (set PULUMI_CONFIG_PASSPHRASE or PULUMI_CONFIG_PASSPHRASE_FILE to remember): 
Previewing update (pulumi-dev):
     Type                                             Name                         Plan       
 +   pulumi:pulumi:Stack                              new-pulumi-stize-pulumi-dev  create     
 +   ├─ azure-nextgen:resources/latest:ResourceGroup  rg1                          create     
 +   └─ azure:storage:Account                         storage                      create     
 
Resources:
    + 3 to create
```

## Important things to remember

Pulumi uses `az cli`. You have to have it installed (Windows: `choco install azure-cli` Mac: `brew install azure-cli`), logged into Azure (`az login`) and with a valid subscription setted (`az account list` and `az account set -s ${subcription_id}`)

# Development basics

## Basic commands

These are the basic commands when working with Stize.Infrastructure and Visual Studio Code:

> Remember to `cd ./src/Infrastructure` before executing any command

| Command      | Description                             |
| ------------ | --------------------------------------- |
| dotnet build | Builds the debug version of the project |
| dotnet test  | Execute the unit tests                  |

When using VSCode, don't forget to add the C# extension to get autocomplete features. It is also possible to use Visual Studio Community/Professional/Enterprise.