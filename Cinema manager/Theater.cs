using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_manager
{
    class Theater
    {
        public int TotalRows { get; set; }
        public int TotalCols { get; set; }
        public List<Seat> Seats { get; set; }

        public Theater()
        {
            this.TotalRows = 0;
            this.TotalCols = 0;
            Seats = new List<Seat>();
        }

        public Theater(int totalRows, int totalCols)
        { 
            this.TotalRows = totalRows;
            this.TotalCols = totalCols;
            Seats = createSeats(totalRows, totalCols);
        }    

        public Theater(int totalRows, int totalCols, List<Seat> seats)
        {
            this.TotalRows = totalRows;
            this.TotalCols = totalCols;
            this.Seats = seats;
        }

        static List<Seat> createSeats(int rows, int cols)
        {
            List<Seat> seats = new List<Seat>();
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 1; j <= cols; j++)
                {
                    seats.Add(new Seat(8, i, j));
                }
            }
            return seats;
        }

        public Seat getSeat(int row, int col)
        {
            return Seats.FirstOrDefault(s => s.Row == row && s.Column == col);
        }

        public void setSeat(Seat seat, int row, int col)
        {
            int index = Seats.FindIndex(s => s.Row == row && s.Column == col);
            if (index != -1)
            {
                Seats[index] = seat;
            }
        }


        public string Output()
        {
            string line = "";
            foreach (Seat seat in Seats)
            {
                line += seat.ToString() + "\n";
            }
            return line;
        }
    }
}
