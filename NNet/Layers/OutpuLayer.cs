using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNet.Neurons;

namespace NNet.Layers
{
    class OutputLayer : Layer
    {
        public OutputLayer(int capacity)
        {
            this.Neurons = new List<Neuron>();
            this.Output = new double[capacity];
            for (int i = 0; i < capacity; i++)
            {
                this.Neurons.Add(new OutputNeuron());
            }
        }

        /// <summary>
        /// just a stub, use Train(double[] desired,double lRate) instead
        /// </summary>
        /// <param name="lRate"></param>
        public override void Train(double lRate)
        {
            return;
        }

        public void Train(double[] desired, double lRate)
        {
            for (int i = 0; i < desired.Length; i++)
            {
                (Neurons[i] as OutputNeuron).AdjustWeights(desired[i], lRate);
            }
        }
    }
}