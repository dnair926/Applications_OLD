using Applications.Core;
using System.Collections.Generic;
using System.Data;

namespace Applications.Core.Repository
{
    public interface IDataMapper
    {
        IEnumerable<T> MapDataToEntities<T>(IDataReader dataReader) where T : IDataEntity, new();
    }
}