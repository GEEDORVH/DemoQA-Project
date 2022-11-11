using CodeLouisvilleUnitTestProject;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class SemiTruckTests
    {
        public ITestOutputHelper _logger;

        public SemiTruckTests(ITestOutputHelper testOutputHelper)
        {
            _logger = testOutputHelper;
        }
        //Verify that the SemiTruck constructor creates a new SemiTruck
        //object which is also a Vehicle and has 18 wheels. Verify that the
        //Cargo property for the newly created SemiTruck is a List of
        //CargoItems which is empty, but not null.
        [Fact]
        public void NewSemiTruckIsAVehicleAndHas18TiresAndEmptyCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            //act

            //assert
            using (new AssertionScope())
            {
                semiTruck.NumberOfTires.Should().Be(18);
                semiTruck.Cargo.Should().BeOfType<List<CargoItem>>();
                semiTruck.Cargo.Should().BeEmpty();
                semiTruck.Cargo.Should().NotBeNull();
            };
        }

        //Verify that adding a CargoItem using LoadCargo does successfully add
        //that CargoItem to the Cargo. Confirm both the existence of the new
        //CargoItem in the Cargo and also that the count of Cargo increased to 1.
        [Fact]
        public void LoadCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem();
            //act
            semiTruck.Cargo.Count.Should().Be(0);
            semiTruck.LoadCargo(cargoItem);
            //assert
            using (new AssertionScope())
            {
                semiTruck.Cargo.Should().Contain(cargoItem);
                semiTruck.Cargo.Count.Should().Be(1);
            }

        }

        //Verify that unloading a  cargo item that is in the Cargo does
        //remove it from the Cargo and return the matching CargoItem
        [Fact]
        public void UnloadCargoWithValidCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem();
            cargoItem.Name = "ItemToRemove";
            //act
            semiTruck.Cargo.Count.Should().Be(0);
            semiTruck.LoadCargo(cargoItem);
            using (new AssertionScope())
            {
                semiTruck.Cargo.Should().Contain(cargoItem);
                semiTruck.Cargo.Count.Should().Be(1);
            }
            CargoItem removedItem = semiTruck.UnloadCargo("ItemToRemove");

            //assert
            using (new AssertionScope())
            {
                semiTruck.Cargo.Count.Should().Be(0);
                semiTruck.Cargo.Should().NotContain(cargoItem);
                removedItem.Should().Be(cargoItem);
            }

        }

        //Verify that attempting to unload a CargoItem that does not
        //appear in the Cargo throws a System.ArgumentException
        [Fact]
        public void UnloadCargoWithInvalidCargoTest()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();

            //act

            //assert
            Assert.Throws<ArgumentException>(() => semiTruck.UnloadCargo("ItemNotInCargo"));

        }

        //Verify that getting cargo items by name returns all items
        //in Cargo with that name.
        [Theory]
        [InlineData("Item1", 0)]
        [InlineData("Item2", 2)]
        [InlineData("Item3", 100)]
        public void GetCargoItemsByNameWithValidName(string nameOfItemToRemove, int itemCountWithSameNameInCargoList)
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem();
            cargoItem.Name = nameOfItemToRemove;
            
            for (int i = 0; i < itemCountWithSameNameInCargoList; i++)
            {
                semiTruck.LoadCargo(cargoItem);
            }
            
            //act
            
            List<CargoItem> cargoItemsRemoved = semiTruck.GetCargoItemsByName(nameOfItemToRemove);
            //assert
            using (new AssertionScope())
            {
                cargoItemsRemoved.Should().HaveCount(itemCountWithSameNameInCargoList);
                for (int i = 0; i < cargoItemsRemoved.Count; i++)
                {
                    cargoItemsRemoved[i].Name.Should().Be(nameOfItemToRemove);
                }
            }
            
        }

        //Verify that searching the Carto list for an item that does not
        //exist returns an empty list
        [Fact]
        public void GetCargoItemsByNameWithInvalidName()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            //act
            List<CargoItem> cargoItemListShouldBeEmpty = semiTruck.GetCargoItemsByName("itemNotInCargo");
            //assert
            cargoItemListShouldBeEmpty.Should().BeEmpty();
        }

        //Verify that searching the Cargo list by description for an item
        //that does exist returns all matched items that contain that description.
        [Theory]
        [InlineData("custom description", 0)]
        [InlineData("four score", 2)]
        [InlineData("seven years ago", 100)]
        public void GetCargoItemsByPartialDescriptionWithValidDescription(string description, int itemsInListMatchingCount)
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem();
            CargoItem cargoItemUnrelated = new CargoItem(); 
            cargoItem.Description = "Stuff before to test contains " + description + " stuff after to test contains";
           
            //act
            semiTruck.LoadCargo(cargoItemUnrelated);
            for (int i = 0; i < itemsInListMatchingCount; i++)
            {
                semiTruck.LoadCargo(cargoItem);
            }
            List<CargoItem> cargoItemsRemovedByDescription = semiTruck.GetCargoItemsByPartialDescription(description);
            //assert

            using (new AssertionScope())
            {
                cargoItemsRemovedByDescription.Should().HaveCount(itemsInListMatchingCount);
                for (int i = 0; i < cargoItemsRemovedByDescription.Count; i++)
                {
                    cargoItemsRemovedByDescription[i].Description.Should().Contain(description);
                }

            }

        }

        //Verify that searching the Carto list by description for an item
        //that does not exist returns an empty list
        [Fact]
        public void GetCargoItemsByPartialDescriptionWithInvalidDescription()
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            //act
            List<CargoItem> cargoItems = semiTruck.GetCargoItemsByPartialDescription("These do not exist");
            //assert
            cargoItems.Should().BeEmpty();
        }

        //Verify that the method returns the sum of all quantities of all
        //items in the Cargo
        [Theory]
        [InlineData(0, 0)]
        [InlineData(2, 10)]
        [InlineData(100, 3)]
        public void GetTotalNumberOfItemsReturnsSumOfAllQuantities(int quantity, int numberOfItems)
        {
            //arrange
            SemiTruck semiTruck = new SemiTruck();
            CargoItem cargoItem = new CargoItem();
            cargoItem.Quantity = quantity;
            for (int i = 0; i < numberOfItems; i++)
            {
                semiTruck.LoadCargo(cargoItem);
            }
            //act
            int expectedTotalQuantity = quantity * numberOfItems;
            int actualTotalQuantity = semiTruck.GetTotalNumberOfItems();
            //assert
            actualTotalQuantity.Should().Be(expectedTotalQuantity);
        }
    }
}
