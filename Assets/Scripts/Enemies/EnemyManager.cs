using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int startingNumberOfEnemies = 5;
    public int currentWaveNumberOfEnemies;
    public int enemyNumberIncrement = 2;

    public List<GameObject> Enemies;

    public float enemySpawnFrequency = 10f;
    private float lastSpawnedTime;

    private bool shmupGameStarted = false;
    private void Awake()
    {
        //GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.shmupGameStart.AddListener(ShmupGameStart);
        //GameManager.instance.gameRestart.AddListener(GameRestart);
        GameManager.instance.shmupGameRestart.AddListener(ShmupGameRestart);
        //GameManager.instance.gameOver.AddListener(GameRestart);
        GameManager.instance.shmupGameOver.AddListener(ShmupGameRestart);
        GameManager.instance.nextWave.AddListener(NextWave);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWaveNumberOfEnemies = startingNumberOfEnemies;
        lastSpawnedTime = Time.time;

        GameManager.instance.shmupGameStart.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        ShmupEnemySpawner();
    }

    public void GameStart()
    {
        currentWaveNumberOfEnemies = startingNumberOfEnemies;

        for (int i = 0; i < currentWaveNumberOfEnemies; i++)
        {
            GameObject enemyClone = Instantiate(Enemies[0], transform);
            enemyClone.transform.position = RandomEnemySpawnLocation();
        }
    }

    public void ShmupGameStart()
    {
        shmupGameStarted = true;
    }

    public void GameRestart()
    {
       
        currentWaveNumberOfEnemies = startingNumberOfEnemies;

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<EnemyClass>().isDead = true;
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < currentWaveNumberOfEnemies; i++)
        {
            GameObject enemyClone = Instantiate(Enemies[0], transform);
            enemyClone.transform.position = RandomEnemySpawnLocation();
        }
    }

    public void ShmupGameRestart()
    {
        shmupGameStarted = true;

        foreach (Transform child in transform)
        {
            child.gameObject.GetComponent<EnemyClass>().isDead = true;
        }

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void NextWave(int currentWave)
    {
        currentWaveNumberOfEnemies = startingNumberOfEnemies + currentWave * enemyNumberIncrement;

        for (int i = 0; i < currentWaveNumberOfEnemies; i++)
        {
            GameObject enemyClone = Instantiate(Enemies[0], transform);
            enemyClone.transform.position = RandomEnemySpawnLocation(); 
        }
    }

    Vector2 RandomEnemySpawnLocation()
    {
        Vector2 randomSpawnPosition = new Vector2(15, Random.Range(-6, 6));

        return randomSpawnPosition;
    }

    void ShmupEnemySpawner()
    {
        if (!shmupGameStarted) return;

        if (Time.time > lastSpawnedTime + enemySpawnFrequency)
        {
            lastSpawnedTime = Time.time;
            StartCoroutine(SingleLineSpawn()); 
        }
    }

    IEnumerator SingleLineSpawn()
    {
        Vector2 spawnPosition = RandomEnemySpawnLocation();
        for (int i = 0; i < currentWaveNumberOfEnemies; i++)
        {
            GameObject enemyClone = Instantiate(Enemies[Random.Range(0, Enemies.Count)], spawnPosition, transform.rotation);
            enemyClone.transform.SetParent(transform);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }

    IEnumerator RandomPosSpawn()
    {
        for (int i = 0; i < currentWaveNumberOfEnemies; i++)
        {
            GameObject enemyClone = Instantiate(Enemies[Random.Range(0, Enemies.Count)], RandomEnemySpawnLocation(), transform.rotation);
            enemyClone.transform.SetParent(transform);
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }


}
