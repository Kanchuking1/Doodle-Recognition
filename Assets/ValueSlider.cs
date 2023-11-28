using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using static UnityEditor.Experimental.GraphView.GraphView;
using TMPro;

public class ValueSlider : MonoBehaviour
{
    Slider slider;
    public UnityEvent<float, string> onValueChangedEvent;
    public string key= "";
    public float value = 0;

    public void Initialize(float value, string key)
    {
        this.value = value;
        this.key = key;
        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(InvokeValueChanged);
        gameObject.name = "W" + key;
        TextMeshProUGUI label = transform.GetComponentInChildren<TextMeshProUGUI>();
        label.text = "W " + key;
    }

    private void InvokeValueChanged(float value)
    {
        onValueChangedEvent?.Invoke(value, key);
        this.value = value;
    }
}
