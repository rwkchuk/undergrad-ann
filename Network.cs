//Robert Kowalchuk
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ann_2
{
    class Network
    {
        int Num_Layers;
        int Num_Hidden_Layers;
        int Num_Inputs;
        int Num_Outputs;
        float Max_Of_Input_Num;
        float Accuracy;
        int Num_Weights;

        //J is layer #
        //I is Node #
        //X is weight # within layer
        //y is also for weight #
        List<List<List<Weight>>> All_Weights;
        List<List<Neuron>> All_Neurons;

        public Network(int Num_Hidden_Layers, int Num_Inputs, int Num_Outputs, float Max_Of_Input_Num, float Accuracy)
        {
            this.Num_Hidden_Layers = Num_Hidden_Layers;
            this.Num_Inputs = Num_Inputs;
            this.Num_Outputs = Num_Outputs;
            this.Max_Of_Input_Num = Max_Of_Input_Num;
            this.Accuracy = Accuracy;
            Num_Weights = this.Num_Inputs + 1;
            Num_Layers = Num_Hidden_Layers + 2;

            All_Neurons = new List<List<Neuron>>();
            All_Weights = new List<List<List<Weight>>>();

            Random Ran = new Random();
            //set number of Neurons and weights for all layers except output
            for(int j=0; j<Num_Layers-1; j++)
            {
                All_Neurons.Add(new List<Neuron>());
                All_Weights.Add(new List<List<Weight>>());
                for (int i=0;i< this.Num_Inputs;i++)
                {
                    All_Neurons[j].Add(new Neuron(Max_Of_Input_Num));
                    All_Weights[j].Add(new List<Weight>());
                    for(int x=0;x< Num_Weights;x++)
                    {
                        //setting bias weight
                        if (x == Num_Weights - 1)
                            All_Weights[j][i].Add(new Weight(1.0f * 0.0001f));
                        else
                            All_Weights[j][i].Add(new Weight( (float)Ran.NextDouble() * 0.0001f ));
                    }
                }
            }
            //Setting the output layer
            All_Neurons.Add(new List<Neuron>());
            for(int i=0;i<Num_Outputs;i++)
            {
                All_Neurons[Num_Layers - 1].Add(new Neuron(Max_Of_Input_Num));
            }
        }
        
        public void Forward_Propagation(List<Example> Examples)
        {
            foreach(Example E in Examples)
            {
                //For each Neuron in a layer do Input -> Activation -> Output
                //only use Activation when needed
                //traverse all weights for a Neuron when do Input 
                Console.WriteLine("Example " + Examples.IndexOf(E));
                for(int i=0;i<Num_Inputs;i++)
                {
                    All_Neurons[0][i].Input_Function(E.Example_Contents[i]);
                    All_Neurons[0][i].Output_Value = All_Neurons[0][i].Input_Value;
                }
                //start at second layer and end at last hidden layer before output layer 
                for(int j=1;j<Num_Hidden_Layers+1;j++)
                {
                    for(int i=0;i<Num_Inputs;i++)
                    {
                        for (int x = 0; x < Num_Weights; x++)
                        {
                            //f(x)
                            All_Neurons[j][i].Input_Value += All_Neurons[j - 1][i].Output_Value * All_Weights[j-1][i][x].Current_Weight;
                        }
                        //g(x)
                        All_Neurons[j][i].Activation_Function(All_Neurons[j][i].Input_Value);
                        //setting output
                        All_Neurons[j][i].Output_Value = All_Neurons[j][i].Activation_Value;
                    }
                }
                //last layer, output layer
                int J = Num_Layers - 1;
                for(int i=0; i<Num_Outputs; i++)
                {
                    for(int x=0;x< Num_Weights; x++)
                    {
                        All_Neurons[J][i].Input_Value += All_Neurons[J-1][i].Output_Value * All_Weights[J - 1][i][x].Current_Weight;
                    }
                    All_Neurons[J][i].Activation_Function(All_Neurons[J][i].Input_Value);
                    //All_Neurons[J][i].Output_Value = All_Neurons[J][i].Activation_Value;
                    All_Neurons[J][i].Output_Function(All_Neurons[J][i].Activation_Value, i, E);
                    //write the finale weight from output with the actual value for the example
                    Console.WriteLine("Output " + i + " : " + All_Neurons[J][i].Output_Value + " : Actual " + E.Example_Actual);
                }
                Console.WriteLine(" - - - - - - - - - ");
                //Reset all stored values except for weights so outputs do not grow biggere and bigger
                foreach(List<Neuron> ln in All_Neurons)
                {
                    foreach(Neuron n in ln)
                    {
                        n.Reset();
                    }
                }
            }
        }
        public void Forward_Back_Propagation(List<Example> Examples)
        {
            foreach (Example E in Examples)
            {
                for (int i = 0; i < Num_Inputs; i++)
                {
                    All_Neurons[0][i].Input_Function(E.Example_Contents[i]);
                    All_Neurons[0][i].Output_Value = All_Neurons[0][i].Input_Value;
                }

                for (int j = 1; j < Num_Hidden_Layers + 1; j++)
                {
                    for (int i = 0; i < Num_Inputs; i++)
                    {
                        for (int x = 0; x < Num_Weights; x++)
                        {
                            All_Neurons[j][i].Input_Value += All_Neurons[j - 1][i].Output_Value * All_Weights[j - 1][i][x].Current_Weight;
                        }
                        All_Neurons[j][i].Activation_Function(All_Neurons[j][i].Input_Value);
                        All_Neurons[j][i].Output_Value = All_Neurons[j][i].Activation_Value;
                    }
                }
                int J = Num_Layers - 1;
                for (int i = 0; i < Num_Outputs; i++)
                {
                    for (int x = 0; x < Num_Weights; x++)
                    {
                        All_Neurons[J][i].Input_Value += All_Neurons[J - 1][i].Output_Value * All_Weights[J - 1][i][x].Current_Weight;
                    }
                    All_Neurons[J][i].Activation_Function(All_Neurons[J][i].Input_Value);
                    //All_Neurons[J][i].Output_Value = All_Neurons[J][i].Activation_Value;
                    All_Neurons[J][i].Output_Function(All_Neurons[J][i].Activation_Value, i, E);
                }

                //Start Back propagation
                //Right to Left
                //set Updated Weight for layers before output
                for(int i=0;i<Num_Outputs;i++)
                {
                    for(int x=0;x< Num_Weights; x++)
                    {
                        All_Weights[J - 1][i][x].Updated_Weight = All_Neurons[J][i].Derivative_Input_Function(All_Neurons[J][i].Input_Value) * ( E.Example_Actual - All_Neurons[J][i].Output_Value );
                    }
                }
                //Set Updated Weight for rest of layers 
                for(int j = Num_Layers- Num_Hidden_Layers; j>0;j--)
                {
                    for(int i=0;i<Num_Inputs;i++)
                    {
                        for(int x=0;x< Num_Weights; x++)
                        {
                            float temp = 0.0f;
                            for(int y=0; y< Num_Weights; y++)
                            {
                                //take product of previous layers new weight and the next layers current weight sumed for all weights for these layers
                                temp += All_Weights[j-1][i][y].Current_Weight * All_Weights[j][i][y].Updated_Weight;
                            }
                            //set the new eight for the next layer
                            All_Weights[j - 1][i][x].Updated_Weight = All_Neurons[j][i].Derivative_Input_Function(All_Neurons[j][i].Input_Value) * temp;
                        }
                    }
                }
                //Set Current Weight using Updated Weight for all Weights in Network
                for(int j=0;j<Num_Layers-1;j++)
                {
                    for(int i=0; i<Num_Inputs;i++)
                    {
                        for(int x =0; x< Num_Weights; x++)
                        {
                            //for all weights set the new current weight
                            All_Weights[j][i][x].Current_Weight = All_Weights[j][i][x].Current_Weight + (Accuracy * All_Neurons[j][i].Output_Value * All_Weights[j][i][x].Updated_Weight);
                        }
                    }
                }
                //rest the Neruons
                foreach (List<Neuron> ln in All_Neurons)
                {
                    foreach (Neuron n in ln)
                    {
                        n.Reset();
                    }
                }
            }
        }
    }
}
