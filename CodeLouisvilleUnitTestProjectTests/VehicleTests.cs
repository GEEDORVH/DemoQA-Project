using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class VehicleTests
    {
        public Vehicle _vehicleMaker;
        public ITestOutputHelper _logger;
        
        public VehicleTests(ITestOutputHelper testOutputHelper)
        {
            _logger = testOutputHelper;
            //_vehicleMaker = new Vehicle();
        }

        //Verify the parameterless constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to their default values.
        [Fact]
        public void VehicleParameterlessConstructorTest()
        {

            //arrange
            Vehicle vehicle = new Vehicle();
            
            //act

            //assert
            using (new AssertionScope())
            {
                vehicle.Make.Should().BeNullOrEmpty();
                vehicle.Model.Should().BeNullOrEmpty();
                vehicle.MilesPerGallon.Should().Be(0);
                vehicle.GasTankCapacity.Should().Be(0);
                vehicle.NumberOfTires.Should().Be(0);
                vehicle.GasLevel.Should().Be("NaN%");
                vehicle.MilesRemaining.Should().Be(0);
                vehicle.Mileage.Should().Be(0);
            };
            
        }

        //Verify the parameterized constructor successfully creates a new
        //object of type Vehicle, and instantiates all public properties
        //to the provided values.
        [Fact]
        public void VehicleConstructorTest()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "Civic", 30);

            //act

            //assert
            using (new AssertionScope())
            {
               
                vehicle.Make.Should().Be("Honda");
                vehicle.Model.Should().Be("Civic");
                vehicle.MilesPerGallon.Should().Be(30);
                vehicle.GasTankCapacity.Should().Be(10);
                vehicle.NumberOfTires.Should().Be(4);
                vehicle.GasLevel.Should().Be("0%");
                vehicle.MilesRemaining.Should().Be(0);
                vehicle.Mileage.Should().Be(0);
            };

        }

        //Verify that the parameterless AddGas method fills the gas tank
        //to 100% of its capacity
        [Fact]
        public void AddGasParameterlessFillsGasToMax()
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        //Verify that the AddGas method with a parameter adds the
        //supplied amount of gas to the gas tank.
        [Fact]
        public void AddGasWithParameterAddsSuppliedAmountOfGas()
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        //Verify that the AddGas method with a parameter will throw
        //a GasOverfillException if too much gas is added to the tank.
        [Fact]
        public void AddingTooMuchGasThrowsGasOverflowException()
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        //Using a Theory (or data-driven test), verify that the GasLevel
        //property returns the correct percentage when the gas level is
        //at 0%, 25%, 50%, 75%, and 100%.
        [Theory]
        [InlineData("0%", 0)]
        [InlineData("25%", 2.5)]
        [InlineData("50%", 5.0)]
        [InlineData("75%", 7.5)]
        [InlineData("100%", 10)]
        public void GasLevelPercentageIsCorrectForAmountOfGas(string percent, float gasToAdd)
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "Civic", 30);

            //act
            vehicle.AddGas(gasToAdd);

            //assert
            vehicle.GasLevel.Should().Be(percent);
        }

        /*
         * Using a Theory (or data-driven test), or a combination of several 
         * individual Fact tests, test the following functionality of the 
         * Drive method:
         *      a. Attempting to drive a car without gas returns the status 
         *      string �Cannot drive, out of gas.�.
         *      b. Attempting to drive a car with a flat tire returns 
         *      the status string �Cannot drive due to flat tire.�.
         *      c. Drive the car 10 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled, 
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      d. Drive the car 100 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled,
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      e. Drive the car until it runs out of gas. Verify that the 
         *      correct amount of gas was used, that the correct distance 
         *      was traveled, that GasLevel is correct, that MilesRemaining
         *      is correct, and that the total mileage on the vehicle is 
         *      correct. Verify that the status reports the car is out of gas.
        */
        [Theory]
        [InlineData("MysteryParamValue")]
        public void DriveNegativeTests(params object[] yourParamsHere)
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        [Theory]
        [InlineData("MysteryParamValue")]
        public void DrivePositiveTests(params object[] yourParamsHere)
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        //Verify that attempting to change a flat tire using
        //ChangeTireAsync will throw a NoTireToChangeException
        //if there is no flat tire.
        [Fact]
        public async Task ChangeTireWithoutFlatTest()
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        //Verify that ChangeTireAsync can successfully
        //be used to change a flat tire
        [Fact]
        public async Task ChangeTireSuccessfulTest()
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }

        //BONUS: Write a unit test that verifies that a flat
        //tire will occur after a certain number of miles.
        [Theory]
        [InlineData("MysteryParamValue")]
        public void GetFlatTireAfterCertainNumberOfMilesTest(params object[] yourParamsHere)
        {
            //arrange
            throw new NotImplementedException();
            //act

            //assert

        }
    }
}