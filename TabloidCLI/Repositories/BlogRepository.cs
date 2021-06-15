// Colten Mayberry

using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class BlogRepository : DatabaseConnector, IRepository<Blog>
    {
        // For connection to database.
        public BlogRepository(string connectionString) : base(connectionString) { }

        // Get all blogs.
        public List<Blog> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT Id, Title, Url
                                        FROM Blog";

                    List<Blog> blogs = new List<Blog>();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Blog blog = new Blog()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Url = reader.GetString(reader.GetOrdinal("Url"))
                        };
                        blogs.Add(blog);
                    }

                    reader.Close();

                    return blogs;
                }
            }
        }


        // Get a single blog.
        public Blog Get(int id)
        {
            throw new NotImplementedException();
        }


        // Add a blog.
        public void Insert(Blog entry)
        {
            // A connection to the SQL database.
            using (SqlConnection conn = Connection)
            {
                // Opening the connection.
                conn.Open();

                // Building an object that will execute SQL commands.
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Blog (Title, URL)
                                        VALUES (@title, @url)";
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@url", entry.Url);

                    cmd.ExecuteNonQuery();
                }
            }
        }


        public void Update(Blog entry)
        {
            throw new NotImplementedException();
        }


        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
