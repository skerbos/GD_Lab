using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{

    public string nextSceneName;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Change scene");
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Single);

            other.gameObject.transform.position = new Vector3(0, 3, 0);
        }
    }
}
