//Robert Kowalchuk
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ann_2
{
    //set so when this reads a file it also creates the collection used by Network
    class Read_From_Example_File
    {
        string Filename;
        int Num_Inputs;
        public List<Example> Examples;

        public Read_From_Example_File(string Filename, int Num_Inputs)
        {
            this.Filename = Filename;
            this.Num_Inputs = Num_Inputs;
            Examples = new List<Example>();
        }

        public void ReadFile()
        {
            try
            {
                using (StreamReader sr = new StreamReader(Filename))
                {
                    string temp = "";
                    string[] upto;
                    List<float> temp_input = new List<float>();
                    while (!sr.EndOfStream)
                    {
                        temp = sr.ReadLine();
                        // read the line ans split it
                        upto = temp.Split(',');
                        foreach (string s in upto)
                        {
                            temp_input.Add(float.Parse(s));
                            if (temp_input.Count == Num_Inputs)
                            {
                                //once number of splits needed to fill an example is had 
                                //create the Example and add it to Examples
                                Examples.Add(new Example(temp_input));
                                temp_input = new List<float>();
                            }
                        }
                        upto = null;
                    }
                    //Problem is this doesn't account for a file not having the exact number of inputs in a line
                    //Like temp.split returns an array of 66, that last part will be forgotten about
                }
            }
            catch (Exception e)
            {
                throw new Exception("File *" + Filename + "* Didn't Read? ", e);
            }
        }
        }
}
