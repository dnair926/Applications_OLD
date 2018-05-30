using Applications.Core;

namespace Applications.Core.Repository
{
    public interface IODataQueryBuilder
    {
        string GetServiceUrl(object criteria, CrudAction crudAction);
    }
}