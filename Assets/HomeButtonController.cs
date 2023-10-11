using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButtonController : MonoBehaviour, IInteractiveButton
{
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

        GameManager.instance.ShmupBackToHome();
    }
}
