using Applications.Core;
using System;
using System.Data.SqlClient;

namespace Applications.Core.Repository
{
    public interface ISqlCommandBuilder
    {
        void BuildSqlCommand<T>(SqlCommand command, Func<T, bool> criteria, CrudAction crudAction);
    }
}