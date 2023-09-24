using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    Transform parentBlock;

    AudioSource coinAudio;
    

    // Start is called before the first frame update
    void Start()
    {
        parentBlock = transform.parent.parent.GetChild(0);
        coinAudio = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioDisableQuestionBlock()
    {
        if (!parentBlock.GetComponent<QuestionBlockBehaviour>()) return;
        coinAudio.PlayOneShot(coinAudio.clip);
        parentBlock.GetComponent<QuestionBlockBehaviour>().hitDisabled = true;
        parentBlock.GetComponent<QuestionBlockBehaviour>().FreezeAllConstraints();
        parentBlock.GetComponent<QuestionBlockBehaviour>().ReturnToOriginalPosition();
    }

    public void PlayAudioDisableBrickBlock()
    {
        if (!parentBlock.GetComponent<BrickBehavior>()) return;
        coinAudio.PlayOneShot(coinAudio.clip);
        parentBlock.GetComponent<BrickBehavior>().coinCollected = true;
    }
}
