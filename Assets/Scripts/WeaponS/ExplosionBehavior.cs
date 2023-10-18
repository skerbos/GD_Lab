using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBehavior : MonoBehaviour
{
    public AudioSource explosionAudio;
    public AudioClip explosionClip;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayExplosionAudio()
    { 
        //explosionAudio.PlayOneShot(explosionClip);
    }

    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }
}
