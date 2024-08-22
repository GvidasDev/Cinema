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

        private TicketControllerUI ticketController;
        private TheatreControllerUI theatreController;
        private ScheduleControllerUI scheduleController;

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

            ticketController = new TicketControllerUI(mainContainerPanel);
            theatreController = new TheatreControllerUI(mainContainerPanel, ticketController);
            scheduleController = new ScheduleControllerUI(mainContainerPanel, theatreController);
    
            theatreController.SetScheduleController(scheduleController);
            ticketController.SetTheaterController(theatreController);

            scheduleController.ShowSchedule(scheduleList);
        }

        
    }
}