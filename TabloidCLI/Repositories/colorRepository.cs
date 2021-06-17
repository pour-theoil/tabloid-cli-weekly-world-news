using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using TabloidCLI.Models;

namespace TabloidCLI.Repositories
{
    public class colorRepository : DatabaseConnector
    {
        public colorRepository(string connectionString) : base(connectionString) { }
        //method to set int selection 
        public void SetColor(int colorint)
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                 using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "update ConsoleColor set ColorId = @int where id = 1;";
                    cmd.Parameters.AddWithValue("@int", colorint);
                    cmd.ExecuteNonQuery();
                }
            }


        }

        public Color GetColor()
        {
            using(SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "select colorid from consolecolor where id = 1;";
                    SqlDataReader reader = cmd.ExecuteReader();
                    Color color = null;
                    while(reader.Read())
                    {
                        if(color == null)
                        {
                            color = new Color()
                            {
                                id = 1,
                                colorid = reader.GetInt32(reader.GetOrdinal("ColorId"))

                            };

                        }
                    }
                    reader.Close();
                    return color;
                    
                }
            }
        }

        //method update the database
    }
}
