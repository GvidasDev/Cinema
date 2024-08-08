using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_manager
{
    internal class Schedule
    {
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int AvailableSeats { get; set; }

        Theater theater;

        public Schedule()
        {
            Movie = new Movie();
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            AvailableSeats = 0;
            theater = new Theater();
        }

        public Schedule(Movie movie, DateTime startTime, DateTime endTime, int availableSeats)
        {
            this.Movie = movie;
            this.StartTime = startTime;
            this.EndTime = endTime;
            this.AvailableSeats = availableSeats;
            theater = new Theater(9, 9);
        }

        public override string ToString()
        {
            return string.Format($"Movie: {Movie.Title}, Start time: {StartTime}, End time: {EndTime}, Available seats: {AvailableSeats}");
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode(); 
        }
    }
}
