using System;

namespace NNet.ActivationFunctions
{
    class Sigmoid:IActivationFunction
    {
        private double a = 1;
        private double b =1;
        public double Calculate(double input)
        {
            return b / (1 + Math.Exp(-(a * input))) ;
        }
        public double CalculateDerivative(double input)
        {
            return b * a * Math.Exp(-(a*input)) / Math.Pow(1 + Math.Exp(-(a*input)),2);

        }
    }
}
