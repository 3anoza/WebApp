namespace Gallery.DAL.ProjectProperties
{
    public class Properties
    {
        public byte ExifArraySize { get; private set; } = 8;
        public string DefaultPathToImage { get; private set; } = "~/Resources/Images";
        public string DefaultImageTypes { get; private set; } = "image/jpeg;image/png";
    }
}