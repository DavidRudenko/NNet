using System.Collections.Generic;

namespace NNet.Neurons
{
    internal class OutputNeuron:Neuron
    {
        public OutputNeuron()
        {
           // this.ConnectBacwards(new BiasNeuron(1.0));
        }
        public void CalcuateGradient(double desired)
        {
            this.Error = desired - this.Output;
            this.Gradient = this.Error * _activationFunction.CalculateDerivative(GetWeightedInput());
        }

        public override void AdjustWeights(double lRate,double momentum=0.0)
        {
            return;
        }

        public void AdjustWeights(double desired, double lRate,double momentum=0.0)
        {
            CalcuateGradient(desired);
            foreach (var inputConnection in _inputConnections)
            {
                var dw = this.Gradient * lRate * inputConnection.SourceNeuron.Output + momentum * inputConnection.LastDW;
                inputConnection.LastDW = dw;
                inputConnection.Weight += dw;
            }
        }

        public override void ConnectForwards(Neuron targetNeuron)
        {
            return;
        }
    }
}