using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;
using Uncas.Core;
using Uncas.NowSite.Web.Utilities;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostReadStore : IBlogPostReadStore
    {
        private readonly string _connectionString;
        private readonly IStringSerializer _stringSerializer;

        public BlogPostReadStore(
            string path,
            IStringSerializer stringSerializer)
        {
            _connectionString =
                string.Format(@"Data Source={0};Version=3;", path);
            _stringSerializer = stringSerializer;
            Initialize();
        }

        public void AddBlogPost(BlogPostReadModel blogPost)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(@"
INSERT INTO BlogPost
(Id, Created)
SELECT @Id,  @Created
WHERE NOT EXISTS (SELECT * FROM BlogPost WHERE Id = @Id);

UPDATE BlogPost
SET Model = @Model,
    Modified = @Modified
WHERE Id = @Id;
",
                    new ReadDataModel
                    {
                        Id = blogPost.Id,
                        Created = SystemTime.Now(),
                        Modified = SystemTime.Now(),
                        Model = Serialize(blogPost)
                    });
            }
        }

        public IEnumerable<BlogPostReadModel> GetBlogPosts()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var readModel = connection.Query<ReadDataModel>(
                    "SELECT Id, Model, Created, Modified FROM BlogPost ORDER BY Created DESC");
                return readModel.Select(Deserialize);
            }
        }

        public BlogPostReadModel GetById(Guid id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<ReadDataModel>(
                    "SELECT Id, Model, Created, Modified FROM BlogPost WHERE Id = @Id",
                    new { Id = id }).Select(Deserialize).SingleOrDefault();
            }
        }

        private void Initialize()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                const string existsSql =
                    "SELECT name FROM sqlite_master WHERE type='table' AND name='BlogPost'";
                var existing = connection.Query<string>(existsSql);
                if (existing.Count() == 1)
                {
                    return;
                }

                const string createSql =
                    @"
CREATE TABLE BlogPost
(Id UNIQUEIDENTIFIER PRIMARY KEY, Model TEXT, Created DATETIME, Modified DATETIME);";
                connection.Execute(createSql);
            }
        }

        private string Serialize(BlogPostReadModel readModel)
        {
            return _stringSerializer.Serialize(readModel);
        }

        private BlogPostReadModel Deserialize(ReadDataModel data)
        {
            return _stringSerializer.Deserialize<BlogPostReadModel>(data.Model);
        }
    }
}