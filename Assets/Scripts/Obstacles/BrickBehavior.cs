using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehavior : MonoBehaviour
{
    public Vector2 boxSize;
    public float boxCheckDistance = 2f;
    public LayerMask layerMask;

    public GameObject coinHolder;

    public bool isCoinSpawner = true;
    public bool hitDisabled = false;
    public bool coinCollected = false;
    private RaycastHit2D hitDetect;

    private Rigidbody2D brickRb;

    // Start is called before the first frame update
    void Start()
    {
        brickRb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckAbove();
    }

    public void FreezeAllConstraints()
    {
        brickRb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void UnfreezeYConstraint()
    {
        brickRb.constraints = RigidbodyConstraints2D.FreezeAll;
        brickRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
    }

    public void ReturnToOriginalPosition()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(0, 0, 0), 0.1f);
    }

    void CheckAbove()
    {
        hitDetect = (Physics2D.BoxCast(transform.position + transform.up * boxCheckDistance, boxSize, 0, transform.up, boxCheckDistance, layerMask));
        if (hitDetect)
        {
            FreezeAllConstraints();
        }
        else
        {
            if (!hitDisabled)
                UnfreezeYConstraint();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 normal = collision.GetContact(0).normal;

        if (normal == (Vector2)transform.up && !hitDisabled && isCoinSpawner && !coinCollected)
        {
            Instantiate(coinHolder, transform.parent);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + transform.up * boxCheckDistance, boxSize);
    }
}
