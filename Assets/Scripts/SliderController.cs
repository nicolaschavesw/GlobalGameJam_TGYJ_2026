using UnityEngine;

public class SliderController : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider.value = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float AddSliderValue(float addValue)
    {
        return slider.value;
    }
}
