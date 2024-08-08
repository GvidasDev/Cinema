using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_manager
{
    public class ScheduleDto
    {
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    internal class Schedule
    {
        public Movie Movie { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        Theater theater;

        public Schedule()
        {
            Movie = new Movie();
            StartTime = DateTime.Now;
            EndTime = DateTime.Now;
            theater = new Theater();
        }

        public Schedule(Movie movie, DateTime startTime, DateTime endTime)
        {
            this.Movie = movie;
            this.StartTime = startTime;
            this.EndTime = endTime;
            theater = new Theater(9, 9);
        }

        public override string ToString()
        {
            return string.Format($"Movie: {Movie.Title}, Start time: {StartTime}, End time: {EndTime}");
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
