using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFish : EnemyClass
{
    public List<GameObject> powerUps;

    // Start is called before the first frame update
    void Start()
    {
        //gameManager = GameManager.instance;
        hudManager = GameObject.Find("Canvas").GetComponent<HUDManager>();
        hudManager.CreateHealthBar(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        rb.velocity = new Vector2(-Mathf.Cos(Time.fixedTime * 7 + 2) * 3 - moveSpeed, Mathf.Sin(Time.fixedTime * 5) * 7);
    }

    public override void Attack()
    {

    }

    public override void Death()
    {

        score.Value += 100;
        updateScore.Invoke();
        updateHighScore.Invoke();

        GameObject deathParticleClone = Instantiate(deathParticles, transform.position, transform.rotation);
        Destroy(deathParticleClone, 0.5f);

        Instantiate(powerUps[Random.Range(0, powerUps.Count)], transform.position, transform.rotation);

        Camera.main.GetComponent<CameraBehavior>().ShakeCamera(0.1f, 0.5f);

        isDead = true;
    }
}
