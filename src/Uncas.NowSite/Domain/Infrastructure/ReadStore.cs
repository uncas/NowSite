using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Uncas.Core;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.Infrastructure
{
    public class ReadStore : IReadStore
    {
        private static readonly IList<string> _initializedModelNames =
            new List<string>();
        private static readonly IDictionary<Type, string> _mappedModelNames =
            new Dictionary<Type, string>();

        private readonly string _connectionString;
        private readonly IStringSerializer _stringSerializer;

        protected ReadStore(
            string path,
            IStringSerializer stringSerializer)
        {
            _connectionString =
                string.Format(@"Data Source={0};Version=3;", path);
            _stringSerializer = stringSerializer;
        }

        public virtual void Add<T>(T model) where T : ReadModel
        {
            string modelName = model.ModelName;
            Initialize(modelName);
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
", modelName),
                    new
                    {
                        Id = model.Id,
                        Created = SystemTime.Now(),
                        Modified = SystemTime.Now(),
                        Model = Serialize(model)
                    });
            }
        }

        public virtual IEnumerable<T> GetAll<T>() where T : ReadModel
        {
            string modelName = GetModelName<T>();
            Initialize(modelName);
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var readModel = connection.Query<dynamic>(
                    string.Format("SELECT Id, Model, Created, Modified FROM {0} ORDER BY Created DESC", modelName));
                return readModel.Select(Deserialize<T>);
            }
        }

        public virtual T GetById<T>(Guid id) where T : ReadModel
        {
            string modelName = GetModelName<T>();
            Initialize(modelName);
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<dynamic>(
                    string.Format("SELECT Id, Model, Created, Modified FROM {0} WHERE Id = @Id", modelName),
                    new { Id = id }).Select(Deserialize<T>).SingleOrDefault();
            }
        }

        public virtual void Delete<T>(Guid id) where T : ReadModel
        {
            string modelName = GetModelName<T>();
            Initialize(modelName);
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(
                    string.Format("DELETE FROM {0} WHERE Id = @Id", modelName),
                    new { Id = id });
            }
        }

        protected string GetModelName<T>() where T : ReadModel
        {
            Type type = typeof(T);
            if (_mappedModelNames.ContainsKey(type))
                return _mappedModelNames[type];
            var dummyInstance =
                (T)type.GetConstructor(Type.EmptyTypes).Invoke(null);
            string modelName = dummyInstance.ModelName;
            _mappedModelNames.Add(type, modelName);
            return modelName;
        }

        private void Initialize(string modelName)
        {
            if (_initializedModelNames.Contains(modelName))
            {
                return;
            }

            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                string existsSql =
                    string.Format(
                    "SELECT name FROM sqlite_master WHERE type='table' AND name='{0}'",
                    modelName);
                var existing = connection.Query<string>(existsSql);
                if (existing.Count() == 1)
                {
                    return;
                }

                string createSql = string.Format(
                    @"
CREATE TABLE {0}
(Id UNIQUEIDENTIFIER PRIMARY KEY, Model TEXT, Created DATETIME, Modified DATETIME);", modelName);
                connection.Execute(createSql);
            }

            _initializedModelNames.Add(modelName);
        }

        private string Serialize<T>(T readModel)
        {
            return _stringSerializer.Serialize(readModel);
        }

        private T Deserialize<T>(dynamic data)
        {
            return _stringSerializer.Deserialize<T>(data.Model);
        }
    }
}