using Applications.Core;

namespace Applications.Core.Repository
{
    public interface ISqlMapperConfiguration
    {
        CommandDefinition CommandDefinition(CrudAction crudAction);
    }
}
