namespace Gallery.BLL.Interfaces
{
    public interface IExifDataService
    {
        string[] GetExifDataStrings(string pathToImage);
    }
}