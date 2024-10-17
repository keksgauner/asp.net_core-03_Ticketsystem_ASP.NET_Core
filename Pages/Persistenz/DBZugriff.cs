using MySql.Data.MySqlClient;

namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    public static class DBZugriff
    {
        public static MySqlConnection OpenDB()
        {
            String constr = "server=bszw.ddns.net;Uid=bfi2225a;pwd=geheim;database=bfi2225a_glisnik_csharp";

            MySqlConnection con = new MySqlConnection(constr);
            con.Open();

            return con;
        }

        public static void CloseDB(MySqlConnection con)
        {
            con.Close();
        }

        /// <summary>
        /// Hilfsmethode für alle INSERT, UPDATE, DELETE etc. Befehle
        /// </summary>
        /// <param name="sql">Der SQL-Befehl</param>
        /// <returns>Anzahl der betroffenen Datensätze</returns>
        public static int ExecuteNonQuery(String sql)
        {
            using (MySqlConnection con = DBZugriff.OpenDB())
            {
                MySqlCommand cmd = new MySqlCommand(sql, con);
                int anz = cmd.ExecuteNonQuery();
                return anz;
            }
        }

        public static int ExecuteNonQuery(String sql, MySqlConnection con)
        {
            MySqlCommand cmd = new MySqlCommand(sql, con);
            int anz = cmd.ExecuteNonQuery();
            return anz;
        }

        /// <summary>
        /// Hilfsmethode für alle INSERT, UPDATE, DELETE etc. Befehle
        /// </summary>
        /// <param name="sql">Der SQL-Befehl</param>
        /// <returns>Anzahl der betroffenen Datensätze</returns>
        public static int ExecuteNonQueryV1(String sql)
        {
            MySqlConnection con = DBZugriff.OpenDB();

            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, con);
                int anz = cmd.ExecuteNonQuery(); // gibt die Anzahl der betroffenen Zeilen zurück
                return anz;
            }
            catch
            {
                throw; // die soeben gefangene Exception wird nach dem finally weiter geworfen
            }
            finally
            {
                DBZugriff.CloseDB(con);
            }
        }

        public static MySqlDataReader ExecuteReader(String sql, MySqlConnection con)
        {
            MySqlCommand cmd = new MySqlCommand(sql, con);
            return cmd.ExecuteReader();
        }

        public static int GetLastInsertId(MySqlConnection con)
        {
            String sql = "SELECT LAST_INSERT_ID()";

            MySqlCommand cmd = new MySqlCommand(sql, con);
            int id = Convert.ToInt32(cmd.ExecuteScalar());

            return id;
        }


    }
}
