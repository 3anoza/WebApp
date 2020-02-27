using System;
using System.Configuration;
using App.Constants;

namespace App.Core
{
    public class DefaultConfig
    {
        public void setDefaultAppSettings()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;
            settings.Add(ConstantProvider.serverUploadDirectory, ConstantProvider.defaultDirectory);
            settings.Add(ConstantProvider.fileTypes,ConstantProvider.defaultTypes);
        }
        
        // if config not found or empty -> set default value
    }
}