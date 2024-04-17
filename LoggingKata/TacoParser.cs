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
            //logger.LogInfo("Begin parsing"); commented out to remove the console writeline 237 times.

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
            
            //made instance of my tacobell struct, more readable to use dot.notation to set properties because location has 2 properties inside it.
            var tacoBell = new TacoBell(name, location);

            //created my object by giving values
                tacoBell.Location = location; // had to set value to "location" .
                tacoBell.Name = name; // set value
            
          
            //returned tacobell because it conforms to ITrackable.
            return tacoBell;
        }
    }
}
