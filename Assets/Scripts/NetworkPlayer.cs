using System;
using System.Collections;
using UnityEngine;

public class NetworkPlayer : MonoBehaviour
{
    private const string dataHeader = "@DATA";

    public int[] layerSizes;
    public TextAsset ARFFDataSet;

    readonly ArrayList data;

    private DataVisualizer visualizer;
    public NeuralNetwork nn;

    public NetworkPlayer()
    {
        data = new ArrayList();
    }

    // Start is called before the first frame update
    void Start()
    {
        PopulateData();
        nn = new NeuralNetwork(layerSizes);

        VisualizeData();
    }

    private void PopulateData()
    {
        string content = ARFFDataSet.text;
        string[] AllLines = content.Split("\n");

        int i = 0;
        while (i < AllLines.Length && String.Compare(AllLines[i].Trim(), dataHeader) != 0)
        {
            i++;
        }
        i++;
        while (i < AllLines.Length)
        {
            string[] values = AllLines[i].Split(',');
            DataPoint dataPoint = new DataPoint(Convert.ToDouble(values[0]), Convert.ToDouble(values[1]), Convert.ToInt32(values[2]));
            data.Add(dataPoint);
            i++;
        }
    }

    private void VisualizeData()
    {
        if (!visualizer)
        {
            visualizer = transform.GetComponent<DataVisualizer>();
        }

        if (!visualizer)
        {
            return;
        }

        visualizer.VisualizeData(data);
    }
}
