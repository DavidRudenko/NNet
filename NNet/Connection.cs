using System;
using NNet.Neurons;

namespace NNet
{
    internal class Connection
    {
        private readonly double _weight;
        public Neuron SourceNeuron { get; }
        public Neuron TargetNeuron { get; }
        public double Weight { get; set; }
        public double LastDW { get; set; }

        public Connection(Neuron sourceNeuron, Neuron targetNeuron)
        {
            if (sourceNeuron == null)
                throw new ArgumentNullException(nameof(sourceNeuron));
            if (targetNeuron == null)
                throw new ArgumentNullException(nameof(targetNeuron));
            this.Weight = new Random(Guid.NewGuid().GetHashCode()).NextDouble();
            SourceNeuron = sourceNeuron;
            TargetNeuron = targetNeuron;
        }

        public Connection(Neuron sourceNeuron, Neuron targetNeuron, double weight) : this(sourceNeuron, targetNeuron)
        {
            this.Weight = weight;
        }
    }
}