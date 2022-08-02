namespace CodeLouisvilleUnitTestProject
{
    public class Vehicle
    {
        #region Public Properties
        public string Make { get; }
        public string Model { get; }
        public double MilesPerGallon { get; set; }
        public double GasTankCapacity { get; init; }
        public int NumberOfTires { get; init; }
        public string GasLevel => $"{_gasRemaining / GasTankCapacity * 100}%";
        public double MilesRemaining => _gasRemaining * MilesPerGallon;
        public double Mileage => _mileage;
        #endregion

        #region Private Fields
        private double _gasRemaining;
        private double _mileage;
        private bool _hasFlatTire;
        #endregion

        #region Private Properties
        private double _flatTireChance => 0.0001 * NumberOfTires;
        #endregion

        public Vehicle()
            : this(0, 0, "", "", 0)
        { }

        public Vehicle(int numberOfTires, double gasTankCapacity, string make, string model, double milesPerGallon)
        {
            NumberOfTires = numberOfTires;
            GasTankCapacity = gasTankCapacity;
            Make = make;
            Model = model;
            MilesPerGallon = milesPerGallon;
        }

        /// <summary>
        /// Adds gas up to the maximum amount of gas the tank can hold
        /// </summary>
        /// <returns></returns>
        public double AddGas()
        {
            _gasRemaining = GasTankCapacity;
            return _gasRemaining;
        }

        /// <summary>
        /// Adds a specific amount of gas
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        /// <exception cref="GasOverfillException"> Thrown if the amount of gas added exceeds the capacity of the tank</exception>
        public double AddGas(float amount)
        {
            double newTotal = _gasRemaining + amount;
            if (newTotal > GasTankCapacity)
                throw new GasOverfillException(amount, GasTankCapacity);
            else 
                _gasRemaining = newTotal;
            return _gasRemaining;
        }

        public string Drive(double miles)
        {
            bool ableToDrive = false;
            string statusString;
            if (MilesRemaining == 0)
            {
                statusString = "Cannot drive, out of gas.";
            }
            else if (_hasFlatTire)
            {
                statusString = "Cannot drive due to flat tire.";
            }
            else
            {
                ableToDrive = true;
                if (MilesRemaining > miles)
                {
                    double gasUsed = miles / MilesPerGallon;
                    _gasRemaining -= gasUsed;
                    _mileage += miles;
                    statusString = $"Drove {Math.Round(miles, 2)} miles using {Math.Round(gasUsed, 2)} gallons of gas.";
                }
                else
                {
                    double distanceTraveled = MilesRemaining;
                    _gasRemaining = 0;
                    _mileage += distanceTraveled;
                    statusString = $"Drove {Math.Round(distanceTraveled, 2)} miles, then ran out of gas.";
                }
            }

            if(ableToDrive)
            {
                bool gotFlat = GotFlatTire(miles);
                if(gotFlat)
                {
                    _hasFlatTire = true;
                    statusString += " Oh no! Got a flat tire!";
                }
            }
            return statusString;
        }

        protected async Task ChangeTireAsync()
        {
            if (!_hasFlatTire)
                throw new NoTireToChangeException();
            else
            {
                await Task.Delay(1000);
                _hasFlatTire = false;
            }
        }

        /// <summary>
        /// Determine whether or not a flat tire after driving milesDrive miles based on flat tire chance and randomness
        /// </summary>
        /// <param name="milesDriven"></param>
        /// <returns></returns>
        private bool GotFlatTire(double milesDriven, int rngSeed = 0)
        {
            double probabilityOfFlatPerMile = 1 - _flatTireChance;
            double probPerMile = Math.Pow(probabilityOfFlatPerMile, milesDriven);
            double probabilityOfFlatThisTrip = 1 - Math.Pow(probPerMile, NumberOfTires);

            if (rngSeed == 0)
                rngSeed = int.Parse(DateTime.Now.ToString("mmssffff"));
            Random randomNumberGenerator = new(rngSeed);
            double rand = randomNumberGenerator.NextDouble();
            return rand < probabilityOfFlatThisTrip;
        }
    }
}