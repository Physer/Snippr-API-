﻿using System;
using System.Collections.Generic;
using System.Configuration;
using Nest;
using Snippr.Data.Models;

namespace Snippr.Data.Repositories
{
    public class ElasticRepository : IElasticRepository
    {
        private readonly string _defaultIndex = ConfigurationManager.AppSettings["defaultIndex"];
        private readonly ElasticClient _elasticClient;

        public ElasticRepository(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public void Add<T>(T indexModel, string index = null) where T : IndexModel, new()
        {
            var indexToInsertIn = !string.IsNullOrWhiteSpace(index) ? index : _defaultIndex;
            var result = _elasticClient.Index(indexModel, item => item.Index(indexToInsertIn));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);
        }

        public void Edit<T>(T indexModel, string index = null) where T : IndexModel, new()
        {
            var indexToEditIn = !string.IsNullOrWhiteSpace(index) ? index : _defaultIndex;
            var result = _elasticClient.Index(indexModel, item => item.Index(indexToEditIn));
            if(!result.IsValid)
                throw new Exception(result.OriginalException.Message);
        }

        public void Delete<T>(T indexModel, string index = null) where T : IndexModel, new()
        {
            var indexToInsertIn = !string.IsNullOrWhiteSpace(index) ? index : _defaultIndex;
            var result = _elasticClient.Delete<T>(indexModel, item => item.Index(indexToInsertIn));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);
        }

        public IEnumerable<T> GetAll<T>(string index = null) where T: IndexModel, new()
        {
            var indexToGetFrom = !string.IsNullOrWhiteSpace(index) ? index : _defaultIndex;
            var result = _elasticClient.Search<T>(item => item.Index(indexToGetFrom));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);
            return result.Documents;
        }

        public IEnumerable<T> Search<T>(string searchTerm, string searchValue, string index = null) where T : IndexModel, new()
        {
            var indexToGetFrom = !string.IsNullOrWhiteSpace(index) ? index : _defaultIndex;
            var result = _elasticClient
                .Search<T>(query => query
                    .Index(indexToGetFrom)
                    .Query(q => q.Term(searchTerm, searchValue)));
            if (!result.IsValid)
                throw new Exception(result.OriginalException.Message);
            return result.Documents;
        }

        public T Get<T>(Guid id, string index = null) where T : IndexModel, new()
        {
            var indexToGetFrom = !string.IsNullOrWhiteSpace(index) ? index : _defaultIndex;
            var result = _elasticClient.Get<T>(id, item => item.Index(indexToGetFrom));
            if(!result.IsValid)
                throw new Exception(result.OriginalException.Message);
            return result.Source;
        }
    }
}
