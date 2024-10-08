﻿using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_manager
{
    internal class Ticket
    {
        private static int _idCounter = 0;
        public int Id { get; set; }
        public string Title { get; set; }
        public Seat Seat { get; set; }

        public Ticket() { }

        public Ticket(string title, Seat seat)
        {
            this.Id = ++_idCounter;
            this.Title = title;
            this.Seat = seat;
        }

        public Ticket(int id, string title, Seat seat)
        {
            this.Id = id;
            this.Title = title;
            this.Seat = seat;
        }

        public override string ToString()
        {
            return string.Format($"{Id} {Title} {Seat.ToString()} \n");
        }
    }
}
