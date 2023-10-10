using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public int startingNumberOfEnemies = 5;
    public int currentWaveNumberOfEnemies;
    public int enemyNumberIncrement = 2;

    public List<GameObject> Enemies;


    private void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameRestart.AddListener(GameRestart);
        GameManager.instance.gameOver.AddListener(GameRestart);
        GameManager.instance.nextWave.AddListener(NextWave);
    }

    // Start is called before the first frame update
    void Start()
    {
        currentWaveNumberOfEnemies = startingNumberOfEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        Vector2 randomSpawnPosition = new Vector2(Random.Range(-25, 45), Random.Range(6, 7));

        return randomSpawnPosition;
    }
}
