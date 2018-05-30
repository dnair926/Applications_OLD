using Applications.Core;

namespace Applications.Core.Repository
{
    public class ODataEntity
    {
        public string Name { get; set; }

        public ODataDefinition Select { get; set; }

        public ODataDefinition Crud { get; set; }

        public ODataDefinition Delete { get; set; }

        public ODataDefinition Insert { get; set; }

        public ODataDefinition Update { get; set; }

        public ODataDefinition CommandName(CrudAction crudAction)
        {
            ODataDefinition commandDefinition = null;
            if (crudAction == CrudAction.Select)
            {
                commandDefinition = Select;
            }

            if (crudAction == CrudAction.Delete)
            {
                commandDefinition = Delete;
            }

            if (crudAction == CrudAction.Insert)
            {
                commandDefinition = Insert;
            }

            if (crudAction == CrudAction.Update)
            {
                commandDefinition = Update;
            }

            if (commandDefinition != null)
            {
                return commandDefinition;
            }

            if (Crud != null)
            {
                commandDefinition = Crud;
            }

            return commandDefinition;
        }
    }

    public class ODataDefinition
    {
        public string Name { get; set; }

        public string ConnectionStringName { get; set; } = "Application";
    }
}