using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Cinema_manager
{
    internal class Seat
    {
        public double Price { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Status SeatStatus { get; set; }

        public enum Status{
            FREE,
            SELECTED,
            TAKEN
        }

        public Seat()
        { 
            Price = 0;
            Row = 0;
            Column = 0;
            SeatStatus = Status.FREE;
        }
        public Seat(double price, int row, int column)
        {
            this.Price = price;
            this.Row = row;
            this.Column = column;
            this.SeatStatus = Status.FREE;
        }
        public Seat(double price, int row, int column, Status status)
        {
            this.Price = price;
            this.Row = row; 
            this.Column = column;
            this.SeatStatus = status;
        }

        public void SetFree()
        {
            SeatStatus = Status.FREE;
        }

        public void SetSelected()
        {
            SeatStatus = Status.SELECTED;
        }

        public void SetTaken()
        {
            SeatStatus = Status.TAKEN;
        }

        public bool IsFree()
        {
            if(SeatStatus == Status.FREE)
                return true;
            else return false;
        }

        public bool IsSelected()
        {
            if (SeatStatus == Status.SELECTED)
                return true;
            else return false;
        }

        public bool IsTaken()
        {
            if (SeatStatus == Status.TAKEN)
                return true;
            else return false;
        }

        public override string ToString() 
        {
            return string.Format($"Seat: row {Row}, column: {Column}, price: {Price}");
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
