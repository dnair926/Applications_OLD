using Applications.Core.Business.DataEntities;
using Applications.Core.Common;
using Applications.Core.Models;
using Applications.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Applications.Core.Business.Services
{
    public class DataAuditService : IDataAuditService
    {
        private readonly IRepository<DataAudit> repository;
        private readonly ICurrentUserService currentUserService;

        public DataAuditService(
            IRepository<DataAudit> repository,
            ICurrentUserService currentUserService)
        {
            this.repository = repository;
            this.currentUserService = currentUserService;
        }

        public void TrackDelete<T>(T entity) where T : IDataEntity, new()
        {
            var properties = entity.GetValidValues()?.Select(p => new PropertyValueTracker() { PropertyName = p.Key, ValueOld = p.Value })?.ToList();
            SaveAudit(properties, CrudAction.Delete);
        }

        public void TrackUpdate<T>(T entity) where T : IDataEntity, new()
        {
            var changedProperties = entity.ChangedProperties;
            if ((changedProperties?.Count() ?? 0) == 0)
            {
                return;
            }

            SaveAudit(changedProperties, entity.IsPersisted ? CrudAction.Update : CrudAction.Insert);
        }

        private void SaveAudit(IEnumerable<PropertyValueTracker> entity, CrudAction changeType)
        {
            var audit = new DataAudit()
            {
                ChangedBy = currentUserService.GetCurrentUserInfo()?.NetworkID,
                ChangedDate = DateTime.Now,
                ChangeType = (int)changeType,
                EntityName = entity.GetType().FullName,
                NewInfo = entity.ToXMLString()
            };

            repository.Save(audit);
        }
    }
}