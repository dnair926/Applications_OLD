using Applications.Core.Services;
using Microsoft.AspNetCore.Hosting.Internal;
using System;
using Xunit;

namespace Applications.Core.Tests.Services
{
    public class FileServiceTests
    {
        private IFileService SUT
        {
            get
            {
                return new FileService(new HostingEnvironment()
                {
                    ContentRootPath = @"c:\projects\applications\applications.core.tests",
                });
            }
        }
        [Fact]
        public void File_Service_Should_Get_File_Contents()
        {
            var contents = SUT.GetFileContent("ServicesTests/contentfile.txt");
            Assert.Equal("This is test", contents);

            contents = SUT.GetFileContent("");
            Assert.Empty(contents);

            contents = SUT.GetFileContent("ServicesTests/contentfile2.txt");
            Assert.Empty(contents);
        }
    }
}
