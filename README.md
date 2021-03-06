## Introduction
Procx is a Cross-Platform library that simplifiy working with a proccess, cmd and terminal. 

## Getting Started

From Nuget

`Install-Package Procx -Version 1.0.0`

To excute a command line from your C# App

```csharp
using (var client = new TerminalClient())
{
    string workingDir = @"C:\";
    string fileName = "cmd.exe";
    string cmd = "/c dir";

    string result = await client.ExcuteAndReadOutputAsync(workingDir, fileName, cmd);
    
    Console.WriteLine(result);
}
```
### Redirect Proccess Output
To redirect the proccess output

```csharp
using (var client = new TerminalClient())
{
    client.OnOutput += OnOutput;
    
    await client.ExcuteAsync(@"C:\","ipconfig",null);
}

private void OnOutput(object sender,string e)
{
    Console.WriteLine(e);
}
```
### Trace Proccess

```csharp
public class TraceWriter : ITraceWriter
{
    public void Info(string output)
    {
        Console.WriteLine(output);
    }
}

private TraceWriter _trace = new TraceWriter();

using (var client = new TerminalClient(_trace))
{
    await client.ExcuteAsync(@"C:\","cmd.exe","/c dir");
}

```

## Acknowledgements

This project was built from great work done by

* [Azure piplines agent](https://github.com/microsoft/azure-pipelines-agent)

