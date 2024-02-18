using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CanvasToggle : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider sliderObject;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject player;

    void Update()
    {
        if (sliderObject.value == 0)
        {
            Destroy(player); player = null;
            canvasObject.SetActive(false);
        }
        else
        {
            canvasObject.SetActive(true);
        }
    }
}
