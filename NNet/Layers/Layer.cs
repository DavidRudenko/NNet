using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNet.Neurons;

namespace NNet.Layers
{
    abstract class Layer
    {
        public List<Neuron> Neurons { get;protected set; }
        public double[] Output { get; protected set; }

        public virtual void Calculate()
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].Calculate();
                this.Output[i] = Neurons[i].Output;
            }
        }
        public virtual void ConnectForwards(Layer layer)
        {
            foreach (var neuron in Neurons)
            {
                foreach (var layerNeuron in layer.Neurons)
                {
                    neuron.ConnectForwards(layerNeuron);
                }
            }
        }

        public virtual void ConnectBacwards(Layer layer)
        {
            foreach (var neuron in Neurons)
            {
                foreach (var layerNeuron in layer.Neurons)
                {
                    neuron.ConnectBacwards(layerNeuron);
                }
            }
        }

    }
}
