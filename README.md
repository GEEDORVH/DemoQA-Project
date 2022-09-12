# QACourse1Project
Contains the sample code and base project files for the Code Louisville QA Course 1 final project

# Project Rubric
#1: Test Plan
Identify an online web application to test. You may choose a “real” website, such as https://www.amazon.com/, https://www.ebay.com, or https://www.yahoo.com/, or you may use a “sample” website such as https://www.demoblaze.com/ or https://demoqa.com/books. Include the name of your chosen website in your submission. 
Select one feature from this website and pretend that it was newly added to the application. Write a test plan for this new feature that includes at least 10 test cases. Indicate the type of each test case (critical path, regression, non-functional, etc) and include all of the necessary components of each test case (preconditions, steps, expected result, etc). 
Hint: refer to the 10-minute test plan supplemental reading if you need help deciding what to include.
#2: Manual Test Execution
Execute the test plan created in #1. Record the results of your tests as if you would be presenting them to the software quality manager for your team. Include at least one bug report in your results. The bug report should contain all necessary details a developer would need to begin work on fixing the defect. If you are unable to identify a legitimate defect in the feature you are testing, you may imagine that one of your test cases failed and proceed from there.
#3: Unit Test Creation
Check out the provided project. The code contains a single Visual Studio Solution which itself contains two projects: CodeLouisvilleUnitTestProject and CodeLouisvilleUnitTestProjectTests. The former contains the definition for two classes, Vehicle and SemiTruck. 
The Vehicle class has been implemented for you. You will need to add unit tests to it per the instructions below, refactoring where necessary.
The SemiTruck class has been stubbed out. You will need to implement all methods therein and write unit tests for them per the instructions below.
You will also add a 3rd class, Car, implement it, and add unit tests for it. See details below.
General requirements and guidelines
For all code in this project, adhere to the following guidelines:
All assertions should be from the FluentAssertions library. This means they will most likely all contain the keyword Should. 
The unit testing library of choice for this project is xUnit.
When refactoring existing code to make it more testable, ensure that you:
Do not alter the existing functionality of the code, and
Refactor as little as possible to ensure testability of code. Do not just make everything public, refactor only that which must be refactored to improve testability.
If a unit test ever contains more than one assertion, be sure to wrap all assertions inside of an AssertionScope from FluentAssertions.
Testing the Vehicle class
Implement the following tests for the Vehicle class:
Verify the parameterless constructor successfully creates a new object of type Vehicle, and instantiates all public properties to their default values. 
Verify the parameterized constructor successfully creates a new object of type Vehicle, and instantiates all public properties to the provided values.
Verify that the parameterless AddGas method fills the gas tank to 100% of its capacity
Verify that the AddGas method with a parameter adds the supplied amount of gas to the gas tank.
Verify that the AddGas method with a parameter will throw a GasOverfillException if too much gas is added to the tank.Verify that the exception details how much gas you tried to add and what the capacity was.
Using a Theory (or data-driven test), verify that the GasLevel property returns the correct percentage when the gas level is at 0%, 25%, 50%, 75%, and 100%.
Using a Theory (or data-driven test), or a combination of several individual Fact tests, test the following functionality of the Drive method:
Attempting to drive a car without gas returns the status string “Cannot drive, out of gas.”.
Attempting to drive a car with a flat tire returns the status string “Cannot drive due to flat tire.”.
Drive the car 10 miles. Verify that the correct amount of gas was used, that the correct distance was traveled, that GasLevel is correct, that MilesRemaining is correct, and that the total mileage on the vehicle is correct.
Drive the car 100 miles. Verify that the correct amount of gas was used, that the correct distance was traveled, that GasLevel is correct, that MilesRemaining is correct, and that the total mileage on the vehicle is correct.
Drive the car until it runs out of gas. Verify that the correct amount of gas was used, that the correct distance was traveled, that GasLevel is correct, that MilesRemaining is correct, and that the total mileage on the vehicle is correct. Verify that the status reports the car is out of gas.
There is significant logic in the Vehicle class around flat tires. Refactor the class as necessary to make this logic easier to test. Ensure that you do not change any functionality and only make the minimum necessary changes to the code. Then, test the below:
Verify that attempting to change a flat tire using ChangeTireAsync will throw a NoTireToChangeException if there is no flat tire.
Verify that ChangeTireAsync can successfully be used to change a flat tire.
BONUS: Refactor the GotFlatTire method to make it unit-testable. Write a theory unit test with two cases that verifies that GotFlatTire can be both true and false. This will require you to pass a value for the optional parameter in this method. If you do not attempt this bonus, you may leave GotFlatTire alone.

Implementing the SemiTruck class
The SemiTruck class contains several stub methods. Implement them all.
Constructor: Ensure that SemiTrucks always have 18 tires. Instantiate Cargo to be an empty List of CargoItems.
LoadCargo: Takes a CargoItem as a parameter. Adds the CargoItem to the current Cargo.
UnloadCargo. Takes the Name of a CargoItem as a parameter. If a CargoItem with that name exists in the Cargo, removes that CargoItem from the Cargo and returns it. Otherwise, throws an ArgumentException.
GetCargoItemsByName: takes the name of a CargoItem as a parameter and returns all CargoItems with that exact name. If no CargoItems have that name, returns an empty List.
GetCargoItemsByPartialDescription: takes a description as a parameter and returns all CargoItems who have a description containing the passed description. If no CargoItems have that name, returns an empty list.
GetTotalNumberOfItems: Returns an integer equal to the number of total items in the Cargo. This is the sum of all Quantity properties on all CargoItems in the Cargo list.

Testing the SemiTruck class
Add at least one unit test for each method in the SemiTruck class, including the constructor. Do so per these instructions:
Constructor: Verify the constructor creates a new SemiTruck object which is also a Vehicle and has 18 wheels. Verify that the Cargo property for the newly created SemiTruck is a List of CargoItems which is empty, but not null.
LoadCargo: Verify the method correctly adds the passed object to the Cargo.
UnloadCargo Positive Test: Verify that unloading a cargo item that is in the Cargo does remove it from the Cargo and return the matching CargoItem.
UnloadCargo Negative Test: Verify that attempting to unload a CargoItem that does not appear in the Cargo throws a System.ArgumentException.
GetCargoItemsByName Positive Test: Verify that searching the Cargo list by name for an item that does exist returns all matched items with exactly that name. 
GetCargoItemsByName Negative Test: Verify that searching the Cargo list by name for an item that does not exist returns an empty list.
GetCargoItemsByPartialDescription Positive Test: Verify that searching the Cargo list by description for an item that does exist returns all matched items that contain that Description.
GetCargoItemsByPartialDescription Negative Test: Verify that searching the Cargo list by description for an item that does not exist returns an empty list.
GetTotalNumberOfItems: Verify that the method returns the sum of all quantities of all items in the Cargo List.

Implementing the Car class
The Car class has not been provided for you. Create it yourself and implement it as follows:
Car inherits from Vehicle. 
Car has a private field of type HttpClient to be used to call a public external API. 
HttpClient documentation can be found here.
Car has a public integer property NumberOfPassengers with a public get accessor but a private set.
Car has two constructors:
The first constructor has no parameters. It creates a new Car object with a GasTankCapacity of 0, an empty string for Make and Model, and a MilesPerGallon of 0. 
The second constructor has 4 parameters:
A double for GasTankCapacity.
A string for Make.
A string for Model.
A double for MilesPerGallon.
Both constructors always create a Car with NumberOfTires = 4.
Both constructors instantiate the private HttpClient with a BaseAddress equal to https://vpic.nhtsa.dot.gov/api/ 
Hint: If done properly, you will not have to put any code at all inside of the curly braces {} for the parameterless constructor.
Car has a method called IsValidModelForMakeAsync. This method is async and returns a type of Task<bool>. This method has no parameters. The method reaches out to the National Highway Traffic Safety Administration (NHTSA) API via the private HttpClient to determine whether or not the Car’s provided Model is actually a real Model for its Make. If the Model is valid, it returns true. If the Model is not valid, it returns false. You are responsible for determining how best to accomplish this by reading the provided documentation for that API.
Example: Calling this method on a Car with Make = “Honda” and Model = “Civic” should return true. 
Example: Calling this method on a Car with Make = “Honda” and Model = “Camry” should return false.
Hint: It is STRONGLY recommended that you use json and deserialize the response of your API call into a strongly-typed C# object. This may require creating additional classes.
Car has a method called WasModelMadeInYearAsync. This method is async and returns a type of Task<bool>. This method has one parameter: an integer value representing the year. If the user passes in a year before 1995, this method should throw a System.ArgumentException with a helpful message telling the user no data is available for years before 1995. If the provided year is 1995 or after, this method also reaches out to the NHTSA API via the private HttpClient and determines if the Car’s Make and Model were indeed made during the provided year. If the Make and Model were made in that year, the endpoint returns true. If they were not, it returns false.
Example: Calling this method on a Car with Make = “Subaru” and Model = “WRX” and passing in a year of 2000 should return false.
Example: Calling this method on a Car with Make = “Subaru” and Model = “WRX” and passing in a year of 2020 should return true.
Car has a method called AddPassengers. This method returns void. This method has one parameter, an integer representing the number of passengers to add. The method increases NumberOfPassengers by the amount provided. The method will also reduce the MilesPerGallon the car gets by a value of .2 per passenger.
Car has a method called RemovePassengers. This method returns void. This method has one parameter, an integer representing the number of passengers to remove.
The NumberOfPassengers should not go below zero. If the user attempts to remove more passengers from the car than the car has, set NumberOfPassengers to 0.
Otherwise, reduce NumberOfPassengers by the amount the user provided.
Regardless, this method should increase MilesPerGallon by a value of .2 per passenger actually removed.
Example: A Car has a MPG of 20 and contains 5 passengers.
The user removes 3 passengers. NumberOfPassengers is now 2. The MPG is now 20.6.
The user removes 5 passengers. NumberOfPassengers is now 0. The MPG is now 21.
The user removes 25 passengers. NumberOfPassengers is now 0. The MPG is now 21.
Car may also define any private helper methods you deem necessary in implementing the above.

Testing the Car class
Unit test your newly created Car class as follows:
Constructor: verify that newly created Car instances are also Vehicles and have 4 tires.
IsValidModelForMakeAsync test: Test that a Make of Honda and a Model of Civic is valid. Test that a Make of Honda and a Model of Camry is not. You may use two Facts or one Theory for this test.
WasModelMadeInYearAsync Negative Test: Test that passing a value for year that is before 1995 throws a System.ArgumentException.
WasModelMadeInYearAsync Positive Tests: Test that each of these values return the expected result (using a Theory would be a good idea):
A Make that does not exist at all returns false (regardless of model/year).
Make Honda, Model Camry returns false (regardless of year).
Make Subaru, Model WRX returns true for year 2020.
Make Subaru, Model WRX returns false for year 2000.
AddPassengers test: Test that adding passengers to the car reduces the fuel economy of the car by .2 per passenger. Test that removing the passengers then adds back the fuel economy.
RemovePassengers test: Using a Theory, test the following:
Create a Car with 5 passengers that gets 21 MPG. Remove 3 passengers from the car. Verify the car now has 2 passengers and gets 20.6 MPG.
Create a Car with 5 passengers that gets 21 MPG. Remove 5 passengers from the car. Verify the car now has 0 passengers and gets 21 MPG.
Create a Car with 5 passengers that gets 21 MPG. Remove 25 passengers from the car. Verify the car now has 0 passengers and gets 21 MPG.
Feel free to add any additional unit tests to the Car class that you deem necessary. It is not necessary to test any private members of the class.

Wrapping Up
Before submitting your project, ensure that you have completed all the requirements above. If done correctly, your project should have:
At least 4 classes: Vehicle, SemiTruck, CargoItem, Car, and any others you may have needed along the way.
At least 3 test classes: VehicleTests, SemiTruckTests, and CarTests.
Around 40 total unit tests. To pass, at least 30 of your unit tests must be passing and correct, but you should attempt them all in case you make mistakes.
