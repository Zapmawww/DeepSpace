using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public Slider slider; //Health bar slider
    public Gradient gradient; ////The color gradient of the oxygen bar        氧气条的颜色渐变
    public Image fill;


    public void SetMaxOxygen(int oxygen) //Control the maximum value of the slider directly in the script          直接在代码中控制slider的最大值
    {
        slider.maxValue = oxygen;
        slider.value = oxygen;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetOxygen(int oxygen)  //Make the oxygen value equal to the slider's value                  使氧气值等于slider的值
    {
        slider.value = oxygen;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
