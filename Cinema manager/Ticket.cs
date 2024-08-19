using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_manager
{
    internal class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Seat Seat { get; set; }

        public Ticket() { }

        public Ticket(int id, string title, Seat seat)
        {
            this.Id = id;
            this.Title = title;
            this.Seat = seat;
        }
    }
}
