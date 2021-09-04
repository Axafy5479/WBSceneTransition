using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class SliderValue : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private Text text;
    private void Start()
    {
        text = this.GetComponent<Text>();
        OnValueChanged();
    }
    public void OnValueChanged()
    {
        text.text = slider.value.ToString();
    }
}
