using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNet.Neurons
{
    class BiasNeuron:Neuron
    {
        public double Bias { get; private set; }
        public BiasNeuron(double bias)
        {
            this.Bias = bias;
            this.Output = this.Bias;
        }
        public override string ToString()
        {
            return $"Bias Neuron: bias = {this.Bias}";
        }
        public override void ConnectBacwards(Neuron sourceNeuron)
        {
            return;//base.ConnectBacwards(sourceNeuron);
        }
        public override void ConnectForwards(Neuron targetNeuron)
        {
            return;// base.ConnectForwards(targetNeuron);
        }
    }
}
