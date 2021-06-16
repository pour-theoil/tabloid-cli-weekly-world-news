using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class PostRepository : DatabaseConnector, IRepository<Post>
    {
        public PostRepository(string connectionString) : base(connectionString) { }

        public List<Post> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.id AS PostId, 
                                                p.Title AS PostTitle, 
                                                p.Url, 
                                                p.PublishDateTime AS Date, 
                                                a.FirstName, a.LastName, 
                                                b.Title as BlogTitle
                                        FROM Post p
                                        LEFT JOIN Blog b ON b.Id = p.BlogId
                                        LEFT JOIN Author a ON a.Id = p.AuthorId
                                        ";

                    List<Post> posts = new List<Post>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Post post = new Post()
                        {
                            //Id = reader.GetInt32(reader.GetOrdinal("PostId")),
                            Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                            Url = reader.GetString(reader.GetOrdinal("Url"))
                            //PublishDateTime = reader.GetDateTime(reader.GetOrdinal("Date")),
                            //Author = new Author()
                            //{
                            //    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            //    LastName = reader.GetString(reader.GetOrdinal("LastName"))
                            //},
                            //Blog = new Blog()
                            //{
                            //    Title = reader.GetString(reader.GetOrdinal("BlogTitle"))
                            //}
                        };
                        posts.Add(post);
                    }
                    reader.Close();

                    return posts;
                }
            }
        }

        public Post Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetByAuthor(int authorId)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.id,
                                               p.Title As PostTitle,
                                               p.URL AS PostUrl,
                                               p.PublishDateTime,
                                               p.AuthorId,
                                               p.BlogId,
                                               a.FirstName,
                                               a.LastName,
                                               a.Bio,
                                               b.Title AS BlogTitle,
                                               b.URL AS BlogUrl
                                          FROM Post p 
                                               LEFT JOIN Author a on p.AuthorId = a.Id
                                               LEFT JOIN Blog b on p.BlogId = b.Id 
                                         WHERE p.AuthorId = @authorId";
                    cmd.Parameters.AddWithValue("@authorId", authorId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Post> posts = new List<Post>();
                    while (reader.Read())
                    {
                        Post post = new Post()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("PostTitle")),
                            Url = reader.GetString(reader.GetOrdinal("PostUrl")),
                            PublishDateTime = reader.GetDateTime(reader.GetOrdinal("PublishDateTime")),
                            Author = new Author()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("AuthorId")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                Bio = reader.GetString(reader.GetOrdinal("Bio")),
                            },
                            Blog = new Blog()
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("BlogId")),
                                Title = reader.GetString(reader.GetOrdinal("BlogTitle")),
                                Url = reader.GetString(reader.GetOrdinal("BlogUrl")),
                            }
                        };
                        posts.Add(post);
                    }

                    reader.Close();

                    return posts;
                }
            }
        }
        /// <summary>
        /// Creates new post
        /// </summary>
        public void Insert(Post post)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Post 
                                        (Title, URL, PublishDateTime, AuthorId, BlogId )
                                        VALUES (@title, @url, @publishDate, @authorId, 1);";
                    cmd.Parameters.AddWithValue("@title", post.Title);
                    cmd.Parameters.AddWithValue("@publishDate", post.PublishDateTime);
                    cmd.Parameters.AddWithValue("@authorId", post.Author.Id);
                    cmd.Parameters.AddWithValue("@url", post.Url);
                    //cmd.Parameters.AddWithValue("@blog", 1);

                    cmd.ExecuteNonQuery();
                }
            }
            
        }
        /// <summary>
        /// Updates a post
        /// </summary>
        public void Update(Post post)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Post
                                        SET Title = @title,
	                                        Url = @url,
	                                        AuthorId = @authorId
                                        WHERE id = @id";

                    cmd.Parameters.AddWithValue("@title", post.Title);
                    cmd.Parameters.AddWithValue("@url", post.Url);
                    cmd.Parameters.AddWithValue("@authorId", post.Author.Id);
                    cmd.Parameters.AddWithValue("@id", post.Id);
                    //cmd.Parameters.AddWithValue("@blogId", post.Blog.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
