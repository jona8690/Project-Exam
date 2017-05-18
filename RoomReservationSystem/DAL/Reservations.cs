﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public interface IReservationsForMocking
    {
        List<Dictionary<string, string>> GetAllReservationsFromDatabase();
    }
    public class Reservations: Database, IReservationsForMocking
    {
        public List<Dictionary<string, string>> GetAllReservationsFromDatabase()
        {
            List<Dictionary<string, string>> result = new List<Dictionary<string, string>>();

            SqlConnection conn = this.OpenConnection();

            SqlCommand command = new SqlCommand("SP_GetAllReservations", conn)
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

        public void DeleteReservationFromDatabase(string username, DateTime from, DateTime to)
        {
            SqlConnection conn = this.OpenConnection();

            SqlCommand command = new SqlCommand("SP_DeleteReservation", conn)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("Username", username);
            command.Parameters.AddWithValue("DateFrom", from);
            command.Parameters.AddWithValue("DateTo", to);

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
