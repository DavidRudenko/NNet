using System.Collections.Generic;
using NNet.Layers;
using NNet.Neurons;

namespace NNet
{
    class Network
    {
        public List<Layer> Layers { get; }=new List<Layer>();
        public double[] Output;
        private int _depth = 0;
        public Network(int inputCapacity, int outputCapacity, params int[] hiddenCapacities)
        {
            this._depth = 2 + hiddenCapacities.Length;
            Layers.Add(new InputLayer(inputCapacity));
            for (int i = 0; i < hiddenCapacities.Length; i++)
            {
                Layers.Add(new HiddenLayer(hiddenCapacities[i]));
                
            }
            Layers.Add(new OutputLayer(outputCapacity));
            for (int i = 1; i <_depth; i++)
            {
             Layers[i-1].ConnectForwards(Layers[i]);   
            }
            Output = new double[outputCapacity];
        }

        public void Train(double[]expected, double lRate, double momentum = 0.0)
        {
            var outputLayer = Layers[Layers.Count - 1] as OutputLayer;
            outputLayer.Train(expected,lRate);
            for (int i = Layers.Count - 1; i > 0; i--)
            {
                Layers[i].Train(lRate,momentum);
            }
        }

        public double GetMSE()
        {
            var sum=0.0;
            var outputLayer = Layers[Layers.Count - 1] as OutputLayer;
            foreach ( var neuron in outputLayer.Neurons)
            {
                sum += neuron.Error * neuron.Error;
            }
            return .5 * sum;
        }
        public void Calculate(params double[] input)
        {
            (this.Layers[0] as InputLayer).SetInput(input);
            foreach (var layer in Layers)
            {
                layer.Calculate();

            }
            this.Output = Layers[Layers.Count - 1].Output;
        }

    }
}
