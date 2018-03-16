namespace NNet.ActivationFunctions
{
    interface IActivationFunction
    {
        double Calculate(double input);
        double CalculateDerivative(double input);
    }
}
