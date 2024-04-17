using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggingKata 
{
    public class TacoBell : ITrackable
    {   // made everything conform to ITrackable, gave values because its inheriting from an interface.
        public TacoBell(string name, Point location) 
        {
            Name = Name;
            Location = Location;
        }
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}
