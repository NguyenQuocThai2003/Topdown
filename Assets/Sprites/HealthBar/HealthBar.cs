using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Image fillBar;
    public TextMeshProUGUI HealthText;

    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue;
        HealthText.text = currentValue.ToString() + " / " + maxValue.ToString();
        //HealthText.ForceMeshUpdate();
    }

    //public void UpdateHealth(int value, int maxValue, string text)
    //{
    //    HealthText.text = text;
    //    fillBar.fillAmount = (float)value / (float)maxValue;
    //}
}
