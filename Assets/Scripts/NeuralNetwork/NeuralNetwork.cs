public class NeuralNetwork
{
    public readonly Layer[] layers = null;
    public readonly int[] layerSize;

    public double cost;

    public NeuralNetwork(int[] layerSizes)
    {
        int numLayers = layerSizes.Length;
        layers = new Layer[numLayers - 1];

        for (int i = 0; i < numLayers - 1; i++)
        {
            layers[i] = new Layer(layerSizes[i], layerSizes[i + 1]);
        }
    }

    double[] Propagte(double[] inputs)
    {
        foreach (Layer layer in layers)
        {
            inputs = layer.CalculateOutputs(inputs);
        }
        return inputs;
    }

    public int Classify(double[] inputs)
    {
        double[] outputs = Propagte(inputs);
        return IndexOfMaxValue(outputs);
    }

    void UpdateCost(double[] inputs)
    {
        //double[] outputs = Propagte()
    }

    int IndexOfMaxValue(double[] outputs)
    {
        int max = 0;
        for (int i = 0; i < outputs.Length; i++)
        {
            if (outputs[i] > outputs[max])
            {
                max = i;
            }
        }
        return max;
    }

    public override string ToString()
    {
        string output = "";
        foreach (Layer layer in layers)
        {
            output += layer.ToString() + "\n";
        }
        return output;
    }
}