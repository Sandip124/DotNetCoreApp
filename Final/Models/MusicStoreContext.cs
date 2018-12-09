using MusicStore.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class MusicStoreContext
    {
        public string ConnectionString { get; set; }

        public MusicStoreContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Album> GetAllAlbums()
        {
            List<Album> list = new List<Album>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Album where id < 10", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Album()
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            ArtistName = reader["ArtistName"].ToString(),
                            Price = Convert.ToInt32(reader["Price"]),
                            Genre = reader["genere"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
