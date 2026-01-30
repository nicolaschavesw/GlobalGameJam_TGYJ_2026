using UnityEngine;
using UnityEngine.InputSystem;

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
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("SPACE mantenido");
            AddSliderValue(1.0f);
        }
    }

    public float AddSliderValue(float addValue)
    {
        slider.value += addValue;
        return slider.value;
    }

    

}
