//Robert Kowalchuk
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ann_2
{
    class Example
    {
        public List<float> Example_Contents;
        public float Example_Actual;
        //take in the inputs from a file and set the actual value from that collection and update 
        //this example so that actual is no longer in this collection
        public Example (List<float> Contents)
        {
            Example_Contents = new List<float>();
            Example_Actual = Contents[Contents.Count() - 1];
            Example_Contents = Contents;
            Example_Contents.RemoveAt(Example_Contents.Count() - 1);
        }

    }
}
