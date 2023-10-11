using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMarioBehavior : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateMoveUpMario();
    }

    public void RotateMoveUpMario()
    {
        transform.rotation *= Quaternion.Euler(0, 0, 300 * Time.deltaTime);
    }
}
