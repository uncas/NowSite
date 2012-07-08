using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using Dapper;

namespace Uncas.NowSite.Web.Models.ReadStores
{
    public class BlogPostReadStore : IBlogPostReadStore
    {
        private readonly string _connectionString;

        public BlogPostReadStore(string path)
        {
            _connectionString =
                string.Format(@"Data Source={0};Version=3;", path);
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
SET Title = @Title,
    Content = @Content
WHERE Id = @Id;
",
                    blogPost);
            }
        }

        public IEnumerable<BlogPostReadModel> GetBlogPosts()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<BlogPostReadModel>(
                    "SELECT Id, Title, Content FROM BlogPost ORDER BY Created DESC");
            }
        }

        public BlogPostReadModel GetById(Guid id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<BlogPostReadModel>(
                    "SELECT Id, Title, Content, Created FROM BlogPost WHERE Id = @Id",
                    new { Id = id }).SingleOrDefault();
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
                if (existing.Count() == 0)
                {
                    const string createSql =
                        @"
CREATE TABLE BlogPost
(Id UNIQUEIDENTIFIER PRIMARY KEY, Title TEXT, Content TEXT, Created DATETIME);";
                    connection.Execute(createSql);
                }
            }
        }
    }
}