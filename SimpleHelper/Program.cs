using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Keystroke.API;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SimpleHelper
{

    class App
    {
        private static List<Dictionary<string, string>> filesInfo;

        static void Run() {
            Stopwatch clock = new Stopwatch();

            bool isAltActive = false;

            using (var api = new KeystrokeAPI())
            {
                api.CreateKeyboardHook((character) => {
                    if (character.KeyCode.ToString().ToLower() == "lmenu")
                    {
                        clock.Reset();
                        isAltActive = true;
                        clock.Start();
                    } else if (isAltActive && character.KeyCode.ToString().ToLower() == "e") {
                        clock.Stop();
                        if (clock.ElapsedMilliseconds < 500)
                        {
                            System.Environment.Exit(0);
                        }
                    }
                    Console.WriteLine(character.KeyCode);
                    foreach (var file in filesInfo)
                    {
                        if (isAltActive && file["hotkey"] == character.KeyCode.ToString().ToLower())
                        {
                            clock.Stop();
                            Console.WriteLine(clock.Elapsed);
                            if (clock.ElapsedMilliseconds < 500)
                            {
                                Process proc = new Process();
                                proc.StartInfo.FileName = file["url"];
                                proc.Start();
                                isAltActive = false;
                                clock.Reset();
                            }
                        }
                    }
                });

                Application.Run();
            }
        }

        static void Main(string[] args)
        {

            FileManager fileManager = new FileManager();

            if (fileManager.UserSettings.Count > 0)
            {
                filesInfo = fileManager.UserSettings;
            }
            else {
                filesInfo = new List<Dictionary<string, string>> ();
                filesInfo.Add(new Dictionary<string, string>() { { "hotkey", "g" }, { "name", "chrome.exe" }, { "path", @"C:\Program Files (x86)" } });
                filesInfo.Add(new Dictionary<string, string>() { { "hotkey", "f" }, { "name", "firefox.exe" }, { "path", @"C:\Program Files" } });
                filesInfo.Add(new Dictionary<string, string>() { { "hotkey", "c" }, { "name", "Code.exe" }, { "path", @"C:\Users\Egoluback\AppData\Local\Programs" } });
            }

            SearchFile searchFile = new SearchFile(filesInfo);

            filesInfo = searchFile.FilesInfo;

            foreach (var file in searchFile.FilesInfo)
            {
                Console.WriteLine(file["url"] + " was found.");
            }

            Run();
            
        }
    }
}
