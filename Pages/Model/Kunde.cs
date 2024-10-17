namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    public class Kunde : Person
    {
        public int Id { get; set; }
        public Kunde() : base()
        {
        }

        public Kunde(string name, string vorname, DateTime gebdat, Gender geschlecht) : base(name, vorname, gebdat, geschlecht)
        {
        }

        public Kunde(int id, string name, string vorname, DateTime gebdat, Gender geschlecht) : base(name, vorname, gebdat, geschlecht)
        {
            Id = id;
        }

        public void Erstellen()
        {
            DBKunde.Erstellen(this);
        }

        public static List<Kunde> AlleLesen()
        {
            return DBKunde.AlleLesen();
        }

        public static Kunde Lesen(int id)
        {
            return DBKunde.Lesen(id);
        }

        public override string ToString()
        {
            //return $"{Id} {Name} {Vorname} {Gebdat.ToShortDateString()} {Geschlecht} {Alter}";
            // return $"{Id,3} {base.ToString()}";
            return $"{Name} {Vorname}";
        }

        public override bool Equals(object? obj)
        {
            Kunde k = obj as Kunde;

            return k != null && this.Id == k.Id;
        }
    }
}

