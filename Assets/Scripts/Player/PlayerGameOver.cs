using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGameOver : MonoBehaviour
{
    public UnityEvent shmupGameOver;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOverCallback()
    {
        shmupGameOver.Invoke();
    }
}
