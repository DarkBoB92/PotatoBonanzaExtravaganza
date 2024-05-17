using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;   // Text Element In Canvas
    [SerializeField] GameObject winPanel;
    public float elapsedTime;   // Float That Holds Time

    private void Start()
    {
        elapsedTime = 0f;
        winPanel.SetActive(false);

    }

    void Update()
    {
        elapsedTime += Time.deltaTime;    // Update Timer Every Frame (For The Time Inbetween Frames)
        int minutes = Mathf.FloorToInt(elapsedTime / 60);   // Divide By 60, Round DOWN
        int seconds = Mathf.FloorToInt(elapsedTime % 60);   // Find The Remainder If Divide By 60
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);   // Update And Format Time To Be Displayed Onto Text Element

        if (elapsedTime >= 300)   // if timer == amountofseconds, then turn on winpanel
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
            // FindObjectOfType<GameplayAudio>().AudioTrigger(GameplayAudio.SoundFXCat.Win, transform.position, 0.5f);       - Need to find a way to play it once (since its inside an update loop)
        }
    }
}
