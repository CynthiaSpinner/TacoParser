using System;
using Xunit;

namespace LoggingKata.Test
{
    public class TacoParserTests
    {
        [Fact]
        public void ShouldReturnNonNullObject()
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse("34.073638, -84.677017, Taco Bell Acwort...");

            //Assert
            Assert.NotNull(actual);

        }

        [Theory]
        [InlineData("34.073638, -84.677017, Taco Bell Acwort...", -84.677017)]
        [InlineData("33.470013,-86.816966,Taco Bell Birmingham...", -86.816966)]
        
        public void ShouldParseLongitude(string line, double expected)
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse(line);// we want to do a instance method call but, we want to specify the location called line, within
                                                // the perameters of the parse method.
            //Assert
            Assert.Equal(expected, actual.Location.Longitude);// because the parse method is returning tacoBell,
                                                              // we want to specified what part of the return we are comparing to our expected.
            
        }


       
        [Theory]
        [InlineData("33.671982,-85.826674,Taco Bell Annisto...", 33.671982)]
        [InlineData("32.555148,-84.946447,Taco Bell Columbus/1...", 32.555148)]

        public void ShouldParseLatitude(string line, double expected) 
        {
            //Arrange
            var tacoParser = new TacoParser();

            //Act
            var actual = tacoParser.Parse(line);

            //Assert
            Assert.Equal(expected, actual.Location.Latitude);
        }

        [Theory]
        [InlineData(900, 0.5592340730136005)]
        [InlineData(567, 0.35231746599856834)]

        public void CovertDistanceTest(double distance,  double expected)
        {
            //static

            //Act
            var actual = ConvertDistance.ConvertMetersToMiles(distance);

            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
