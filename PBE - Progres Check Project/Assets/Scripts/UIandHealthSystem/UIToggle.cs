using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
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
