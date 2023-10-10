using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    private Vector3[] scoreTextPosition = {
        new Vector3(-747, 473, 0),
        new Vector3(0,0,0)
    };

    private Vector3[] restartButtonPosition = {
        new Vector3(844, 455, 0),
        new Vector3(0,-150,0)
    };

    private Vector3 titleLogoPosition = new Vector3(0, 132, 0);
    private Vector3 startButtonPosition = new Vector3(0, -70, 0);

    private Vector3 pistolSelectUIPosition = new Vector3(-750, 370, 0);
    private Vector3 smgSelectUIPosition = new Vector3(-750, 370, 0);

    public GameObject scoreText;
    public GameObject gameOverText;
    public Transform restartButton;


    public Image titleLogo;
    public Button startButton;

    public GameObject waveCounter;

    public GameObject healthbar;

    public GameObject gameOverPanel;

    private void Awake()
    {
        GameManager.instance.gameStart.AddListener(GameStart);
        GameManager.instance.gameOver.AddListener(GameOver);
        GameManager.instance.gameRestart.AddListener(GameStart);
        GameManager.instance.scoreChange.AddListener(SetScore);
        GameManager.instance.nextWave.AddListener(NextWave);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameStart()
    {
        gameOverPanel.SetActive(false);
        gameOverText.SetActive(false);

        scoreText.SetActive(true);
        waveCounter.GetComponent<TextMeshProUGUI>().text = "Wave 1";
        waveCounter.SetActive(true);

        titleLogo.enabled = false;
        startButton.enabled = false;
        startButton.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().enabled = false;

        scoreText.transform.localPosition = scoreTextPosition[0];
        restartButton.localPosition = restartButtonPosition[0];

        DisplayEnemyHealthbars();

    }

    public void SetScore(int score)
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.SetActive(true);

        scoreText.transform.localPosition = scoreTextPosition[1];
        restartButton.localPosition = restartButtonPosition[1];
    }

    public void NextWave(int currentWave)
    {
        waveCounter.GetComponent<TextMeshProUGUI>().text = "Wave " + currentWave.ToString();

        DisplayEnemyHealthbars();
    }

    public void DisplayEnemyHealthbars()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            GameObject healthbarClone = Instantiate(healthbar, transform);
            healthbarClone.GetComponent<HealthBarBehavior>().attachedEnemy = enemy;
        }
    }
}
