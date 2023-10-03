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
        transform.SetParent(GameObject.Find("Canvas").transform);
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
        transform.position = Camera.main.WorldToScreenPoint(attachedEnemy.transform.position + positionOffset);
    }

    void ShowEnemyHealth()
    {
        transform.GetComponent<Slider>().value = attachedEnemy.GetComponent<EnemyClass>().currentHealthNormalized;
    }

    void DestroyOnEnemyDeath()
    {
        if (attachedEnemy == null)
        {
            Destroy(gameObject);
        }

        if (attachedEnemy.GetComponent<EnemyClass>().isDead)
        {
            Destroy(attachedEnemy);
            Destroy(gameObject);
        }
    }
}
