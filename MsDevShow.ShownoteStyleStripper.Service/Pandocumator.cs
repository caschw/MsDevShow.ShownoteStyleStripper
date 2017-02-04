using System.Diagnostics;
using System.IO;

namespace MsDevShow.ShownoteStyleStripper.Service
{
    public static class Pandocumator
    {
        private static string pandocInstallLocation = @"C:\Program Files (x86)\Pandoc\pandoc.exe";
        public static void ExecuteDocumentTransformation(string folderPath, string inputFilename, string outputFilename)
        {
            if (!File.Exists(pandocInstallLocation))
            {
                return;
            }
            var arguments = $"-i \"{Path.Combine(folderPath, inputFilename)}\" -o \"{Path.Combine(folderPath, outputFilename)}\"";
            var p = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    FileName = pandocInstallLocation,
                    Arguments = arguments
                }
            };
            p.Start();
        }
    }
}
