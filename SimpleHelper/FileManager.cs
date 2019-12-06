using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleHelper
{
    class FileManager
    {
        private List<Dictionary<string, string>> userSettings = new List<Dictionary<string, string>>();

        public List<Dictionary<string, string>> UserSettings {
            get {
                return userSettings;
            }
        }

        public FileManager() {
            ReadInfo();
        }

        public void ReadInfo()
        {
            using (StreamReader streamReader = new StreamReader("settings.txt"))
            {
                string line = "";

                while ((line = streamReader.ReadLine()) != null)
                {
                    string[] keywords = line.Split(new char[] { ';' });

                    if (keywords.Length < 2) return;

                    string path;

                    if (keywords.Length < 3 || keywords[2] == " ") path = "C:/";
                    else path = keywords[2];

                    userSettings.Add(new Dictionary<string, string>() { { "hotkey", keywords[0] }, { "name", keywords[1] }, { "path", keywords[2] } });
                }
            }

        }
    }
}
