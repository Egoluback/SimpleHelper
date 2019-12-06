using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace SimpleHelper
{
    class SearchFile
    {
        private List<Dictionary<string, string>> filesInfo;

        public List<Dictionary<string, string>> FilesInfo
        {
            get
            {
                return filesInfo;
            }
        }

        public SearchFile(List<Dictionary<string, string>> _filesInfo)
        {
            filesInfo = _filesInfo;

            foreach (var file in filesInfo)
            {
                List<string> files = GetFile(file["path"], file["name"], "");
                if (files.Count > 0)
                    file.Add("url", files[0]);
            }
        }
        public List<string> GetFile(string path, string pattern, string localPath)
        {
            var files = new List<string>();

            if (localPath != "")
            {
                try
                {
                    files.AddRange(Directory.GetFiles(localPath, pattern, SearchOption.TopDirectoryOnly));
                    foreach (var directory in Directory.GetDirectories(localPath))
                        files.AddRange(GetFile(null, pattern, directory));
                }
                catch (Exception) { }

                return files;
            }
            else
            {
                try
                {
                    files.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                    foreach (var directory in Directory.GetDirectories(path))
                        files.AddRange(GetFile(null, pattern, directory));
                }
                catch (Exception) { }
                if (files.Count > 0)
                {
                    return files;
                }
            }
            return files;
        }
    }
}
