using CodeLouisvilleUnitTestProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace CodeLouisvilleUnitTestProjectTests
{
    public class CarTests
    {
        public ITestOutputHelper _logger;
        public Car _car;

        public CarTests(ITestOutputHelper testOutputHelper)
        {
            _logger = testOutputHelper;

        }
    
       
    }
}
