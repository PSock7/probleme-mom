using System;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Data.SQLite;

/* @author : Othmane Oubouselham, Victor Tran, Pape Mouhamadou Mamoune Sock*/
namespace Projet_applications
{
    public class Database
    {
        public SQLiteConnection myConnection;
        private static Database currentInstance;
        private static string path = "../../../database.db";
        public Database()
        {
            try
            {
                if (File.Exists(path))
                {
                    myConnection = new SQLiteConnection("Data Source="+path);
                }
                else
                {
                    SQLiteConnection.CreateFile(path);
                    myConnection = new SQLiteConnection("Data Source="+path);
                    string query = File.ReadAllText("../../../database.sql");
                    SQLiteCommand myCommand = new SQLiteCommand(query, myConnection);
                    myCommand.ExecuteNonQuery();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public SQLiteDataReader Select(string query)
        {
            SQLiteCommand myCommand = new SQLiteCommand(query, myConnection);
            return myCommand.ExecuteReader();
        }
        
        public void Open()
        {
            if(myConnection.State != ConnectionState.Open)myConnection.Open();
        }
        
        
        public void Close()
        {
            if(myConnection.State != ConnectionState.Closed)myConnection.Close();
        }


        public void Seed()
        {
            string query = File.ReadAllText("../../../seed.sql");
            SQLiteCommand myCommand = new SQLiteCommand(query, myConnection);
            myCommand.ExecuteNonQuery();
        }


        public static Database getInstance()
        {
            if (currentInstance == null) currentInstance = new Database();

            return currentInstance;
        }
    }
}