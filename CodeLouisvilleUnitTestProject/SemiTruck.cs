using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace CodeLouisvilleUnitTestProject
{
    public class SemiTruck : Vehicle
    {
        public List<CargoItem> Cargo { get; private set; }

        /// <summary>
        /// Creates a new SemiTruck that always has 18 Tires
        /// </summary>
        public SemiTruck()
        {
            //YOUR CODE HERE: 
            NumberOfTires = 18;
            Cargo = new List<CargoItem>();
        }

        /// <summary>
        /// Adds the passed CargoItem to the Cargo
        /// </summary>
        /// <param name="item">The CargoItem to add</param>
        public void LoadCargo(CargoItem item)
        {
            //YOUR CODE HERE
            Cargo.Add(item);
        }
            
        /// <summary>
        /// Attempts to remove the first item with the passed name from the Cargo and return it
        /// </summary>
        /// <param name="name">The name of the CargoItem to attempt to remove</param>
        /// <returns>The removed CargoItem</returns>
        /// <exception cref="ArgumentException">Thrown if no CargoItem in the Cargo matches the passed name</exception>
        public CargoItem UnloadCargo(string name)
        {
            CargoItem itemToRemove = new CargoItem();
            //YOUR CODE HERE
            for (int i = 0; i < Cargo.Count; i++)
            {
                int counter = 0;
                if (Cargo[i].Name == name && counter == 0)
                {
                    itemToRemove = Cargo[i];   
                    Cargo.Remove(itemToRemove);
                    counter++;
                }
            }
            if (itemToRemove.Name == null)
            {
                throw new ArgumentException();
            }
            else
            {
                return itemToRemove;
            }
        }

        /// <summary>
        /// Returns all CargoItems with the exact name passed. If no CargoItems have that name, returns an empty List.
        /// </summary>
        /// <param name="name">The name to match</param>
        /// <returns>A List of CargoItems with the exact name passed</returns>
        public List<CargoItem> GetCargoItemsByName(string name)
        {
            List<CargoItem> matchingItems  = new List<CargoItem>();
            //YOUR CODE HERE
            for (int i = 0; i < Cargo.Count; i++)
            {
                if (Cargo[i].Name == name)
                {
                    CargoItem matchingItem = Cargo[i];
                    matchingItems.Add(matchingItem);
                    
                }
            }
            return matchingItems;
        }

        /// <summary>
        ///  Returns all CargoItems who have a description containing the passed description. If no CargoItems have that name, returns an empty list.
        /// </summary>
        /// <param name="description">The partial description to match</param>
        /// <returns>A List of CargoItems with a description containing the passed description</returns>
        public List<CargoItem> GetCargoItemsByPartialDescription(string description)
        {
            List<CargoItem> matchingItemsByDescription = new List<CargoItem>();
            //YOUR CODE HERE
            for (int i = 0; i < Cargo.Count; i++)
            {
                if (Cargo[i].Description.Contains(description)) 
                {
                    CargoItem matchingItemByDescription = Cargo[i];
                    matchingItemsByDescription.Add(matchingItemByDescription);

                }
            }
            return matchingItemsByDescription;
        }

        /// <summary>
        /// Get the number of total items in the Cargo.
        /// </summary>
        /// <returns>An integer representing the sum of all Quantity properties on all CargoItems</returns>
        public int GetTotalNumberOfItems()
        {
            int totalNumberOfItems = 0;
            //YOUR CODE HERE
            for (int i = 0; i < Cargo.Count; i++)
            {
                totalNumberOfItems += Cargo[i].Quantity;
            }
            return totalNumberOfItems;
        }
    }
}
