using Applications.Core.Attributes;
using Applications.Core.Business.Models;
using System.Collections.Generic;

namespace Applications.Core.Business.Models
{
    public class FormInformation<T>
    {
        public string Name { get; set; }

        public T Model { get; set; }

        public string Title { get; set; }

        public FormOrientation Orientation { get; set; } = FormOrientation.Vertical;

        public IEnumerable<Field> Fields { get; set; }

        public bool Hidden { get; set; }
    }
}