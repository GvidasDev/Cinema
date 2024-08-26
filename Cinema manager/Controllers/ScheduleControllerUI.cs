using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema_manager {
    internal class ScheduleControllerUI {

        private Panel mainContainerPanel;
        private TheatreControllerUI theatreController;

        public ScheduleControllerUI() { }

        public ScheduleControllerUI(Panel mainContainerPanel, TheatreControllerUI theatreController) {
            this.mainContainerPanel = mainContainerPanel;
            this.theatreController = theatreController;
        }
        public void ShowSchedule(List<Schedule> scheduleList) {
            mainContainerPanel.Controls.Clear();

            Panel scrollablePanel = new Panel {
                AutoScroll = true,
                Dock = DockStyle.Fill
            };
            mainContainerPanel.Controls.Add(scrollablePanel);

            int totalHeight = 0;

            for (int i = 0; i < scheduleList.Count; i++) {
                totalHeight += CreateUIBlock(scrollablePanel, totalHeight, scheduleList[i], scheduleList);
            }

            scrollablePanel.AutoScrollMinSize = new Size(0, totalHeight);
        }

        private int CreateUIBlock(Panel parentPanel, int yOffset, Schedule schedule, List<Schedule> scheduleList) {
            Panel panel = new Panel {
                BorderStyle = BorderStyle.FixedSingle,
                Width = 550,
                Top = yOffset,
                Left = 10
            };

            Label label1 = new Label {
                Text = $"{schedule.Movie.Title}",
                Width = 100,
                Top = 10,
                Left = 10
            };
            panel.Controls.Add(label1);

            Label label2 = new Label {
                Text = $"{schedule.StartTime}",
                Width = 100,
                Top = 10,
                Left = 110
            };
            panel.Controls.Add(label2);

            Label label3 = new Label {
                Text = $"{schedule.EndTime}",
                Width = 100,
                Top = 10,
                Left = 210
            };
            panel.Controls.Add(label3);

            double totalSeats = schedule.GetTheater().getTotalSeats();
            double perc = totalSeats > 0 ? (schedule.GetTheater().getTotalTaken() * 100) / totalSeats : 0;

            Label label4 = new Label {
                Text = $"{perc:F0}%",
                Width = 50,
                Top = 10,
                Left = 310
            };
            panel.Controls.Add(label4);

            Button button = new Button {
                Text = "Tickets",
                Width = 100,
                Top = 10,
                Left = 400
            };
            button.Click += (sender, e) => theatreController.ShowSeatSelection(schedule, scheduleList);
            panel.Controls.Add(button);

            parentPanel.Controls.Add(panel);

            return panel.Height + 10;
        }
    }
}
