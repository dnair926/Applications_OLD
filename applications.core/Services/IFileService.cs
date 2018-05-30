namespace Applications.Core.Services
{
    /// <summary>
    /// File Service interface
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Get contents of file
        /// </summary>
        /// <param name="path">Path of file to read</param>
        /// <returns>File contents as string</returns>
        string GetFileContent(string path);
    }
}