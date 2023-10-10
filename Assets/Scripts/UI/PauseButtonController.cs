using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClick()
    {

        isPaused = !isPaused;
        if (isPaused)
        {
            image.sprite = playIcon;
            GameManager.instance.GamePause();
        }
        else
        {
            image.sprite = pauseIcon;
            GameManager.instance.GameResume();
        }

        
    }
}