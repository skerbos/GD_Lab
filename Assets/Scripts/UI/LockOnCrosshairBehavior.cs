using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnCrosshairBehavior : MonoBehaviour
{

    public AudioSource lockOnAudio;
    public AudioClip lockOnSound;

    public GameObject lockOnTarget;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowLockOnTarget();
    }

    void FollowLockOnTarget()   
    {
        //transform.position = Camera.main.WorldToScreenPoint(lockOnTarget.transform.position);
        transform.position = lockOnTarget.transform.position;
    }

}
