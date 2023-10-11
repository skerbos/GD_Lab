using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPropsBehavior : MonoBehaviour
{
    private float timeToMove = 2f;
    private Rigidbody2D rb;
    private bool isCalled = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        MoveDown();
    }

    public void MoveDownCallback()
    {
        isCalled = true;
    }

    void MoveDown()
    {
        if (!isCalled) return;

        if (timeToMove > 0)
        {
            timeToMove -= Time.deltaTime;
            rb.velocity = -transform.up * 10f;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }
}
