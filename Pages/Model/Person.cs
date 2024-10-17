namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    public enum Gender
    {
        maennlich, weiblich
    }

    public class Person
    {
        public string Vorname { get; set; } = "";
        public string Name { get; set; } = "";

        public Gender Geschlecht { get; set; } = default;

        private DateTime _gebdat;
        public DateTime Gebdat
        {
            get
            {
                return _gebdat;
            }

            set
            {
                // man könnte hier auch eine spezielle Exception verwenden
                // wie z.B. ArgumentOutOfRangeException
                if (DateTime.Now < value)
                    throw new Exception("Geburtsdatum darf nicht in der Zukunft liegen");

                _gebdat = value;
            }
        }

        public int Alter
        {
            get
            {
                return berechneAlter();
            }
        }



        public Person()
        {

        }

        public Person(string name, string vorname, DateTime gebdat, Gender geschlecht)
        {
            Name = name;
            Vorname = vorname;
            Gebdat = gebdat;
            Geschlecht = geschlecht;
        }

        public int berechneAlter()
        {
            DateTime today = DateTime.Today;

            // Unterschied Jahr
            int age = today.Year - Gebdat.Year;

            // Abziehen wenn Monat noch nicht war, ODER
            // Abziehen wenn gleicher Monat, aber Tag noch nicht war
            if ((Gebdat.Month > today.Month) || (Gebdat.Month == today.Month && Gebdat.Day > today.Day))
                age--;

            // Alternativ:
            // if(Gebdat.AddYears(alter) > heute)
            //  age--


            return age;
        }

        public override string ToString()
        {
            return $"{Name,-20} {Vorname,-20} {Gebdat.ToShortDateString()} {Geschlecht,-5} {Alter,4}";
        }
    }
}
