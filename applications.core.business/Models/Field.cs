using Applications.Core.Attributes;
using System.Collections.Generic;

namespace Applications.Core.Business.Models
{
    public class Field
    {
        public string Caption { get; set; }

        public FormFieldType FieldType { get; set; }

        public int MaxCharLength { get; set; }

        public int DisplayOrder { get; set; }

        public string Name { get; set; }

        public bool Required { get; set; }

        //public IEnumerable<SelectListItem> Items { get; set; }

        public bool Disabled { get; set; }

        public string Accessor { get; set; }

        public string DescriptionAccessor { get; set; }

        public string HelpInfoAccessor { get; set; }

        public string ListItemsAccessor { get; set; }

        public bool Hidden { get; set; }
    }
}