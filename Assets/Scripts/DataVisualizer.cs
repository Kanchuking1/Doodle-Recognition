using System;
using System.Collections;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    public GameObject defaultMarker;
    public GameObject[] markers;

    public ValueSlider valueSlider;
    public GameObject gridSquare;

    public NetworkPlayer player;

    public Camera mainCamera;

    public Transform markerHolder;
    public Transform valueSliderHolder;

    public float bgPixelSize = 0.2f;

    public double scale = 1f;

    public Color[] bgColors;
    public Color defaultColor;

    private SpriteRenderer[] bgGrid = null; 

    public void VisualizeData(ArrayList datapoints) { 
        foreach (DataPoint point in datapoints)
        {
            GameObject marker = Instantiate(point.diamondType < markers.Length ? markers[point.diamondType] : defaultMarker, markerHolder);
            float x = (float)(point.a0 * scale);
            float y = (float)(point.a1 * scale);
            marker.transform.position += new Vector3(x, y, 0);
        }

        if (!player)
        {
            player = transform.GetComponent<NetworkPlayer>();
        }

        int k = 0;
        foreach (Layer layer in player.nn.layers)
        {
            // Get the parameter length
            int[] dims = layer.GetLayerDims();
            for (int i = 0; i < dims[0]; i++) {
                for (int j = 0; j < dims[1]; j ++)
                {
                    // Render Value Slider
                    string key = k + "," + i + "," + j;
                    ValueSlider sliderGo = Instantiate(valueSlider, valueSliderHolder);
                    sliderGo.Initialize((float)(layer.GetWeight(i, j)), key);
                    sliderGo.onValueChangedEvent.AddListener(onSliderValueChanged);
                }
            }
            k++;
        }

        PaintBG();
    }

    public void onSliderValueChanged(float value, string key)
    {
        string[] decode = key.Split(',');
        int layerNum = Convert.ToInt32(decode[0]);
        int inputNum = Convert.ToInt32(decode[1]);
        int outputNum = Convert.ToInt32(decode[2]);

        player.nn.layers[layerNum].SetWeight(value, inputNum, outputNum);

        PaintBG();
    } 

    void PaintBG ()
    {
        Vector3 screenWorldDims = mainCamera.ScreenToWorldPoint(Vector3.zero);
        float screenHeight = -screenWorldDims.y * 2;
        float screenWidth = -screenWorldDims.x * 2;

        int numberOfPixels = (int)Math.Round((float)((screenHeight + 1) * (screenWidth + 1) / (bgPixelSize * bgPixelSize)));
        if (bgGrid == null || bgGrid?.Length < 1)
        {
            int j = 0;
            bgGrid = new SpriteRenderer[numberOfPixels];
            for (float px = -screenWidth / 2; px < screenWidth / 2; px += bgPixelSize)
            {
                for (float py = -screenHeight / 2; py < screenHeight / 2; py += bgPixelSize)
                {
                    GameObject bgCell = Instantiate(gridSquare);
                    bgCell.transform.position = new Vector3(px, py, 0);
                    bgCell.transform.localScale = new Vector3(bgPixelSize, bgPixelSize, 1f);
                    SpriteRenderer bg = bgCell.GetComponent<SpriteRenderer>();
                    bgGrid[j] = bg;
                    j++;
                }
            }
        }
        int i = 0;
        for (float px = -screenWidth / 2; px < screenWidth / 2; px += bgPixelSize)
        {
            for (float py = -screenHeight / 2; py < screenHeight / 2; py += bgPixelSize)
            {
                float x1_in = px / screenWidth * 2;
                float x2_in = py / screenHeight * 2;
                double[] inputs = { x1_in, x2_in };
                int prediction = player.nn.Classify(inputs);
                bgGrid[i].color = (prediction < bgColors.Length) ? bgColors[prediction] :defaultColor;
                i++;
            }
        }
    }
}
