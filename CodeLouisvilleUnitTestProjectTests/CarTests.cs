using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class CarTests
    {
      
        public ITestOutputHelper _logger;
        
        



        public CarTests(ITestOutputHelper testOutputHelper)
        {
            _logger = testOutputHelper;
           

    }

        [Fact]
        public void CarParameterlessConstructorTest()
        {
            //arrange
            Car car = new Car();
            //act

            //assert
            using  (new AssertionScope())
            {
                car.NumberOfTires.Should().Be(4);
                car.Should().BeOfType<Car>();
                typeof(Car).IsSubclassOf(typeof(Vehicle));
            };
        }

        [Fact]
        public void CarConstructorTest()
        {
            //arrange
            Car car = new Car(20, "Honda", "Civic", 21);
            //act

            //assert
            using (new AssertionScope())
            {
                car.NumberOfTires.Should().Be(4);
                car.Should().BeOfType<Car>();
                typeof(Car).IsSubclassOf(typeof(Vehicle));
            };
        }

        [Fact]
        public async void IsValidModelForMakeAsyncPositive()
        {
            //arrange
            Car car = new Car(20, "Honda", "Civic", 21);
            //act
            bool validModel = await car.IsValidModelForMakeAsync();
            //assert
            validModel.Should().Be(true);
        }

        [Fact]
        public async void IsValidMakeForMakeNegative()
        {
            //arrange
            Car car = new Car(20, "Honda", "Camry", 21);
            //act
            bool validModel = await car.IsValidModelForMakeAsync();
            //assert
            validModel.Should().Be(false);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(1600)]
        [InlineData(1994)]
        public async void WasModelMadeInYearAsyncNegative(int year)
        {
            //arrange
            Car car = new Car();

            //act
            Func<Task> validYear = async () => { await car.WasModelMadeInYearAsync(year); };
            //assert
            await validYear.Should().ThrowAsync<ArgumentException>();   
            
        }

        [Theory]
        [InlineData("Trents","Pants", 2030, false)]
        [InlineData("Honda", "Camry", 1996, false)]
        [InlineData("Subaru", "WRX", 2020, true)]
        [InlineData("Subaru", "WRX", 2000, false)]
        public async void WasModelMadeInYearAsyncPositive(string make, string model, int year, bool result)
        {
            //arrange
            Car car = new Car(20, make, model, 21);

            //act
            bool validYear = await car.WasModelMadeInYearAsync(year);

            //assert

            validYear.Should().Be(result);

        }

        [Theory]
        [InlineData(0)]
        [InlineData(2)]
        [InlineData(15)]
        public void AddPassengersMpg(int passengersBeingManipulated)
        {
            //arrange
            Car car = new Car(20, "Honda", "Civic", 21);
            double originalMpg = car.MilesPerGallon;
            double mpgEffects = .2 * passengersBeingManipulated;
            //act
            car.AddPassengers(passengersBeingManipulated);
            double mpgAfterAddingPassengers = car.MilesPerGallon;
            car.RemovePassengers(passengersBeingManipulated);
            double mpgAfterRemovingPassengers = car.MilesPerGallon;

            //assert
            using (new AssertionScope())
            {
                mpgAfterAddingPassengers.Should().Be(originalMpg - mpgEffects);
                mpgAfterRemovingPassengers.Should().Be(originalMpg);
            }
      
        }

        [Theory]
        [InlineData(3, 20.6)]
        [InlineData(5, 21)]
        [InlineData(25, 21)]

        public void RemovePassengers(int passengersToRemove, double milesPerGallon)
        {
            //arrnage
            Car car = new Car(20, "Honda", "Civic", 21);
            //act
            car.AddPassengers(5);
            car.RemovePassengers(passengersToRemove);   
            //assert
            car.MilesPerGallon.Should().Be(milesPerGallon);
        }

    }
}
