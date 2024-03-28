using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialControlsScript : MonoBehaviour
{
    [SerializeField] private GameObject tutorialUI;

    void Start()
    {
        Time.timeScale = 0f;
        tutorialUI.SetActive(true);
    }

    void Update()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 1f;
            tutorialUI.SetActive(false);
        }
    }
}
