using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema_manager
{
    internal class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public Movie()
        {
            Id = int.MinValue;
            Title = string.Empty;
            Description = string.Empty;
        }

        public Movie(int id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public override string ToString() 
        {
            return string.Format($"ID: {Id}, Title: {Title}, Description: {Description}"); 
        }

        public override int GetHashCode() { return Id; }
        public override bool Equals(object obj) { return Equals(obj as Movie); }

    }
}
