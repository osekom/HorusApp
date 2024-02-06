using System;
using System.IO;
using HorusApp.Droid.Dependencies;
using HorusApp.Helpers;
using HorusApp.Settings;
using Xamarin.Forms;

[assembly: Dependency(typeof(PathService))]
namespace HorusApp.Droid.Dependencies
{
    public class PathService : IPathService
    {
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, AppConfiguration.Values.NameDB);
        }
    }
}
