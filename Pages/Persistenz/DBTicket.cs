using MySql.Data.MySqlClient;

namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    public static class DBTicket
    {
        public static void Erstellen(Ticket t)
        {
            String sql = $"INSERT INTO Ticket (Beschreibung, Status, ErstellDatum, ErstellerId)" +
                            $" VALUES ('{t.Beschreibung}',{(int)t.TicketStatus}, '{t.ErstellDatum.ToString("yyyy-MM-dd")}', {t.Ersteller.Id})";
            DBZugriff.ExecuteNonQuery(sql);
        }
        public static void Aendern(Ticket t)
        {
            // Erkennung eines Mehrbenutzerzugriffs durch Überprüfung des
            // Zeitstempels.
            // Update wird nur durchgeführt, wenn der Zeitstempel in der DB seit
            // dem letzten Lesen unverändert geblieben ist.

            String sql = $"UPDATE Ticket SET Beschreibung='{t.Beschreibung}', Status={(int)t.TicketStatus} WHERE Id={t.Id} AND Zeitstempel='{t.Zeitstempel.ToString("yyyy-MM-dd HH:mm:ss")}'";
            int anz = DBZugriff.ExecuteNonQuery(sql);

            if (anz == 0)
                throw new MultiUserAccessException($"Fehler beim Ändern: Ticket mit der Id {t.Id} wurde geändert oder gelöscht");
        }

        public static void Loeschen(Ticket t)
        {
            String sql = $"DELETE FROM Ticket WHERE Id={t.Id};";
            int anz = DBZugriff.ExecuteNonQuery(sql);

            if (anz == 0)
                throw new Exception($"Fehler beim Löschen: Ticket mit der Id {t.Id} wurde nicht gefunden");
        }
        public static List<Ticket> AlleLesen()
        {
            List<Ticket> lstTickets = new List<Ticket>();

            String sql = $"SELECT * FROM Ticket";

            using (MySqlConnection con = DBZugriff.OpenDB())
            {
                using (MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con))
                {
                    while (rdr.Read())
                    {
                        Ticket t = GetDataFromReader(rdr);
                        lstTickets.Add(t);
                    }
                }
            }

            return lstTickets;
        }

        private static Ticket GetDataFromReader(MySqlDataReader rdr)
        {
            Ticket t = new Ticket();
            t.Id = rdr.GetInt32("Id");
            t.Beschreibung = rdr.GetString("Beschreibung");
            t.TicketStatus = (Status)rdr.GetInt32("Status");
            t.ErstellDatum = rdr.GetDateTime("ErstellDatum");
            t.Zeitstempel = rdr.GetDateTime("Zeitstempel");

            // Auflösung des Fremdschlüssels in eine Assoziation
            int kundeId = rdr.GetInt32("ErstellerId"); // Fremdschlüssel
            t.Ersteller = Kunde.Lesen(kundeId);
            return t;
        }

        public static Ticket Lesen(int id)
        {
            String sql = $"SELECT * FROM Ticket WHERE Id={id}";

            using (MySqlConnection con = DBZugriff.OpenDB())
            {
                using (MySqlDataReader rdr = DBZugriff.ExecuteReader(sql, con))
                {
                    if (rdr.Read())
                    {
                        Ticket t = GetDataFromReader(rdr);
                        return t;
                    }

                    throw new Exception($"Ticket mit der Id {id} nicht gefunden");
                }
            }
        }
    }
}
