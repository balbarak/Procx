## Introduction
Procx is .NET Standard library that simplifiy working with a proccess, cmd and terminal. 

## Getting Started

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
