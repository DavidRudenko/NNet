using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNet.Neurons;

namespace NNet.Layers
{
    class InputLayer:Layer
    {
        public InputLayer(int capacity)
        {
            this.Neurons=new List<Neuron>();
            this.Output = new double[capacity];
            for (int i = 0; i < capacity; i++)
            {
                this.Neurons.Add(new InputNeuron());
            }
            
        }

        public void SetInput(double[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                ((InputNeuron) Neurons[i]).Input = input[i];
            }
        }

    }
}
