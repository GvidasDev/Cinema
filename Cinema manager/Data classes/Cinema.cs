using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_manager
{
    internal class Cinema
    {
        public List<Movie> Movies { get; set; } = new List<Movie>();
        public List<Schedule> Schedule { get; set; } = new List<Schedule>();

        public Cinema()
        { 
            Movies = new List<Movie>();
            Schedule = new List<Schedule>();
        }

        public Cinema(List<Movie> movies, List<Schedule> schedule)
        {
            Movies = movies;
            Schedule = schedule;
        }

        public string Output()
        {
            string line = "";
            for (int i = 0; i < Schedule.Count; i++)
            {
                line += Schedule[i].ToString() + "\n";
            }
            return line;
        }
    }
}
