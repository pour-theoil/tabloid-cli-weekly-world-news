using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Text;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class NoteRepository : DatabaseConnector, IRepository<Note>
    {

        public NoteRepository(string connectionString) : base(connectionString) { }
        public void Delete(int id)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update note set isdeleted = 1 where Id =@id";
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Note Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Note> GetAll()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"select Id, Title, Content, CreateDateTime, PostId from Note where isdeleted = 0";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Note> notes = new List<Note>();
                    while (reader.Read())
                    {
                        Note note = new Note()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Title = reader.GetString(reader.GetOrdinal("Title")),
                            Content = reader.GetString(reader.GetOrdinal("Content")),
                            CreateDateTime = reader.GetDateTime(reader.GetOrdinal("CreateDateTime")),
                            PostId = reader.GetInt32(reader.GetOrdinal("PostId"))
                        };
                        notes.Add(note);
                    }
                    reader.Close();
                    return notes;
                }
            }
        }

        public void Insert(Note entry)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using(SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "insert into note (title, content, createdatetime, postId) values (@title, @content, @createdatetime, @postid)";
                    cmd.Parameters.AddWithValue("@id", entry.Id);
                    cmd.Parameters.AddWithValue("@content", entry.Content);
                    cmd.Parameters.AddWithValue("@createdatetime", entry.CreateDateTime);
                    cmd.Parameters.AddWithValue("@title", entry.Title);
                    cmd.Parameters.AddWithValue("@postid", entry.PostId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Note entry)
        {
            throw new NotImplementedException();
        }
    }
}
