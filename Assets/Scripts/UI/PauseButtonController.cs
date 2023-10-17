using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseButtonController : MonoBehaviour, IInteractiveButton
{
    private bool isPaused = false;
    public Sprite pauseIcon;
    public Sprite playIcon;
    private Image image;

    public UnityEvent gamePaused;
    public UnityEvent gameResumed;
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
            //GameManager.instance.GamePause();
            gamePaused.Invoke();
        }
        else
        {
            image.sprite = pauseIcon;
            //GameManager.instance.GameResume();
            gameResumed.Invoke();
        }

        
    }
}