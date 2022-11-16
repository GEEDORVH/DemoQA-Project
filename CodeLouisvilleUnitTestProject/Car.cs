using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;
using static System.Net.WebRequestMethods;

namespace CodeLouisvilleUnitTestProject
{
    public class Car : Vehicle
    {
        private ITestOutputHelper _logger;

        private HttpClient _httpClient = new HttpClient()
        {
            BaseAddress = new Uri("https://vpic.nhtsa.dot.gov/api/") 
        };
        
        public new int NumberOfTires = 4;
        
        public int NumberOfPassengers { get; private set; }

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

        public async Task<bool> IsValidModelForMakeAsync()
        {
            bool isValidForMake = false;
            try
            {
                var getUrl = $"{_httpClient.BaseAddress}/vehicles/GetModelsForMake/{this.Make}?format=json";
                var response = await _httpClient.GetAsync(getUrl);
                var respMessage = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                MakeModelSpecsRoot makeModelSpecs = JsonConvert.DeserializeObject<MakeModelSpecsRoot>(respMessage);

                for (int i =0; i < makeModelSpecs.Results.Count; i++)
                {
                    if (makeModelSpecs.Results[i].ModelName == this.Model)
                    {
                        isValidForMake = true;
                    }
                } 
            }
            catch (Exception ex)
            {
                _logger.WriteLine("Failed when calling API to get MakeModel specs", ex);
                return false;
            }
            return isValidForMake;
            
        }

        public async Task<bool> WasModelMadeInYearAsync( int year)
        {
            bool wasModelMadeInYear = false;
            if (year < 1995)
            {
                throw new ArgumentException("No data available for vehicles before year 1995");
            }
            try
            {
                var getUrl = $"{_httpClient.BaseAddress}/vehicles/GetModelsForMakeYear/make/{this.Make}/modelyear/{year}?format=json";
                var response = await _httpClient.GetAsync(getUrl);
                var respMessage = await response.EnsureSuccessStatusCode().Content.ReadAsStringAsync();

                MakeModelSpecsRoot makeModelSpecs = JsonConvert.DeserializeObject<MakeModelSpecsRoot>(respMessage);
                for (int i = 0; i < makeModelSpecs.Count; i++)
                {
                    if (makeModelSpecs.Results[i].ModelName == this.Model)
                    {
                        wasModelMadeInYear = true;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.WriteLine("Failed when calling API to get MakeModel specs by Make and Year", ex);
                return false;
            }
            return wasModelMadeInYear;
        }
            
        public void AddPassengers(int numberOfPassengersToAdd)
        {
            NumberOfPassengers = NumberOfPassengers = numberOfPassengersToAdd;            
            MilesPerGallon = MilesPerGallon - (numberOfPassengersToAdd * .2);
            if (MilesPerGallon < 0)
            {
                MilesPerGallon = 0;
            }
        }

        public void RemovePassengers(int numberOfPassengersToRemove)
        {
            if (numberOfPassengersToRemove > NumberOfPassengers)
            {
                numberOfPassengersToRemove = NumberOfPassengers;
            }            
            NumberOfPassengers = NumberOfPassengers + numberOfPassengersToRemove;
            MilesPerGallon = MilesPerGallon + (numberOfPassengersToRemove * .2);
        }   
    }
}
