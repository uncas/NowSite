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

        public void AddBlogPost(BlogPostReadModel blogPost)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                connection.Execute(
                    "INSERT INTO BlogPost (Id, Title, Content, Created) VALUES (@Id, @Title, @Content, @Created)",
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
    }
}