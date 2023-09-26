using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    Transform parentBlock;

    AudioSource coinAudio;

    private GameManager gameManager; 

    // Start is called before the first frame update
    void Start()
    {
        parentBlock = transform.parent.parent.GetChild(0);
        coinAudio = transform.GetComponent<AudioSource>();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
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

        gameManager.IncreaseScore(1);
    }

    public void PlayAudioDisableBrickBlock()
    {
        if (!parentBlock.GetComponent<BrickBehavior>()) return;
        coinAudio.PlayOneShot(coinAudio.clip);
        parentBlock.GetComponent<BrickBehavior>().coinCollected = true;

        gameManager.IncreaseScore(1);
    }
}
