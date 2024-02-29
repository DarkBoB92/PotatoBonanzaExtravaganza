using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        Debug.Log("Quit Button Pressed");
        Application.Quit();
    }
}
