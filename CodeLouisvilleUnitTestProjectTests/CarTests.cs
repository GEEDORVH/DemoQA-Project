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



    }
}
