using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {

            string Server = @".\SQLEXPRESS";
            string Database = @"database";
            string User = @"sa";
            string Password = @"Password";

            List<string> IDList = new List<string>();

            IDList = File.ReadLines(@"ID.txt").ToList();


            string connectionString = $"Server={Server};Database={Database};User Id={User};Password={Password};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                if(connection.State == System.Data.ConnectionState.Open) connection.Close();
                connection.Open();

                foreach (var x in IDList)
                {
                    string newstring = String.Empty;
                    string queryString = $"select top 1 Fusstext from Bestellung where id = {x}";
                    SqlCommand command = new SqlCommand(queryString, connection);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string desc = (string)reader["Fusstext"];
                            newstring = desc.Split(@"tzung! \par\par")[0] + @"tzung! \par\par{\field{\*\fldinst INCLUDEPICTURE ""R:\\\\USERDATA\\\\MISCELLANEOUS\\\\CovidInfo.png"" \\d}{\fldrslt {}}}\par }}";
                        }


                    }

                    string querystring2 = $"update bestellung set Fusstext = '{newstring.Replace("'", "''")}' where id = {x}";

                    SqlCommand command2 = new SqlCommand(querystring2, connection);
                    command2.ExecuteNonQuery();

                    Console.WriteLine(x + " Erledigt");
                }
            }
        }
    }
}
