using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLouisvilleUnitTestProject
{
    public class MakeModelSpecsRoot
    {
        public int Count { get; set; }
        public string Message { get; set; }
        public string SearchCriteria { get; set; }
        public List<MakeModelSpecs> Results { get; set; }
    }
}
