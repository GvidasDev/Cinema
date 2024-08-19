using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Windows.Forms;

namespace Cinema_manager
{
    public partial class Form1 : Form
    {
        private Panel mainContainerPanel;

        public Form1()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            List<Movie> movieList = InOut.GetMovies();
            List<Schedule> scheduleList = InOut.GetSchedules(movieList);
            Cinema cinema = new Cinema(movieList, scheduleList);

            mainContainerPanel = new Panel
            {
                Dock = DockStyle.Fill
            };
            this.Controls.Add(mainContainerPanel);

            ShowScheduleView(scheduleList);
        }


        private void ShowScheduleView(List<Schedule> scheduleList)
        {
            mainContainerPanel.Controls.Clear();

            Panel scrollablePanel = new Panel
            {
                AutoScroll = true,
                Dock = DockStyle.Fill
            };
            mainContainerPanel.Controls.Add(scrollablePanel);

            int totalHeight = 0;

            for (int i = 0; i < scheduleList.Count; i++)
            {
                totalHeight += CreateUIBlock(scrollablePanel, i, totalHeight, scheduleList[i], scheduleList);
            }

            scrollablePanel.AutoScrollMinSize = new Size(0, totalHeight);
        }

        private int CreateUIBlock(Panel parentPanel, int index, int yOffset, Schedule schedule, List<Schedule> scheduleList)
        {
            Panel panel = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Width = 550,
                Top = yOffset,
                Left = 10
            };

            Label label1 = new Label
            {
                Text = $"{schedule.Movie.Title}",
                Width = 100,
                Top = 10,
                Left = 10
            };
            panel.Controls.Add(label1);

            Label label2 = new Label
            {
                Text = $"{schedule.StartTime.ToString()}",
                Width = 100,
                Top = 10,
                Left = 110
            };
            panel.Controls.Add(label2);

            Label label3 = new Label
            {
                Text = $"{schedule.EndTime.ToString()}",
                Width = 100,
                Top = 10,
                Left = 210
            };
            panel.Controls.Add(label3);

            double totalSeats = schedule.GetTheater().getTotalSeats();
            double perc = totalSeats > 0 ? (schedule.GetTheater().getTotalTaken() * 100) / totalSeats : 0;

            Label label4 = new Label
            {
                Text = $"{perc:F0}%",
                Width = 50,
                Top = 10,
                Left = 310
            };
            panel.Controls.Add(label4);

            Button button = new Button
            {
                Text = $"Tickets",
                Width = 100,
                Top = 10,
                Left = 400
            };
            button.Click += (sender, e) => ShowSeatSelectionView(schedule, scheduleList);
            panel.Controls.Add(button);

            parentPanel.Controls.Add(panel);

            return panel.Height + 10;
        }

        private void ShowSeatSelectionView(Schedule schedule, List<Schedule> scheduleList)
        {
            mainContainerPanel.Controls.Clear();

            Button backButton = new Button
            {
                Text = "Back",
                Top = 10,
                Left = 10
            };

            backButton.Click += (sender, e) => ShowScheduleView(scheduleList);
            mainContainerPanel.Controls.Add(backButton);
            
            Button buyButton = new Button
            {
                Text = "Buy",
                Top = 10,
                Left = 100
            };
            buyButton.Click += (sender, e) => BuyTicketsView(schedule, scheduleList);
            mainContainerPanel.Controls.Add(buyButton);

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

            foreach (Seat seat in schedule.GetTheater().Seats)
            {
                int i = seat.Row-1;
                int j = seat.Column-1;
                Button seatButton = new Button
                {
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

                    if (clickedSeat.IsFree())
                    {
                        clickedSeat.SetSelected();
                    }
                    else if(clickedSeat.IsSelected())
                    {
                        clickedSeat.SetFree();
                    }
                    else if (clickedSeat.IsTaken())
                    {
                        MessageBox.Show("This seat is taken!");
                    }
                    UpdateSeatButtonColor(seatButton, seat.SeatStatus);
                };

                mainContainerPanel.Controls.Add(seatButton);
            }
        }

        private void UpdateSeatButtonColor(Button seatButton, Seat.Status status)
        {
            switch (status)
            {
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

        private void BuyTicketsView(Schedule schedule, List<Schedule> scheduleList)
        {
            mainContainerPanel.Controls.Clear();
            Button backButton = new Button
            {
                Text = "Back",
                Top = 10,
                Left = 10
            };

            RichTextBox outputText = new RichTextBox
            {
                Width = 800,
                Height = 400,
                ReadOnly = true,
                Top = 60,
                Left = 10,
            };

            backButton.Click += (sender, e) => ShowSeatSelectionView(schedule, scheduleList);
            mainContainerPanel.Controls.Add(backButton);
            mainContainerPanel.Controls.Add(outputText);
            List<Ticket> tickets = MakeTickets(schedule);

            outputText.AppendText(printTickets(tickets));
            Console.WriteLine(printTickets(tickets));
        }

        private List<Ticket> MakeTickets(Schedule schedule) 
        {
            List<Ticket> list = new List<Ticket>();
            List<Seat> seats= schedule.GetTheater().Seats;
            foreach (Seat seat in seats) 
            {
                if (seat.SeatStatus == Seat.Status.SELECTED) 
                {
                    seat.SetTaken();
                    Ticket ticket = new Ticket(schedule.Movie.Title, seat);
                    list.Add(ticket);
                }
            }
            return list;
        }

        private string printTickets(List<Ticket> tickets) 
        {
            string output = "";
            foreach (Ticket ticket in tickets)
            {
                output += ticket.ToString();
            }
            return output;
        }
    }
}