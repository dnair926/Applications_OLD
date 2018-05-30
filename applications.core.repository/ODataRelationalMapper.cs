using Applications.Core;
using Applications.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Applications.Core.Repository
{
    public class ODataRelationalMapper<T> : IObjectRelationalMapper<T> where T : IBaseModel, new()
    {
        private readonly IODataQueryBuilder queryBuilder;

        public ODataRelationalMapper(IODataQueryBuilder queryBuilder)
        {
            this.queryBuilder = queryBuilder;
        }

        public T Add(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> LoadAll() => LoadByCriteria(new T());

        public IQueryable<T> LoadByCriteria<TCriteria>(TCriteria criteria) where TCriteria : IBaseModel => InvokeAPI(criteria as IODataCriteria);

        public int Remove(T entity)
        {
            throw new NotImplementedException();
        }

        public int Update(T entity)
        {
            throw new NotImplementedException();
        }

        private IQueryable<T> InvokeAPI(IODataCriteria profileCriteria)
        {
            if (profileCriteria == null)
            {
                return null;
            }

            var url = queryBuilder.GetServiceUrl(profileCriteria, CrudAction.Select);
            if (string.IsNullOrWhiteSpace(url))
            {
                throw new InvalidOperationException($"Url not provided for oData Query: {nameof(profileCriteria)}");
            }

            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri(this._serviceUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(url);
                if (response == null)
                {
                    return null;
                }

                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var content = JsonConvert.DeserializeObject<OData<T>>(result.Content.ReadAsStringAsync().Result);

                    if (content == null)
                    {
                        return null;
                    }
                    return content.Value?.Count() > 0 ? content.Value.ToList().AsQueryable<T>() : null;
                }
                else
                {
                    //ToDo: Log error message
                }
            }

            return null;
        }
    }

    public class OData<T>
    {
        public IEnumerable<T> Value { get; set; }
    }
}