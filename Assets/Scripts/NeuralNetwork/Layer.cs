using System;
using UnityEngine;

public class Layer {
    int numNodesIn;
    public int numNodesOut;
    double[,] weights;
    double[] biases;

    public Layer(int numNodesIn, int numNodesOut)
    {
        this.numNodesIn = numNodesIn;
        this.numNodesOut = numNodesOut;
        weights = new double[numNodesIn, numNodesOut];
        biases = new double[numNodesOut];
    }

    public double[] CalculateOutputs(double[] input) {
        double[] weightedInputs = new double[numNodesOut];

        for (int nodeOut = 0; nodeOut < numNodesOut; nodeOut++)
        {
            double weightedInput = this.biases[nodeOut];
            for (int nodeIn = 0; nodeIn < numNodesIn; nodeIn++)
            {
                weightedInput += weights[nodeIn, nodeOut] * input[nodeIn];
            }
            weightedInputs[nodeOut] = Sigmoid(weightedInput);
        }

        return weightedInputs;
    }

    public double Sigmoid(double value)
    {
        return 1 / (1 + Math.Exp(-value));
    }

    public override string ToString()
    {
        string output = "W: [ ";
        foreach (double weight in  weights)
        {
            output += weight.ToString() + " ";
        }
        output += "]\n B : [ ";
        foreach (double bias in biases)
        {
            output += bias.ToString() + " ";
        }
        output += "]";
        return output;
    }

    public double GetWeight(int i, int j)
    {
        return weights[i, j];
    }

    public void SetWeight(double value, int i, int j)
    {
        weights[i, j] = value;
    }

    public double GetBias(int i)
    {
        return biases[i];
    }

    public void SetBias(double value, int i)
    {
        biases[i] = value;
    }

    public int[] GetLayerDims()
    {
        int[] dims = { numNodesIn, numNodesOut };
        return dims;
    }
}