using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarBehavior : MonoBehaviour
{

    public GameObject attachedEnemy;

    private Vector3 positionOffset = new Vector3(0, 0.7f, 0);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeHealthbar());

        gameObject.SetActive(true);
        //transform.SetParent(GameObject.Find("Canvas").transform);
    }

    // Update is called once per frame
    void Update()
    {
        DestroyOnEnemyDeath();
        FollowAttachedEnemy();
        ShowEnemyHealth();
    }

    void FollowAttachedEnemy()
    {
        if (attachedEnemy == null) return;
        transform.position = Camera.main.WorldToScreenPoint(attachedEnemy.transform.position + positionOffset);
    }

    void ShowEnemyHealth()
    {
        if (attachedEnemy == null) return;
        transform.GetComponent<Slider>().value = attachedEnemy.GetComponent<EnemyClass>().currentHealthNormalized;
    }

    void DestroyOnEnemyDeath()
    {
        if (gameObject == null) return;

        if (attachedEnemy != null)
        {
            if (attachedEnemy.GetComponent<EnemyClass>().isDead)
            {
                Destroy(attachedEnemy);
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }

        /*
        if (attachedEnemy == null)
        {
            Destroy(gameObject);
            return;
        }

        if (attachedEnemy.GetComponent<EnemyClass>().isDead)
        {
            Destroy(attachedEnemy);
            Destroy(gameObject);
        }*/
    }

    IEnumerator InitializeHealthbar()
    {
        gameObject.SetActive(false);
        transform.SetParent(GameObject.Find("Canvas").transform);
        yield return new WaitForSecondsRealtime(0.1f);
    }
}
