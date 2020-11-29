using System;
using System.IO;

namespace SalesApp.LocalData
{
    public class FileNames
    {
        public static string TOKEN_FILE_PATH = Path.Combine(
               Environment.GetFolderPath(
                   Environment.SpecialFolder.LocalApplicationData),
               "token.txt");
    }
}
