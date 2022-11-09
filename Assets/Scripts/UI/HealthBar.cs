using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider; //Health bar slider
    public Gradient gradient; //The color gradient of the health bar         血条的颜色渐变
    public Image fill;


    public void SetMaxHealth(int health) //Control the maximum value of the slider directly in the script          直接在代码中控制slider的最大值
    {
        slider.maxValue = health;  
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health)  //Make the health value equal to the slider's value                  使健康值等于slider的值
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }


}
