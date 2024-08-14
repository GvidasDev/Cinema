using System;
using System.Collections.Generic;
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
            // Clear the main container panel
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
                Width = 500,
                Top = yOffset,
                Left = 10
            };

            Label label1 = new Label
            {
                Text = $"{schedule.Movie.Title}",
                Top = 10,
                Left = 10
            };
            panel.Controls.Add(label1);

            Label label2 = new Label
            {
                Text = $"{schedule.StartTime.ToString()}",
                Top = 10,
                Left = 110
            };
            panel.Controls.Add(label2);

            Label label3 = new Label
            {
                Text = $"{schedule.EndTime.ToString()}",
                Top = 10,
                Left = 210
            };
            panel.Controls.Add(label3);

            Button button = new Button
            {
                Text = $"Tickets",
                Top = 10,
                Left = 350
            };
            button.Click += (sender, e) => ShowSeatSelectionView(schedule, scheduleList); // Show seat selection in the same window
            panel.Controls.Add(button);

            parentPanel.Controls.Add(panel);

            return panel.Height + 10;
        }

        private void ShowSeatSelectionView(Schedule schedule, List<Schedule> scheduleList)
        {
            // Clear the main container panel
            mainContainerPanel.Controls.Clear();

            // Add a Back button to return to the main view
            Button backButton = new Button
            {
                Text = "Back",
                Top = 10,
                Left = 10
            };
            backButton.Click += (sender, e) => ShowScheduleView(scheduleList);
            mainContainerPanel.Controls.Add(backButton);

            int rows = schedule.GetTheater().TotalRows;
            int cols = schedule.GetTheater().TotalCols;

            foreach (Seat seat in schedule.GetTheater().Seats)
            {
                int i = seat.Row;
                int j = seat.Column;
                Button seatButton = new Button
                {
                    Width = 50,
                    Height = 50,
                    Left = 100 + j * 55,
                    Top = 50 + i * 55,
                    Text = $"{(char)('A' + i)}{j + 1}",
                    Tag = seat
                };

                switch (seat.SeatStatus)
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

                seatButton.Click += (sender, e) =>
                {
                    Button clickedButton = (Button)sender;
                    Seat clickedSeat = (Seat)clickedButton.Tag;

                    if (clickedSeat.IsFree())
                    {
                        clickedSeat.SetSelected();
                        schedule.GetTheater().setSeat(clickedSeat, i, j);
                    }
                    else
                    {
                        clickedSeat.SetFree();
                        schedule.GetTheater().setSeat(clickedSeat, i, j);
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
    }
}