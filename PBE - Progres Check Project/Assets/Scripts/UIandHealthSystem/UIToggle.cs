using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIToggle : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider sliderObject;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        //if (player != null)
        //{
        //    if (sliderObject.value == 0)
        //    {
        //        Destroy(player);
        //        player = null;
        //        canvasObject.SetActive(false);
        //    }
        //    else
        //    {
        //        canvasObject.SetActive(true);
        //    }
        //}
    }

}
