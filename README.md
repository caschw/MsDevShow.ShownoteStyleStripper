### MsDevShow.ShownoteStyleStripper

This tool is optimized for removing extraneous styles and HTML elements that persist after conversting shownotes from HTML into Markdown. Inline stlyes and spans just get in the way.

The Core logic is three regex files located in the Common Project. A UWP interface exists for those just wanting to try it out and a Windows Service exists to let the conversion happen automatically.

### Installing the service

Once the Service Project is compiled in release mode, navigate to the bin folder in the [Visual Studio Command Prompt Shell](https://msdn.microsoft.com/en-us/library/ms229859%28v=vs.110%29.aspx). 

 - Install:

```
installutil.exe MsDevShow.ShownoteStyleStripper.Service.exe
```

 - Uninstall:

```
installutil.exe /u MsDevShow.ShownoteStyleStripper.Service.exe
```

### Why a Windows Service?

We're lazy. As a Windows service, we know that this application will run no matter how many times we restart the computer. As we record at *least* one episode a week, these regexs save several minutes in formatting of the shownotes. This isn't our only tool in the process. You should check out [Pandoc](http://pandoc.org/).

Anyways, the Service Project is also a great example of how to make a Service stay running if you have some code that looks like it executes and moves on. In this case, keeping our FileSystemWatcher alive.