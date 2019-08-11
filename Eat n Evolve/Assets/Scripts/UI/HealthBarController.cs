using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour
{
    [SerializeField] private float lerpSpeed; // the rate the hp bar drops or increases
    [SerializeField] private Image image;


    private float fillAmount;

    public float MaxValue { get; set; }
    public float Value
    {
        set
        {
            Debug.Log("MaxValue:" + MaxValue);
            Debug.Log("Before fillAmount: " + fillAmount);
            Debug.Log("Value:" + value);
            fillAmount = Map(value, 0, MaxValue, 0, 1);
            Debug.Log("After fillAmount: " + fillAmount);
        }
    }

    private void HandleBar()
    {
        if (fillAmount != image.fillAmount)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, fillAmount, Time.deltaTime * lerpSpeed); // if hp changesm make UI display it
        }

        ChangingBarColor();
    }

    // Update is called once per frame
    void Update()
    {
        HandleBar();
    }

    // (passed in value, min HP so 0, Max Hp, 0 as min, 1 as max)
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        Debug.Log("Map Value:" + value);
        float mapValue = (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        return mapValue;
    }

    private void ChangingBarColor()
    {
        image.color = Color.Lerp(Color.red, Color.green, Mathf.Clamp(fillAmount - 0f / 1f - 0f, 0.0f, 1.0f));
    }
}
