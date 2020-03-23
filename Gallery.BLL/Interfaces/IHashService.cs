namespace Gallery.BLL.Interfaces
{
    public interface IHashService
    {
        bool CompareHashes(string imageHash);
    }
}