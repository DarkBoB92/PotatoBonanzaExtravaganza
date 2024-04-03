using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialControlsScript : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialUI;
    [SerializeField] private GameObject hudUI;
    [SerializeField] private GameObject background;
    [SerializeField] private Image backgroundImage;
    [SerializeField] float value;
    Color alphaBackground;
    public bool firstPlay;
    int screenCounter, controlCounter;

    void Start()
    {
        Time.timeScale = 0f;
        background.SetActive(true);
        tutorialUI[0].SetActive(true);
        controlCounter = 0;
    }

    void Update()
    {
        if (Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Escape) && !firstPlay)
        {
            screenCounter++;
            if (screenCounter == 1)
            {
                tutorialUI[0].SetActive(false);
                tutorialUI[1].SetActive(true);
            }
            else if (screenCounter == 2)
            {
                tutorialUI[1].SetActive(false);
                tutorialUI[2].SetActive(true);
            }
            else if (screenCounter == 3)
            {
                tutorialUI[2].SetActive(false);
                alphaBackground.a = 0.5f;
                backgroundImage.color = alphaBackground; 
                hudUI.SetActive(true);
            }
            else if (screenCounter == 4)
            {
                Time.timeScale = 1f;
                firstPlay = true;
                screenCounter = 0;
                alphaBackground.a = 1f;                
                background.SetActive(false);
                backgroundImage.color = alphaBackground;
                hudUI.SetActive(false);
            }
        }
    }
}
