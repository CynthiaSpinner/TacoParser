namespace LoggingKata 
{
    public class TacoBell : ITrackable
    {   // made everything conform to ITrackable, gave values because its inheriting from an interface.
        public TacoBell(string name, Point location) 
        {
            Name = name;
            Location = location;
        }
        public string Name { get; set; }
        public Point Location { get; set; }
    }
}
