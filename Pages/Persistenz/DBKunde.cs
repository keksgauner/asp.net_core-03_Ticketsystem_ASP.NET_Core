using _03_Ticketsystem_ASP.NET_Core.Pages;
using MySql.Data.MySqlClient;

namespace _03_Ticketsystem_ASP.NET_Core
{
    public static class DBKunde
    {
        public static void Erstellen(Kunde kunde)
        {
            using (MySqlConnection con = DBZugriff.OpenDB())
            {
                string sql = $"INSERT INTO Kunde (Vorname,Nachname,GebDatum,Geschlecht) " +
                           $"VALUES ('{kunde.Name}','{kunde.Vorname}','{kunde.Gebdat.ToString("yyyy-MM-dd")}',{(int)kunde.Geschlecht});";
                DBZugriff.ExecuteNonQuery(sql, con);

                // wichtig: GetLastInsertId muss über dieselbe Connection wie der 
                // der INSERT-Befehl aufgerufen werden
                kunde.Id = DBZugriff.GetLastInsertId(con);
            }
        }

        public static List<Kunde> AlleLesen()
        {
            List<Kunde> lstKunden = new List<Kunde>();

            using (MySqlConnection con = DBZugriff.OpenDB())
            {
                String sql = $"SELECT * FROM Kunde;";
                MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con);

                while (rdr.Read())
                {
                    Kunde k = GetDataFromReader(rdr);
                    lstKunden.Add(k);
                }
                rdr.Close();
            }

            return lstKunden;
        }

        public static Kunde Lesen(int id)
        {
            using (MySqlConnection con = DBZugriff.OpenDB())
            {
                String sql = $"SELECT * FROM Kunde WHERE Id={id}";
                using (MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con))
                {
                    if (rdr.Read()) // es kann genau einen oder keinen Datensatz geben -> if statt while
                    {
                        Kunde k = GetDataFromReader(rdr);
                        return k;
                    }

                    throw new Exception($"Kunde mit der ID {id} nicht gefunden!");
                }
            }
        }

        private static Kunde GetDataFromReader(MySqlDataReader rdr)
        {
            // Objektrelationales Mapping (OR-Mapping)
            // Abbildung der relationalen Datenbank auf die objektorientierte Welt

            Kunde k = new Kunde();
            k.Id = rdr.GetInt32("Id");
            k.Vorname = rdr.GetString("Vorname");
            k.Name = rdr.GetString("Nachname");
            k.Gebdat = rdr.GetDateTime("GebDatum");
            k.Geschlecht = (Gender)rdr.GetInt32("Geschlecht");

            return k;
        }
    }
}