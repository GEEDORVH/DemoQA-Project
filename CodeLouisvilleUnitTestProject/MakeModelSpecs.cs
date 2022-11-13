using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeLouisvilleUnitTestProject
{
    [Serializable]
    public class MakeModelSpecs
    {
        [JsonProperty("Make_ID")]
        public int MakeId { get; set; }

        [JsonProperty("Make_Name")]
        public string MakeName { get; set; }

        [JsonProperty("Model_ID")]
        public int ModelId { get; set; }

        [JsonProperty("Model_Name")]
        public string ModelName { get; set; }
    }
}
