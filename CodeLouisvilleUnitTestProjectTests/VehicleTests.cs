using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class VehicleTests
    {

        public ITestOutputHelper _logger;
        
        public VehicleTests(ITestOutputHelper testOutputHelper)
        {
            _logger = testOutputHelper;
            
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
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "Civic", 30);
            //act
            vehicle.GasLevel.Should().Be("0%");
            vehicle.AddGas();
            //assert
            vehicle.GasLevel.Should().Be("100%");
        }

        //Verify that the AddGas method with a parameter adds the
        //supplied amount of gas to the gas tank.
        [Fact]
        public void AddGasWithParameterAddsSuppliedAmountOfGas()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "Civic", 30);
            //act
            vehicle.GasLevel.Should().Be("0%");
            vehicle.AddGas(5);
            //assert
            vehicle.GasLevel.Should().Be("50%");

        }

        //Verify that the AddGas method with a parameter will throw
        //a GasOverfillException if too much gas is added to the tank.
        [Fact]
        public void AddingTooMuchGasThrowsGasOverflowException()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "Civic", 30);
            //act
            vehicle.GasLevel.Should().Be("0%");         
            Action testAddGas = () => vehicle.AddGas(12);
            //assert
            testAddGas.Should().Throw<GasOverfillException>();
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
         *      string “Cannot drive, out of gas.”.
         *      b. Attempting to drive a car with a flat tire returns 
         *      the status string “Cannot drive due to flat tire.”.
         *      c. Drive the car 10 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled, 
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      d. Drive the car 100 miles. Verify that the correct amount 
         *      of gas was used, that the correct distance was traveled,
         *      that GasLevel is correct, that MilesRemaining is correct, 
         *      and that the total mileage on the vehicle is correct.
         *      e. Drive the car until it runs out of gas. Verify that the 
         *      correct amount of gas was used, the amount of gas left
         *      is correct, and that the total mileage on the vehicle is 
         *      correct. Verify that the status reports the car is out of gas.
        */
        [Fact]
        public void DriveWithoutGas()
        {
            //Arrange
            Vehicle vehicle = new Vehicle();
            //Act
            String driveWithoutGas = vehicle.Drive(10);
            //Assert
            driveWithoutGas.Should().Be("Cannot drive, out of gas.");
        }

        [Fact]
        public void DriveWithFlatTire()
        {
            //Arrange
            Vehicle vehicle = new Vehicle(1000, 100, "Honda", "Civic", 30);
            //Act
            vehicle.AddGas();
            vehicle.Drive(100);
            String driveWithFlatTire = vehicle.Drive(100);
            //Assert
            driveWithFlatTire.Should().Be("Cannot drive due to flat tire.");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(100)]
        [InlineData(400)]
        public void DrivingSpecTest(int distanceDrivenInput)
        {
            // log scenario being tested 
            _logger.WriteLine($"Case tested: Vehicle drives {distanceDrivenInput} miles");
            //Arrange
            Vehicle vehicle = new Vehicle(4, 10, "Honda", "Civic", 30);
            double distanceDriven = distanceDrivenInput;
            var beginningVehicleMileage = vehicle.Mileage;
            var totalMilesInTank = vehicle.MilesPerGallon * vehicle.GasTankCapacity;
            double gasLeftPercent = (totalMilesInTank - distanceDriven) / totalMilesInTank * 100;
            
            var gasUsed = Math.Round(distanceDriven / vehicle.MilesPerGallon, 2);
            //Act
            vehicle.AddGas();
            var currentGasLevelPercent = Math.Round(double.Parse(vehicle.GasLevel.Replace("%", "")), 2);
            // assertions to make sure the vehicle is instantiated correctly
            using (new AssertionScope())
            {
                beginningVehicleMileage.Should().Be(0);
                currentGasLevelPercent.Should().Be(100);
            }
            
            String driveStatus = vehicle.Drive(distanceDriven);   
            
            /*
            while (driveStatus.Contains("Oh no! Got a flat tire!"))
            {
                vehicle._hasFlatTire = false;   
                driveStatus = vehicle.Drive(distanceDriven);
            }
            */

            var totalVehicleMileage = vehicle.Mileage;
            currentGasLevelPercent = Math.Round(double.Parse(vehicle.GasLevel.Replace("%", "")), 2);
            var milesRemaining = Math.Round(vehicle.MilesRemaining, 2);
            //Assert
            if (distanceDriven >= totalMilesInTank)
            {
                using (new AssertionScope())
                {
                    _logger.WriteLine($"{driveStatus}");
                    _logger.WriteLine($"Vehicle mileage is: {totalVehicleMileage} miles, amount of gas used was: {vehicle.GasTankCapacity} gallons and current gas level is: {currentGasLevelPercent} percent."); 
                    // total mileage on vehicle is correct
                    totalVehicleMileage.Should().Be(beginningVehicleMileage + totalMilesInTank);
                    // total distance driven and gas used
                    driveStatus.Should().Be($"Drove {Math.Round(totalMilesInTank, 2)} miles, then ran out of gas.");
                    // gas level remaining
                    currentGasLevelPercent.Should().Be(0);
                    // miles remaining
                    milesRemaining.Should().Be(0);                    
                }                
            }
            else
            {
                using (new AssertionScope())
                {
                    _logger.WriteLine($"{driveStatus}");
                    _logger.WriteLine($"Vehicle mileage is: {totalVehicleMileage} miles, amount of gas used was: {gasUsed} gallons and current gas level is: {currentGasLevelPercent} percent.");
                    // total mileage on vehicle is correct
                    totalVehicleMileage.Should().Be(distanceDriven + beginningVehicleMileage);
                    // total distance driven and gas used
                    driveStatus.Should().Be($"Drove {Math.Round(distanceDriven, 2)} miles using {Math.Round(gasUsed, 2)} gallons of gas.");
                    // gas level remaining
                    currentGasLevelPercent.Should().Be(Math.Round(gasLeftPercent, 2));
                    // miles remaining
                    milesRemaining.Should().Be(Math.Round((totalMilesInTank - distanceDriven), 2));                  
                }
            }
            
        }

        //Verify that attempting to change a flat tire using
        //ChangeTireAsync will throw a NoTireToChangeException
        //if there is no flat tire.
        [Fact]
        public async Task ChangeTireWithoutFlatTest()
        {
            //Arrange
            Vehicle vehicle = new Vehicle(4, 100, "Honda", "Civic", 30);
            //Act
            Func<Task> changeTire = async () => { await vehicle.ChangeTireAsync(); };
            //Assert
            await changeTire.Should().ThrowAsync<NoTireToChangeException>();
            
        }

        //Verify that ChangeTireAsync can successfully
        //be used to change a flat tire
        [Fact]
        public async Task ChangeTireSuccessfulTest()
        {
            //arrange
            Vehicle vehicle = new Vehicle(4, 100, "Honda", "Civic", 30);
            //act
            vehicle._hasFlatTire = true;
            await vehicle.ChangeTireAsync();
            //assert
            vehicle._hasFlatTire.Should().Be(false);
            
        }

        //BONUS: Write a unit test that verifies that a flat
        //tire will occur after a certain number of miles.
        [Theory]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(200)]
        public void GetFlatTireAfterCertainNumberOfMilesTest(int milesDriven)
        {
            //arrange
            
            //act

            //assert

        }
    }
}