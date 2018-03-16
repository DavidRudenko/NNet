using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NNet.ActivationFunctions;

namespace NNet.Neurons
{
    abstract class Neuron
    {
       public double Output { get; protected set; }
       protected IActivationFunction _activationFunction=new Sigmoid();
       public double Error { get; protected set; }
       public double Gradient { get; protected set; }

        public virtual double Calculate()
        {
            this.Output = _activationFunction.Calculate(GetWeightedInput());
            return this.Output;
        }

        protected double GetWeightedInput()
        {
            var sum = 0.0;
            foreach (var inputConnection in _inputConnections)
            {
                sum += inputConnection.SourceNeuron.Output * inputConnection.Weight;
            }
            return sum;
        }
        protected virtual void CalculateGradient()
        {
            var weightedSum = 0.0;
            foreach (var outputConnection in _outputConnections)
            {
                weightedSum += outputConnection.TargetNeuron.Gradient * outputConnection.Weight;
            }
            this.Gradient = weightedSum * _activationFunction.CalculateDerivative(GetWeightedInput());
        }

        public virtual void AdjustWeights(double lRate,double momentum=0.0)
        {
            CalculateGradient();
            foreach (var inputConnection in _inputConnections)
            {
                var dw = this.Gradient * lRate * inputConnection.SourceNeuron.Output + momentum * inputConnection.LastDW;
                inputConnection.LastDW = dw;
                inputConnection.Weight += dw;
            }
        }
        protected List<Connection> _inputConnections = new List<Connection>();
        protected List<Connection> _outputConnections = new List<Connection>();
        public virtual void ConnectForwards(Neuron targetNeuron)//overriden in InputNeuron
        {
            //if (targetNeuron is InputNeuron)
            //    return;
            if (this._outputConnections.FirstOrDefault(c => c.TargetNeuron == targetNeuron) != null)
                return;

            this._outputConnections.Add(new Connection(this,targetNeuron,RandomizeWeight()));
            targetNeuron.ConnectBacwards(this);
        }
        
        public virtual void ConnectBacwards(Neuron sourceNeuron)//overriden in OutputNeuron
        {
            //if (sourceNeuron is OutputNeuron)
            //    return;
            if (this._inputConnections.FirstOrDefault(c => c.SourceNeuron == sourceNeuron) != null)
                return;
            sourceNeuron.ConnectForwards(this);
            this._inputConnections.Add(new Connection(sourceNeuron,this,RandomizeWeight()));
        }

        protected virtual double RandomizeWeight()
        {
            var rand = new Random(Guid.NewGuid().GetHashCode());
            return rand.Next(-2000, 2000) / 1000;
            
        }
    }
}
