using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace Cinema_manager
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            List<Movie> movieList = InOut.GetMovies();
            List<Schedule> scheduleList = InOut.GetSchedules(movieList);
            Cinema cinema = new Cinema(movieList, scheduleList);

            Panel scrollablePanel = new Panel();
            scrollablePanel.AutoScroll = true; // Enable scrolling
            scrollablePanel.Dock = DockStyle.Fill; // Fill the entire form
            this.Controls.Add(scrollablePanel);

            int totalHeight = 0; // Variable to keep track of total height of added blocks

            // Add multiple blocks
            for (int i = 0; i < 10; i++) // Adjust the number of blocks as needed
            {
                totalHeight += CreateUIBlock(scrollablePanel, i, totalHeight, scheduleList[i]);
            }
        }

        private int CreateUIBlock(Panel parentPanel, int index, int yOffset, Schedule schedule)
        {
            // Create a Panel to hold the UI block
            Panel panel = new Panel();
            panel.BorderStyle = BorderStyle.FixedSingle;
            panel.Width = 200;
            panel.Height = 150;
            panel.Top = index * 160; // Adjust the position based on index
            panel.Left = 10;

            // Create the first label
            Label label1 = new Label();
            label1.Text = $"{schedule.Movie.Title}";
            label1.Top = 10;
            label1.Left = 10;
            panel.Controls.Add(label1);

            // Create the second label
            Label label2 = new Label();
            label2.Text = $"{schedule.Movie.Description}";
            label2.Top = 40;
            label2.Left = 10;
            panel.Controls.Add(label2);

            // Create the third label
            Label label3 = new Label();
            label3.Text = $"{schedule.StartTime}";
            label3.Top = 70;
            label3.Left = 10;
            panel.Controls.Add(label3);

            // Create the button
            Button button = new Button();
            button.Text = $"Tickets";
            button.Top = 100;
            button.Left = 10;
            button.Click += (sender, e) => MessageBox.Show($"Button {index + 1} clicked!");
            panel.Controls.Add(button);

            // Add the block panel to the scrollable panel
            parentPanel.Controls.Add(panel);

            // Return the height of this block to update yOffset
            return panel.Height + 10; // 10 pixels for spacing between blocks
        }
    }
}
