namespace LoggingKata
{
    //interface for parsing location data from strings
    //this follows the dependency inversion principle - depend on abstractions, not concretions
    public interface ILocationParser
    {
        ITrackable Parse(string line);
    }
}

