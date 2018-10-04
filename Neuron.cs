//Robert Kowalchuk
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ann_2
{
    
    class Neuron
    {
        public float Output_Value;
        public float Activation_Value;
        public float Input_Value;
        float Input_Max;

        public Neuron(float Input_Max)
        {
            this.Input_Max = Input_Max;
            Output_Value = 0.0f;
            Activation_Value = 0.0f;
            Input_Value = 0.0f;
        }
        //used for input laer to normalise inputs
        public void Input_Function(float Input)
        {
            Input_Value = Input / Input_Max;
        }
        //g(x)
        public void Activation_Function(float Value)
        {
            Activation_Value = 1.0f / (float)(1.0f + Math.Pow(Math.E, (-1.0f * Value)));
        }
        //Used for output layer
        public void Output_Function(float value, int i, Example E)
        {
            if (i == E.Example_Actual)
                Output_Value = (float)(1.0f - value);
            else
                Output_Value = (float)( 0.0f - value);
        }
        //used for back propagation
        public float Derivative_Input_Function(float Input)
        {
            float value = Input / (1.0f - Input);
            
            return value;
        }
        //used to reset the Neuron before going into another Example
        public void Reset()
        {
            Output_Value = 0.0f;
            Activation_Value = 0.0f;
            Input_Value = 0.0f;
        }
    }
}
