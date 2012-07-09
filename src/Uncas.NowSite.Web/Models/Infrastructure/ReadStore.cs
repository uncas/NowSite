using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Uncas.Core;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public abstract class ReadStore<T> :
        IReadStore<T>
        where T : ReadModel
    {
        private readonly string _connectionString;
        private readonly IStringSerializer _stringSerializer;
        protected readonly string _modelName;
        private bool _initialized;

        protected ReadStore(
            string path,
            IStringSerializer stringSerializer,
            string modelName)
        {
            _connectionString =
                string.Format(@"Data Source={0};Version=3;", path);
            _stringSerializer = stringSerializer;
            _modelName = modelName;
            Initialize();
        }

        public virtual void Add(T model)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(string.Format(@"
INSERT INTO {0}
(Id, Created)
SELECT @Id,  @Created
WHERE NOT EXISTS (SELECT * FROM {0} WHERE Id = @Id);

UPDATE {0}
SET Model = @Model,
    Modified = @Modified
WHERE Id = @Id;
", _modelName),
                    new ReadDataModel
                    {
                        Id = model.Id,
                        Created = SystemTime.Now(),
                        Modified = SystemTime.Now(),
                        Model = Serialize(model)
                    });
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var readModel = connection.Query<ReadDataModel>(
                    string.Format("SELECT Id, Model, Created, Modified FROM {0} ORDER BY Created DESC", _modelName));
                return readModel.Select(Deserialize);
            }
        }

        public virtual T GetById(Guid id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<ReadDataModel>(
                    string.Format("SELECT Id, Model, Created, Modified FROM {0} WHERE Id = @Id", _modelName),
                    new { Id = id }).Select(Deserialize).SingleOrDefault();
            }
        }

        public virtual void Delete(Guid id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(
                    string.Format("DELETE FROM {0} WHERE Id = @Id", _modelName),
                    new { Id = id });
            }
        }

        private void Initialize()
        {
            if (_initialized)
            {
                return;
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string existsSql =
                    string.Format(
                    "SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'",
                    _modelName);
                var existing = connection.Query<string>(existsSql);
                if (existing.Count() == 1)
                {
                    return;
                }

                string createSql = string.Format(
                    @"
CREATE TABLE {0}
(Id UNIQUEIDENTIFIER PRIMARY KEY, Model TEXT, Created DATETIME, Modified DATETIME);", _modelName);
                connection.Execute(createSql);
            }

            _initialized = true;
        }

        private string Serialize(T readModel)
        {
            return _stringSerializer.Serialize(readModel);
        }

        private T Deserialize(ReadDataModel data)
        {
            return _stringSerializer.Deserialize<T>(data.Model);
        }
    }
}