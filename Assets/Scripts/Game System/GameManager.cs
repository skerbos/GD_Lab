using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public UnityEvent gameStart;
    public UnityEvent gamePause;
    public UnityEvent gameResume;
    public UnityEvent gameRestart;
    public UnityEvent<int> scoreChange;
    public UnityEvent<int> highScoreChange;
    public UnityEvent gameOver;
    public UnityEvent<int> nextWave;

    public UnityEvent shmupGameStart;
    public UnityEvent shmupGameRestart;
    public UnityEvent shmupGameOver;
    public UnityEvent shmupBackToHome;

    public GameObject enemyManager;

    private int score = 0;
    private int highScore = 0;
    private int currentWave = 1;
    private int enemiesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        enemyManager = GameObject.FindWithTag("Enemy Manager");
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateEnemiesRemaining();
        //NextWave();
        UpdateHighScore();
    }

    public void GameStart()
    {
        gameStart.Invoke();
        Time.timeScale = 1.0f;

        SceneManager.activeSceneChanged += SceneSetup;
    }

    public void GamePause()
    {
        gamePause.Invoke();
        Time.timeScale = 0.0f;
    }

    public void GameResume()
    {
        gameResume.Invoke();
        Time.timeScale = 1.0f;
    }

    public void SceneSetup(Scene current, Scene next)
    {
        shmupGameStart.Invoke();
        SetScore(0);
        SetHighScore(highScore);
    }

    public void GameRestart()
    {
        score = 0;
        SetScore(score);

        currentWave = 1;

        gameRestart.Invoke();

        foreach(GameObject enemyBullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(enemyBullet);
        }

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

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            SetHighScore(highScore);
        }
    }

    public void SetHighScore(int highScore)
    {
        highScoreChange.Invoke(highScore);
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

    public void ShmupGameStart()
    {
        Time.timeScale = 1.0f;
        shmupGameStart.Invoke();

        SceneManager.activeSceneChanged += SceneSetup;

        SetScore(score);
        SetHighScore(score);
    }

    public void ShmupGameRestart()
    {
        Time.timeScale = 1.0f;
        shmupGameRestart.Invoke();


        if (highScore < score)
        {
            highScore = score;
            SetHighScore(highScore);
        }

        score = 0;
        SetScore(score);
    }

    public void ShmupGameOver()
    {
        Time.timeScale = 0.0f;
        shmupGameOver.Invoke();


        if (highScore < score)
        {
            highScore = score;
            SetHighScore(highScore);
        }
    }

    public void ShmupBackToHome()
    {
        Time.timeScale = 1.0f;
        shmupBackToHome.Invoke();

        SceneManager.activeSceneChanged += SceneSetup;

        SetScore(0);
        SetHighScore(highScore);
    }


    void UpdateEnemiesRemaining()
    {
        enemiesRemaining = enemyManager.transform.childCount;
    }
}
