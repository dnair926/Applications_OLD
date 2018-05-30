using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Applications.PersonnelTracker.Infrastructure
{
    public class JsonSerializerFormatter : IOutputFormatter
    {
        public bool CanWriteResult(OutputFormatterCanWriteContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task WriteAsync(OutputFormatterWriteContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
