using System;
using System.IO;
using HorusApp.Helpers;
using HorusApp.iOS.Dependencies;
using HorusApp.Settings;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace HorusApp.iOS.Dependencies
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, AppConfiguration.Values.NameDB);
        }
    }
}
