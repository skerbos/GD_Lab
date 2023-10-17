using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public IntVariable gameScore;
    public IntVariable highScore;
    //public UnityEvent gameStart;
    //public UnityEvent gamePause;
    //public UnityEvent gameResume;
    //public UnityEvent gameRestart;
    //public UnityEvent<int> scoreChange;
    //public UnityEvent<int> highScoreChange;
    //public UnityEvent gameOver;
    //public UnityEvent<int> nextWave;

    public UnityEvent shmupGameStart;
    //public UnityEvent shmupGameRestart;
    //public UnityEvent shmupGameOver;
    //public UnityEvent shmupBackToHome;

    //ublic GameObject enemyManager;

    //private int score = 0;
    //private int highScore = 0;
    private int currentWave = 1;
    private int enemiesRemaining;

    public UnityEvent updateScore;
    public UnityEvent updateHighScore;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1.0f;
        gameScore.Value = 0;
        //enemyManager = GameObject.FindWithTag("Enemy Manager");
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
        //gameStart.Invoke();
        shmupGameStart.Invoke();
        Time.timeScale = 1.0f;

        SceneManager.activeSceneChanged += SceneSetup;
    }

    public void GamePause()
    {
        //gamePause.Invoke();
        Time.timeScale = 0.0f;
    }

    public void GameResume()
    {
        //gameResume.Invoke();
        Time.timeScale = 1.0f;
    }

    public void SceneSetup(Scene current, Scene next)
    {
        shmupGameStart.Invoke();
        //SetScore(0);
        //SetHighScore(highScore.Value);
        gameScore.Value = 0;
        updateScore.Invoke();
        updateHighScore.Invoke();

    }

    public void GameRestart()
    {
        gameScore.Value = 0;
        //SetScore(score);
        updateScore.Invoke();

        //gameRestart.Invoke();

        foreach(GameObject enemyBullet in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(enemyBullet);
        }

        Time.timeScale = 1.0f;
    }

    public void IncreaseScore(int increment)
    {
        //score += increment;
        //SetScore(score);
        gameScore.ApplyChange(increment);
        updateScore.Invoke();
    }

    public void SetScore(int score)
    {
        //scoreChange.Invoke(score);
        
    }

    public void UpdateHighScore()
    {
        if (gameScore.Value > highScore.Value)
        {
            highScore.Value = gameScore.Value;
            //SetHighScore(highScore);
            updateScore.Invoke();
        }
    }

    public void SetHighScore(int highScore)
    {
        //highScoreChange.Invoke(highScore);
    }


    public void GameOver()
    {
        Time.timeScale = 0.0f;
        //gameOver.Invoke();
    }

    public void NextWave()
    {
        if (enemiesRemaining == 0)
        {
            currentWave++;
            //nextWave.Invoke(currentWave);
        }
    }

    public void ShmupGameStart()
    {
        Time.timeScale = 1.0f;
        shmupGameStart.Invoke();

        SceneManager.activeSceneChanged += SceneSetup;

        //SetScore(score);
        //SetHighScore(score);
        updateScore.Invoke();
    }

    public void ShmupGameRestart()
    {
        Time.timeScale = 1.0f;
        //shmupGameRestart.Invoke();


        if (highScore.Value < gameScore.Value)
        {
            highScore.Value = gameScore.Value;
            //SetHighScore(highScore);
            updateHighScore.Invoke();
        }

        gameScore.Value = 0;
        //SetScore(score);
        updateScore.Invoke();
    }

    public void ShmupGameOver()
    {
        Time.timeScale = 0.0f;
        //shmupGameOver.Invoke();


        if (highScore.Value < gameScore.Value)
        {
            highScore.Value = gameScore.Value;
            //SetHighScore(highScore);
            updateHighScore.Invoke();
        }
    }

    public void ShmupBackToHome()
    {
        Time.timeScale = 1.0f;
        //shmupBackToHome.Invoke();

        SceneManager.activeSceneChanged += SceneSetup;

        //SetScore(0);
        gameScore.Value = 0;
        updateScore.Invoke();

        updateHighScore.Invoke();
    }


    void UpdateEnemiesRemaining()
    {
        //enemiesRemaining = enemyManager.transform.childCount;
    }
}
