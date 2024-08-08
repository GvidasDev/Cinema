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
    }
}
