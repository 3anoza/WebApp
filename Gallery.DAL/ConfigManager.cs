using System.Configuration;

namespace Gallery.DAL
{
    public class ConfigManager
    {
        public ConstantProvider Constants = new ConstantProvider();
        public void SetDefaultImageProperty()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = config.AppSettings.Settings;
            settings.Add("ImageDirectory", Constants.DefaultPathToImage);
            settings.Add("FileType",Constants.DefaultImageTypes);
        }

        public bool SetImageProperty()
        {
            return false;
        }

        public string[] GetImageProperty()
        {
            return null;
        }

    }
}