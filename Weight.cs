using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ann_2
{
    class Weight
    {
        public float Current_Weight;
        public float Updated_Weight;
        //holds two float values representing Weight
        public Weight(float Current_Weight)
        {
            this.Current_Weight = Current_Weight;
            Updated_Weight = 0.0f;
        }


    }
}
