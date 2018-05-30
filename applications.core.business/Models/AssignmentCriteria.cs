using Applications.Core.Attributes;

namespace Applications.Core.Business.Models
{
    public class AssignmentCriteria : BaseModel
    {
        [FormField(fieldType: FormFieldType.TextBox, Caption = "Person", DisplayOrder = 10)]
        public int PersonID { get; set; }
    }
}
