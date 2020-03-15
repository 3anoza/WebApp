namespace Gallery.DAL
{
    public class ConstantProvider
    {
        public byte InfoSize { get;} = 8;
        public string DefaultPathToImage { get; } = "~/Assets/Image";
        public string DefaultImageTypes { get; } = "image/jpeg;image/png";
    }
}