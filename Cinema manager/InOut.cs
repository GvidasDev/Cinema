using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cinema_manager
{
    internal class InOut
    {
        public static List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>();
            string filepath = "..\\..\\Data files\\movies.json";
            try
            {
                string jsonString = File.ReadAllText(filepath);
                movies = JsonConvert.DeserializeObject<List<Movie>>(jsonString);

            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
            return movies;
        }

        public static List<Schedule> GetSchedules(List<Movie> movies)
        {
            List<Schedule> schedules = new List<Schedule>();
            string filePath = "..\\..\\Data files\\schedule.json";
            try
            {
                string schedulesJsonString = File.ReadAllText(filePath);
                List<ScheduleDto> scheduleDtos = JsonConvert.DeserializeObject<List<ScheduleDto>>(schedulesJsonString);

                schedules = new List<Schedule>();
                foreach (var dto in scheduleDtos)
                {
                    var movie = movies.Find(m => m.Id == dto.MovieId);
                    if (movie != null)
                    {
                        schedules.Add(new Schedule(movie, dto.StartTime, dto.EndTime));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error: {e.Message}");
            }
            return schedules;
        }
    }
}
