﻿using System.Data.SqlClient;
using FlashCardsLibrary.Models;

namespace FlashCardsLibrary
{
    public static class FlashCardController
    {
        private static string _connectionString = Database._connectionString;
        public static List<FlashCardRead> GetFlashCards()
        {
            var cards = new List<FlashCardRead>();
            string readCmd = @"SELECT * FROM FlashCards";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(readCmd, conn))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        FlashCardRead card = new FlashCardRead((int)reader["ID"], reader["Front"].ToString(), reader["Back"].ToString(), reader["StackName"].ToString());
                        cards.Add(card);
                    }
                }
            }
            return cards;
        }
        public static void AddFlashCard(FlachCardCreate card)
        {
            string insertCmd = @"INSERT INTO FlashCards (StackName , Front,Back) VALUES (@stack, @front, @back)";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(insertCmd, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("stack", card.StackName);
                        cmd.Parameters.AddWithValue("front", card.Front);
                        cmd.Parameters.AddWithValue("back", card.Back);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error adding FlashCard : {e}");
                        Console.ReadLine();
                    }
                }

            }
        }
        public static void DeleteFlashCard(FlashCardDelete card)
        {
            string deleteCmd = @"DELETE FROM FlashCards WHERE ID = @id";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(deleteCmd, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("id", card.ID);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error Deleting FlashCard {e}");
                        Console.ReadLine();
                    }
                }
            }
        }
        public static void UpdateFlashCard(FlashCardUpdate oldCard,FlashCardUpdate newCard)
        {
            string updateCmd = @"UPDATE FlashCards SET Front = @front, Back = @back WHERE ID  = @id";
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(updateCmd, conn))
                {
                    try
                    {
                        cmd.Parameters.AddWithValue("front", newCard.Front);
                        cmd.Parameters.AddWithValue("back", newCard.Back);
                        cmd.Parameters.AddWithValue("id", oldCard.ID);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error Updating FlashCards : {e}");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
