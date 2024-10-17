using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    [BindProperties(SupportsGet = true)]
    public class BearbeitenModel : PageModel
    {
        public Ticket Ticket { get; set; }
        public string ErstellDatum { get; set; }
        public List<Kunde> lstKunden { get; set; } = Kunde.AlleLesen();

        /*public string Beschreibung { get; set; } = default;
        public Status TicketStatus { get; set; } = default;
        public DateTime ErstellDatum { get; set; } = default;*/
        public Kunde Ersteller { get; set; } = default;
        // public DateTime Zeitstempel { get; set; } = default;

        public void OnGet(int id) // nullable: int? id 
        {
            this.Ticket = Ticket.Lesen(id);
            // this.Beschreibung = Ticket.Beschreibung;
            this.lstKunden = Kunde.AlleLesen();
            this.ErstellDatum = Ticket.ErstellDatum.ToShortDateString();
        }
        public IActionResult OnPostBtnSpeichern()
        {
            bool saved = default;
            // Ticket.Beschreibung = Beschreibung;
            Ticket.Aendern();
            // Response.Redirect("./Index");
            saved = Ticket.Lesen(Ticket.Id).Beschreibung == Ticket.Beschreibung && Ticket.Lesen(Ticket.Id).TicketStatus == Ticket.TicketStatus && Ticket.Lesen(Ticket.Id).TicketStatus == Ticket.TicketStatus;

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
