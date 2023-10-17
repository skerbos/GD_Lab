using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class HomeButtonController : MonoBehaviour, IInteractiveButton
{

    public UnityEvent returnHome;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonClick()
    {
        SceneManager.LoadSceneAsync("Start Screen", LoadSceneMode.Single);

        //GameManager.instance.ShmupBackToHome();
        returnHome.Invoke();
    }
}
