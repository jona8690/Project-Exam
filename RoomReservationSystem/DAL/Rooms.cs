﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public interface IRoomsForMocking
    {
        List<Dictionary<string, string>> GetAllRoomsFromDatabase();
    }
    public class Rooms: Database, IRoomsForMocking
    {
        public List<Dictionary<string, string>> GetAllRoomsFromDatabase()
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            SqlConnection conn = this.OpenConnection();

            SqlCommand command = new SqlCommand("SP_GetAllRooms", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            try
            {
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Dictionary<string, string> row = new Dictionary<string, string>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader[i].ToString());
                        }
                        result.Add(row);
                    }
                }
            }
            finally
            {
                this.CloseConnection();
            }

            return result;
        }

        public void DeleteRoomFromDatabase(string building, string floor, string nr)
        {
            SqlConnection conn = this.OpenConnection();

            SqlCommand command = new SqlCommand("SP_DeleteRoom", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("Building", building);
            command.Parameters.AddWithValue("FloorNr", floor);
            command.Parameters.AddWithValue("Nr", nr);

            try
            {
                command.ExecuteNonQuery();
            }
            finally
            {
                this.CloseConnection();
            }

        }
    }
}
