using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        slider.gameObject.SetActive(false);
    }

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        if(!slider.gameObject.active)
        {
            slider.gameObject.SetActive(true);
        }
        slider.value = currentValue / maxValue;   // Adjusts slider value depending on health in comparison to max health

    }
}
