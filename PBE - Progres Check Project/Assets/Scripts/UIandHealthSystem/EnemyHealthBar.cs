using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    static Vector3 cameraPov;

    private void Awake()
    {
        slider.gameObject.SetActive(false);
        cameraPov = GameObject.FindGameObjectWithTag("CameraPOV").transform.position;
    }

    private void Update()
    {
        if (cameraPov != null)
        {
            slider.transform.LookAt(cameraPov);
        }
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
