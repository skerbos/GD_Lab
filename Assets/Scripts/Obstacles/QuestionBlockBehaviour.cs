using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlockBehaviour : MonoBehaviour
{

    public Vector2 boxSize;
    public float boxCheckDistance = 2f;
    public LayerMask layerMask;

    public Sprite brownBlock;

    public GameObject coinHolder;

    public bool hitDisabled = false;
    private RaycastHit2D hitDetect;

    private Rigidbody2D questionBlockRb;
    private Animator blockAnimator;

    // Start is called before the first frame update
    void Start()
    {
        questionBlockRb = transform.GetComponent<Rigidbody2D>();
        blockAnimator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckAbove();
    }

    public void FreezeAllConstraints()
    {
        questionBlockRb.constraints = RigidbodyConstraints2D.FreezeAll;   
    }

    public void UnfreezeYConstraint()
    {
        questionBlockRb.constraints = RigidbodyConstraints2D.FreezeAll;
        questionBlockRb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
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

        if (normal == (Vector2) transform.up && !hitDisabled)
        {
            transform.GetComponent<SpriteRenderer>().sprite = brownBlock;

            Instantiate(coinHolder, transform.parent);

            blockAnimator.SetBool("isDisabled", true);
        }

    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawCube(transform.position + transform.up * boxCheckDistance, boxSize);
    }
}
