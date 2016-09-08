using System;
using System.Diagnostics;
using System.IO;
using MsDevShow.ShownoteStyleStripper.Common;

namespace MsDevShow.ShownoteStyleStripper.Service
{
    public class WatchAndRip : IDisposable
    {
        private readonly EventLog _log;
        private static string FolderPath = @"C:\MS Dev Show\shownotes";
        private const string Filename = "input.htm";
        private readonly FileSystemWatcher _watcher;

        public WatchAndRip()
        {
            _watcher = new FileSystemWatcher
            {
                Path = FolderPath,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite | NotifyFilters.Size,
                Filter = Filename
            };

            _watcher.Changed += Watcher_Changed;
            _watcher.Created += Watcher_Changed;
            _watcher.Renamed += Watcher_Renamed;
            
            _watcher.EnableRaisingEvents = true;
        }

        public WatchAndRip(EventLog log): this()
        {
            _log = log;
        }

        private void Watcher_Renamed(object sender, RenamedEventArgs e)
        {
            ExecuteWithoutTriggeringChange(() =>
            {
                var t = File.ReadAllText(e.FullPath);
                File.WriteAllText(e.FullPath, t.RemoveCruft());
                _watcher.EnableRaisingEvents = true;
                _log.WriteEntry($"Updated {e.ChangeType} File: {e.Name}\n{DateTime.Now}");
            });
        }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            ExecuteWithoutTriggeringChange(()=>
            {
                var t = File.ReadAllText(e.FullPath);
                File.WriteAllText(e.FullPath, t.RemoveCruft());
                _log.WriteEntry($"Updated {e.ChangeType} File: {e.Name}\n{DateTime.Now}");
            });
        }

        private void ExecuteWithoutTriggeringChange(Action action)
        {
            _watcher.EnableRaisingEvents = false;
            action();
            _watcher.EnableRaisingEvents = true;
        }

        public void Dispose()
        {
            _watcher.Dispose();
        }
    }
}
