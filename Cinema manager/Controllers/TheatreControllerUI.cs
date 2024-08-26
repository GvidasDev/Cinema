using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Cinema_manager {
    internal class TheatreControllerUI {

        private Panel mainContainerPanel;
        private TicketControllerUI ticketController;
        private ScheduleControllerUI scheduleController;

        public TheatreControllerUI() { }

        public TheatreControllerUI(Panel mainContainerPanel, TicketControllerUI ticketController) {
            this.mainContainerPanel = mainContainerPanel;
            this.ticketController = ticketController;
        }

        public TheatreControllerUI(Panel mainContainerPanel, TicketControllerUI ticketController, ScheduleControllerUI scheduleController) {
            this.mainContainerPanel = mainContainerPanel;
            this.ticketController = ticketController;
            this.scheduleController = scheduleController;
        }

        public void ShowSeatSelection(Schedule schedule, List<Schedule> scheduleList) {
            mainContainerPanel.Controls.Clear();

            Button backButton = new Button {
                Text = "Back",
                Top = 10,
                Left = 10
            };

            backButton.Click += (sender, e) => scheduleController.ShowSchedule(scheduleList);
            mainContainerPanel.Controls.Add(backButton);

            Button buyButton = new Button {
                Text = "Buy",
                Top = 10,
                Left = 100
            };
            buyButton.Click += (sender, e) => ticketController.ShowTickets(schedule, scheduleList);
            mainContainerPanel.Controls.Add(buyButton);

            RenderSeats(schedule);
        }

        private void RenderSeats(Schedule schedule) {
            int rows = schedule.GetTheater().TotalRows + 1;
            int cols = schedule.GetTheater().TotalCols + 1;

            int totalGridWidth = cols * 55;
            int totalGridHeight = rows * 55;

            int containerWidth = mainContainerPanel.Width;
            int containerHeight = mainContainerPanel.Height;

            int startX = (containerWidth - totalGridWidth) / 2;
            int startY = (containerHeight - totalGridHeight) / 2;

            startX = Math.Max(0, startX);
            startY = Math.Max(0, startY);

            foreach (Seat seat in schedule.GetTheater().Seats) {
                int i = seat.Row - 1;
                int j = seat.Column - 1;
                Button seatButton = new Button {
                    Width = 50,
                    Height = 50,
                    Left = startX + j * 55,
                    Top = startY + i * 55,
                    Text = $"{(char)('A' + i)}{j + 1}",
                    Tag = seat
                };

                UpdateSeatButtonColor(seatButton, seat.SeatStatus);

                seatButton.Click += (sender, e) =>
                {
                    Button clickedButton = (Button)sender;
                    Seat clickedSeat = (Seat)clickedButton.Tag;

                    if (clickedSeat.IsFree()) {
                        clickedSeat.SetSelected();
                    } else if (clickedSeat.IsSelected()) {
                        clickedSeat.SetFree();
                    } else if (clickedSeat.IsTaken()) {
                        MessageBox.Show("This seat is taken!");
                    }
                    UpdateSeatButtonColor(seatButton, clickedSeat.SeatStatus);
                };

                mainContainerPanel.Controls.Add(seatButton);
            }
        }

        private void UpdateSeatButtonColor(Button seatButton, Seat.Status status) {
            switch (status) {
                case Seat.Status.FREE:
                    seatButton.BackColor = Color.Green;
                    break;
                case Seat.Status.SELECTED:
                    seatButton.BackColor = Color.Yellow;
                    break;
                case Seat.Status.TAKEN:
                    seatButton.BackColor = Color.Red;
                    break;
                default:
                    seatButton.BackColor = Color.Gray;
                    break;
            }
        }

        public void SetScheduleController(ScheduleControllerUI scheduleController) {
            this.scheduleController = scheduleController;
        }
    }
}
