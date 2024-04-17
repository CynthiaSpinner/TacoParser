namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            //the Split method returns ;substrings of an array.
            var cells = line.Split(',');


            //****NULL CHECK****
            // If your array's Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log error message and return null
                return null; 
            }
            //****Started here*****
            
            //grabbed index 0 latitude from the array called cells and parsed to double.
            double latitude = double.Parse(cells[0]);

            //grabbed index 1 logitude from the array called cells and parsed to double.
            double longitude = double.Parse(cells[1]);
           
            // grabbed the name from my array and it was at index 2
            string name = cells[2];
            


           //created tacobell class.
           //***** go to taco bell class****



            //made instance of my Point class and set values to logitude and latitude
            var location = new Point

            {
                Longitude = longitude,
                Latitude = latitude,
            };
            
            //made instance of my tacobell class, needed to use dot.notation and pass parameters.
            var tacoBell = new TacoBell(name, location);


                tacoBell.Location = location; // had to set value to "point" because I had made a new instance called point.
                tacoBell.Name = name;
            
          
            //returned tacobell because it conforms to ITrackable.
            return tacoBell;
        }
    }
}
