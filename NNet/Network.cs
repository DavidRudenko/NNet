using System.Collections.Generic;
using System.Runtime.InteropServices;
using NNet.Layers;
using NNet.Neurons;

namespace NNet
{
    class Network
    {
        public List<Layer> Layers { get; }=new List<Layer>();
        public double[] Output;
        private int _depth = 0;
        private OutputLayer _outputLayer;
        private InputLayer _inputLayer;
        public Network(int inputCapacity, int outputCapacity, params int[] hiddenCapacities)
        {
            this._depth = 2 + hiddenCapacities.Length;
            Layers.Add(new InputLayer(inputCapacity));
            this._inputLayer = (InputLayer) Layers[0];
            for (int i = 0; i < hiddenCapacities.Length; i++)
            {
                Layers.Add(new HiddenLayer(hiddenCapacities[i]));
                
            }
            Layers.Add(new OutputLayer(outputCapacity));
            for (int i = 1; i <_depth; i++)
            {
             Layers[i-1].ConnectForwards(Layers[i]);   
            }
            this._outputLayer = (OutputLayer) Layers[Layers.Count - 1];
            Output = new double[outputCapacity];
        }

        public void Train(double[]expected, double lRate, double momentum = 0.0)
        {
            var outputLayer = _outputLayer;//Layers[Layers.Count - 1] as OutputLayer;
            outputLayer.Train(expected,lRate,.9);
            for (int i = Layers.Count - 2; i > 0; i--)
            {
                (Layers[i] as HiddenLayer).Train(lRate,momentum);
            }
        }

        public double GetMSE()
        {
            var sum=0.0;
            var outputLayer =_outputLayer; //Layers[Layers.Count - 1] as OutputLayer;
            foreach ( var neuron in outputLayer.Neurons)
            {
                sum += neuron.Error * neuron.Error;
            }
            return .5 * sum;
        }
        public void Calculate(params double[] input)
        {
            _inputLayer.SetInput(input);//(this.Layers[0] as InputLayer).SetInput(input);
            foreach (var layer in Layers)
            {
                layer.Calculate();

            }
            this.Output = Layers[Layers.Count - 1].Output;
        }

    }
}
