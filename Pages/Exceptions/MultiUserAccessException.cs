namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    // Vererbung von Basisklasse "Exception"
    public class MultiUserAccessException : Exception
    {
        // Aufruf des Basiskonstruktor mit Übergabe des Exceptiontexts "msg"
        public MultiUserAccessException(String msg) : base(msg)
        {
        }

        public MultiUserAccessException()
        {
        }
    }
}
