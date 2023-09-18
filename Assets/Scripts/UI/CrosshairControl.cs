using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CrosshairControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowMouse();
    }

    void FollowMouse()
    {
        transform.position = Input.mousePosition;
        Cursor.visible = false;
    }
}
