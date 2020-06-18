
using System;

namespace Gallery.ConfigManagment
{
    public class ConfigurationManager
    {
        public string FileExtensions { get; private set; } = "FileExtensions";
        private string DefaultFileExtensions { get; set; } = "image/jpeg;image/png";

        public string IsFileExtensionsExist()
        {
            var appSettings = System.Configuration.ConfigurationManager.AppSettings;
            var types = appSettings[FileExtensions];
            
            if (string.IsNullOrEmpty(types))
            {
                types = DefaultFileExtensions;
            }

            return types;
        }
        
        public static string SqlDbConnectionString()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SQLCONN"] ??
                                   throw new ArgumentException("SQLCONN");
            return connectionString.ConnectionString;
        }
    }
}