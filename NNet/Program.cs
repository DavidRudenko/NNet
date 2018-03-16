using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Security.Cryptography;
using NNet.ActivationFunctions;
using System.IO;
using System.Globalization;

namespace NNet
{
    class Program
    {
        static void Main(string[] args)
        {

            var normTraining = getNormalized(@"../../DataSets/IrisDataSet-normalized-training.txt");
            var normTesting = getNormalized(@"../../DataSets/IrisDataSet-normalized-testing.txt");
            int length = normTraining.GetLength(0);
            int testingLength = normTesting.GetLength(0);
            double[][] trainingInputs = new double[length][];
            double[] trainingExpected = new double[length];


            double[][] testingInputs= new double[testingLength][];
            double[] testingExpected = new double[testingLength];
            for (int i = 0; i < length; i++)
            {
                if (i < testingLength)
                    testingInputs[i] = new double[3];
                trainingInputs[i] = new double[3];
            }
            for (int i = 0; i < length; i++)
            {
                if (i < testingLength)
                    testingExpected[i] = normTesting[i][3];
                trainingExpected[i] = normTraining[i][3];
            }
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i < testingLength)
                        testingInputs[i][j] = normTesting[i][j];
                    trainingInputs[i][j]=normTraining[i][j];
                }
            }
            var net = new Network(3, 1,20);
            int index = 0;
            double epochError=0.0;
            double[] errors = new double[trainingInputs.Length];

            do
            {
                for (int j = 0; j < trainingInputs.Length; j++)
                {
                    var random = new Random(Guid.NewGuid().GetHashCode()).Next(0, trainingInputs.Length);
                    net.Calculate(trainingInputs[random]);
                    net.Train(new[] {trainingExpected[random]}, .01,.9);
                    errors[j] = net.GetMSE();
                }
                epochError = errors.Sum() / trainingInputs.Count();
                if (index % 1000 == 0)
                {
                    Console.Write($"epochError is {epochError}");
                    Console.Write($", Pass# {index}\n");
                }
                index++;
            } while (epochError > 0.001 && index<10000);
            double[] testingErrors = new double[testingLength];
            for (int i = 0; i < testingLength; i++)
            {
                var random1 = new Random(Guid.NewGuid().GetHashCode());
                var randIndex = random1.Next(0, testingLength);
                var input = testingInputs[randIndex];
                net.Calculate(input);
                testingErrors[randIndex] = net.GetMSE();
                foreach (var d in input)
                {
                    Console.Write(d+" ");
                }
               
                Console.WriteLine($"\nExpected: {testingExpected[randIndex]}");
                Console.WriteLine($"Out is {net.Output[0]}");
                Console.WriteLine($"MeanError is {testingErrors.Sum()/testingErrors.Count()}");
                Console.WriteLine();
            }
            
            Console.ReadLine();
        }
        private static double[][] getNormalized(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                using (var reader = new StreamReader(fs))
                {
                    var lines = new List<string>();
                    
                    while(!reader.EndOfStream)
                    {
                        lines.Add(reader.ReadLine());
                    }
                    double[][] result = new double[lines.Count][];
                    for (int index = 0; index < lines.Count; index++)
                    {
                        result[index] = new double[4];
                    }
                    var i = 0;
                    foreach (var line in lines)
                    {
                        var splited = line.Split('\t');
                        splited = splited.Where(c => c != string.Empty).ToArray();

                        for (int j = 0; j < splited.Length; j++)
                        {
                            if(splited[j]!=string.Empty)
                            result[i][j]= double.Parse(splited[j],CultureInfo.InvariantCulture);
                        }
                        ++i;
                    }
                    return result;
                }
            }

        }
    }
}
