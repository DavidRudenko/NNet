using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNet.Neurons
{
    class InputNeuron:Neuron
    {
        public InputNeuron()
        {
            
        }
        public double Input { get; set; }
        public override double Calculate()
        {
            this.Output = _activationFunction.Calculate(Input);
            return this.Output;
        }

        public override void AdjustWeights(double lRate,double momentum=0.0)
        {
            return;
        }

        public override void ConnectBacwards(Neuron sourceNeuron)
        {
            if (sourceNeuron is BiasNeuron)
            {
                base.ConnectBacwards(sourceNeuron);
            }
            else
                return;
        }
    }
}
