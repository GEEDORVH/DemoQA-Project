using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace CodeLouisvilleUnitTestProject
{
    public class Car : Vehicle
    {
        private HttpClient httpClient = new HttpClient()
        {
            BaseAddress = new Uri(" https://vpic.nhtsa.dot.gov/api/") 
        };

        public new int NumberOfTires = 4;
        
        public int numberOfPassengers { get; private set; }

        public Car()
            : this(0, "", "", 0)
        {
                      
        }
        public Car(double gasTankCapacity, string make, string model, double milesPerGallon)
        {
            GasTankCapacity = gasTankCapacity;
            Make = make;
            Model = model;
            MilesPerGallon = milesPerGallon;          
        }
    }
}
