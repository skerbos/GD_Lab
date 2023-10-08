using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{

    public UnityEvent gameStart;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent gameOver;
    public UnityEvent<int> nextWave;

    public GameObject enemyManager;

    private int score = 0;
    private int currentWave = 1;
    private int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEnemiesRemaining();
        NextWave();
    }

    public void GameStart()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;
    }

    public void GameRestart()
    {
        score = 0;
        SetScore(score);

        currentWave = 1;

        gameRestart.Invoke();

        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        score += increment;
        SetScore(score);
    }

    public void SetScore(int score)
    {
        scoreChange.Invoke(score);
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        gameOver.Invoke();
    }

    public void NextWave()
    {
        if (enemiesRemaining == 0)
        {
            currentWave++;
            nextWave.Invoke(currentWave);
        }
    }

    void UpdateEnemiesRemaining()
    {
        enemiesRemaining = enemyManager.transform.childCount;
    }
}
