using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _03_Ticketsystem_ASP.NET_Core.Pages
{
    public class IndexModel : PageModel
    {
        public List<Ticket> LstTickets { get; set; } = DBTicket.AlleLesen();

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
            int id = 0;
            return RedirectToPage("./Bearbeiten", new { id });
            //return Redirect($"./Bearbeiten?ticketId={id}");
        }
        /*
        public void OnPostBtnBearbeiten(int id)
        {
            Response.Redirect("./Bearbeiten?id=" + id);
            //return Redirect($"./Bearbeiten?ticketId={id}");
        }*/
    }
}
