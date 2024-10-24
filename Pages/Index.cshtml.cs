using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    public class IndexModel : PageModel
    {
        public List<Ticket> LstTickets { get; set; } = DBTicket.AlleLesen();

        [BindProperty]
        public string Sort { get; set; } = default;
        [BindProperty]
        public string SortOrder { get; set; } = default;

        public void OnGet()
        {
            // this.LstTickets = Ticket.AlleLesen();
        }

        public void OnPostBtnLoeschen(int id)
        {
            Ticket.Lesen(id).Loeschen();
            // this.LstTickets = Ticket.AlleLesen(); // alternativ: Remove
            this.LstTickets = DBTicket.AlleLesen(); // alternativ: Remove
        }

        public IActionResult OnPostBtnBearbeiten(int id)
        {
            return RedirectToPage("./Bearbeiten", new { id });
            //return Redirect($"./Bearbeiten?ticketId={id}");
        }

        public IActionResult OnPostBtnNeu()
        {
            return RedirectToPage("./Bearbeiten");
            //return Redirect($"./Bearbeiten?ticketId={id}");
        }

        public void OnPostBtnSort()
        {
            // var sortUm = sort == "Beschreibung"?

            string sort = this.Sort;
            string sortOrder = this.SortOrder;

            // this.Sort = sort;
            // this.SortOrder = sortOrder;

            if (sortOrder == "asc")
            {
                switch (sort)
                {
                    case "Beschreibung":
                        this.LstTickets = LstTickets.OrderBy(t => t.Beschreibung).ToList(); // DBTicket.AlleLesen(sort, sortOrder);
                        break;

                    case "Ersteller":
                        this.LstTickets = LstTickets.OrderBy(t => t.Ersteller).ToList();
                        break;

                    case "Status":
                        this.LstTickets = LstTickets.OrderBy(t => t.TicketStatus).ToList();
                        break;

                    case "Erstellungsdatum":
                        this.LstTickets = LstTickets.OrderBy(t => t.ErstellDatum).ToList();
                        break;
                }
            }

            if (sortOrder == "desc")
            {
                switch (sort)
                {
                    case "Beschreibung":
                        this.LstTickets = LstTickets.OrderByDescending(t => t.Beschreibung).ToList(); // DBTicket.AlleLesen(sort, sortOrder);
                        break;

                    case "Ersteller":
                        this.LstTickets = LstTickets.OrderByDescending(t => t.Ersteller).ToList();
                        break;

                    case "Status":
                        this.LstTickets = LstTickets.OrderByDescending(t => t.TicketStatus).ToList();
                        break;

                    case "Erstellungsdatum":
                        this.LstTickets = LstTickets.OrderByDescending(t => t.ErstellDatum).ToList();
                        break;
                }
            }
        }

        /*
        public void OnPostBtnBearbeiten(int id)
        {
            Response.Redirect("./Bearbeiten?id=" + id);
            //return Redirect($"./Bearbeiten?ticketId={id}");
        }*/


        // DTO == Data Transfer Object -> Es ist eine Hilfsklasse, um die Klassen Ticket und Kunde zu entlasten.
        // Hier werden alle Daten gespeichert (z. B. Title, isNew usw.) // und dann an die View übergeben.
    }
}
