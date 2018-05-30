using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace Applications.Core.Services
{
    /// <summary>
    /// File service implementation
    /// </summary>
    public class FileService : IFileService
    {
        readonly IHostingEnvironment hostingEnvironment;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        public FileService(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// Read and return contents of the file at the specified location
        /// </summary>
        /// <param name="path">File to read</param>
        /// <returns>Contents of the file, if the file exists and can be accessed, else blank string</returns>
        public string GetFileContent(string path)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(path))
                {
                    return string.Empty;
                }

                path = $@"{hostingEnvironment.ContentRootPath}\{path}";
                if (!File.Exists(path))
                {
                    return string.Empty;
                }

                using (StreamReader fileStream = File.OpenText(path))
                {
                    return fileStream.ReadToEnd();
                }

            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}