using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema_manager {
    internal class TicketControllerUI {

        private Panel mainContainerPanel;
        private TheatreControllerUI theatreController;

        public TicketControllerUI() { }

        public TicketControllerUI(Panel mainContainerPanel) {
            this.mainContainerPanel = mainContainerPanel;
        }

        public TicketControllerUI(Panel mainContainerPanel, TheatreControllerUI theatreController) {
            this.mainContainerPanel = mainContainerPanel;
            this.theatreController = theatreController;
        }
        public void ShowTickets(Schedule schedule, List<Schedule> scheduleList) {
            mainContainerPanel.Controls.Clear();
            Button backButton = CreateBackButton(schedule, scheduleList);
            RichTextBox outputText = CreateRichTextBox(mainContainerPanel);

            List<Ticket> tickets = MakeTickets(schedule);
            outputText.AppendText(PrintTickets(tickets));
        }

        private Button CreateBackButton(Schedule schedule, List<Schedule> scheduleList) {
            Button backButton = new Button {
                Text = "Back",
                Top = 10,
                Left = 10
            };

            backButton.Click += (sender, e) => theatreController.ShowSeatSelection(schedule, scheduleList);
            mainContainerPanel.Controls.Add(backButton);

            return backButton;
        }

        private RichTextBox CreateRichTextBox(Panel mainContainerPanel) {
            RichTextBox outputText = new RichTextBox {
                Width = 800,
                Height = 400,
                ReadOnly = true,
                Top = 60,
                Left = 10
            };
            mainContainerPanel.Controls.Add(outputText);
            return outputText;
        }

        private List<Ticket> MakeTickets(Schedule schedule) {
            List<Ticket> list = new List<Ticket>();
            List<Seat> seats = schedule.GetTheater().Seats;
            foreach (Seat seat in seats) {
                if (seat.SeatStatus == Seat.Status.SELECTED) {
                    seat.SetTaken();
                    Ticket ticket = new Ticket(schedule.Movie.Title, seat);
                    list.Add(ticket);
                }
            }
            return list;
        }

        private string PrintTickets(List<Ticket> tickets) {
            return string.Join(Environment.NewLine, tickets.Select(ticket => ticket.ToString()));
        }

        public void SetTheaterController(TheatreControllerUI theatreController) {
            this.theatreController = theatreController;
        }
    }
}
