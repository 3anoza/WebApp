namespace Gallery.BLL.Interfaces
{
    public interface IHashService
    {
        string CompareSha256Hashes(string data);
    }
}