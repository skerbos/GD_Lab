using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{

    public GameObject player;
    public float dampTime = 0.15f;

    public bool followPlayerCheck = true;

    private float shakePower;
    private float shakeFadeTime;
    private float shakeTimeRemaining;

    private Vector3 velocity = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (!followPlayerCheck) return;

        Vector3 point = Camera.main.WorldToViewportPoint(player.transform.position);
        Vector3 delta = player.transform.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta + new Vector3(0, 2, 0);
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }

    void OffsetTowardsMouse()
    {
        
    }

    public void ShakeCamera(float duration, float power)
    {
        shakeTimeRemaining = duration;
        shakePower = power;

        shakeFadeTime = power / duration;

        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;

            float xShake = Random.Range(-1f, 1f) * shakePower;
            float yShake = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xShake, yShake, 0);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }
    }
}
