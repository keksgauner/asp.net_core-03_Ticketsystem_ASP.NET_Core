using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    [BindProperties(SupportsGet = true)]
    public class BearbeitenModel : PageModel
    {
        public string Title2 { get; set; } = "Ticket bearbeiten";

        public Ticket Ticket { get; set; }
        public DateTime ErstellDatum { get; set; }
        public List<Kunde> lstKunden { get; set; } = Kunde.AlleLesen();
        public bool Neu { get; set; } = true;

        /*public string Beschreibung { get; set; } = default;
        public Status TicketStatus { get; set; } = default;
        public DateTime ErstellDatum { get; set; } = default;*/
        public Kunde Ersteller { get; set; } = default;

        public int ErstellerId { get; set; } = 0;
        // public DateTime Zeitstempel { get; set; } = default;

        public bool ErstellerDisabled
        {
            get
            {
                // TicketId != 0 => neues Ticket => Erstellerauswahl möglich
                return Ticket.Id != 0;
            }
        }
        public bool StatusDisabled
        {
            get
            {
                // TicketId == 0 => neues Ticket => Status "immer" neu
                return Ticket.Id == 0;
            }
        }

        public void OnGet(int? id) // nullable: int? id 
        {
            if (id == null)
            {
                this.Title2 = "Neues Ticket anlegen";
                this.Ticket = new Ticket();
                this.ErstellDatum = DateTime.Now.Date;
                this.Neu = true;
                //this.Ticket.Ersteller = Ticket.Ersteller;
                // this.Ersteller = Kunde.Lesen(ErstellerId);
            }


            else
            {
                this.Title2 = "Ticket bearbeiten";
                this.Ticket = Ticket.Lesen(id.Value);
                this.ErstellDatum = Ticket.ErstellDatum.Date;
                this.Neu = false;
                this.ErstellerId = Ticket.Ersteller.Id;
                this.Ersteller = Kunde.Lesen(ErstellerId);
            }

            // this.Beschreibung = Ticket.Beschreibung;
            this.lstKunden = Kunde.AlleLesen();

        }
        public IActionResult OnPostBtnSpeichern()
        {
            /*if (Ticket.Id == 0)
                Ticket.Erstellen();
            else
                Ticket.Aendern();*/

            /*if (Ticket.isNew)
                Ticket.Erstellen(); --> evtl. im Ticket implementieren*/

            bool saved = default;
            // Ticket.Beschreibung = Beschreibung;

            /*if(Ticket.Id < 1) // ist das selbe wie this.Neu == true
            {
                Ticket.Erstellen();
                saved = true;
            }
            else
            {
                Ticket.Aendern();
                saved = Ticket.Lesen(Ticket.Id).Beschreibung == Ticket.Beschreibung && Ticket.Lesen(Ticket.Id).TicketStatus == Ticket.TicketStatus && Ticket.Lesen(Ticket.Id).TicketStatus == Ticket.TicketStatus;
            }*/

            // if (this.Neu == false)
            if (Ticket.Id > 1)
            {
                Ticket.Aendern();
                saved = Ticket.Lesen(Ticket.Id).Beschreibung == Ticket.Beschreibung && Ticket.Lesen(Ticket.Id).TicketStatus == Ticket.TicketStatus && Ticket.Lesen(Ticket.Id).TicketStatus == Ticket.TicketStatus;
            }
            // Response.Redirect("./Index");

            else
            {
                // Der Ersteller kann über HTTP nicht als vollständiges 
                // Objekt übertragen werden -> wir übermitteln nur die ID
                // Hier muss aus der ID wieder das betreffende Kunden-Objekt
                // gelesen und dem Ticket zugeordnet werden.
                Ticket.Ersteller = Kunde.Lesen(ErstellerId);

                Ticket.Erstellen();
                saved = true;
            }


            if (saved)
                return RedirectToPage("./Index");
            else
                return RedirectToPage("./Bearbeiten");
            // OnPostBtnZurueck();
        }

        public IActionResult OnPostBtnZurueck()
        {
            return RedirectToPage("./Index");
        }

    }
}
