namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    public enum Status
    {
        neu, bearbeitung, geloest
    }
    public class Ticket
    {
        public int Id { set; get; } = default;
        public string Beschreibung { get; set; } = default;
        public Status TicketStatus { get; set; } = default;
        public DateTime ErstellDatum { get; set; }
        public Kunde Ersteller { get; set; } = default;

        public DateTime Zeitstempel { get; set; }

        //public string Kurzbeschreibung
        //{
        //    get
        //    {
        //        if (Beschreibung.Length > 20)
        //            return Beschreibung.Substring(0, 17) + "...";
        //        else
        //            return Beschreibung;
        //    }
        //}


        public Ticket(int id, string beschreibung, Status ticketStatus, DateTime erstellDatum, Kunde ersteller)
        {
            Id = id;
            Beschreibung = beschreibung;
            TicketStatus = ticketStatus;
            ErstellDatum = erstellDatum;
            Ersteller = ersteller;
        }

        public Ticket()
        {
            TicketStatus = Status.neu;
            ErstellDatum = DateTime.Now;
        }

        public void Erstellen()
        {
            DBTicket.Erstellen(this);
        }

        public void Aendern()
        {
            DBTicket.Aendern(this);
        }

        public void Loeschen()
        {
            DBTicket.Loeschen(this);
        }

        public static List<Ticket> AlleLesen()
        {
            return DBTicket.AlleLesen();
        }

        public static Ticket Lesen(int id)
        {
            return DBTicket.Lesen(id);
        }


        //public override bool Equals(object? obj)
        //{
        //  return obj is Ticket ticket && this.Id == ticket.Id;
        //}

        public override bool Equals(object? obj)
        {
            //Ticket t = (Ticket)obj; // cast Variante 1: wirft im Fehlerfall Exception
            Ticket t = obj as Ticket; // cast Variante 2: liefert im Fehlerfall null

            if (t == null)
                return false;

            //if (t.Id == this.Id)
            //  return true;
            //else
            //  return false;

            return t.Id == Id;
        }

        public override string ToString()
        {
            //return $"{Id.ToString().PadLeft(3)} {Beschreibung.PadRight(20)} {TicketStatus.ToString().PadRight(10)} {ErstellDatum.ToShortDateString().PadRight(10)} {Ersteller.Name} {Ersteller.Vorname}";
            return $"{Id,3}  {TicketStatus,-10} {ErstellDatum.ToShortDateString(),-10} {Ersteller.Name} {Ersteller.Vorname}";
        }


    }
}
