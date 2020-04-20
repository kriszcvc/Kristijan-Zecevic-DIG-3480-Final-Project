using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float gameTime;
    private float currentTime;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text winText;
    public Text gameTimeText;
    private int score;

    private bool gameOver;
    private bool restart;
    public bool win;
    private bool timeOut;

    public AudioSource audioSource;
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    public AudioClip audioClip3;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        winText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine (SpawnWaves());
        gameTime = 25;
        currentTime = gameTime;
        audioSource.clip = audioClip1;
        audioSource.Play();
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.E))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if (score >= 100)
        {
            win = true;
        }

        if (currentTime > 0 && !win)
        {
            currentTime -= 1 * Time.deltaTime;
            print(currentTime);
            gameTimeText.text = currentTime.ToString("0");
        }

        if (currentTime <= 0)
        {
            timeOut = true;
        }

        if (audioSource.clip == audioClip1)
        {
            audioSource.loop = true;
        }

        if (audioSource.clip == audioClip2 || audioSource.clip == audioClip3)
        {
            audioSource.loop = false;
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }

            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restart = true;
                restartText.text = "Press 'E' to Restart";
                break;
            }

            if (win)
            {
                winText.text = "You win! Game made by Kristijan Zecevic";
                restart = true;
                restartText.text = "Press 'E' to Restart";
                audioSource.clip = audioClip2;
                audioSource.Play();
                break;
            }

            if (timeOut && !gameOver)
            {
                gameOverText.text = "Time's up! Game made by Kristijan Zecevic";
                restart = true;
                restartText.text = "Press 'E' to Restart";
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Points: " + score;
    }

    public void GameOver()
    {
        Destroy(gameTimeText);
        gameOverText.text = "You lose! Game made by Kristijan Zecevic";
        gameOver = true;
    }

    public void LoseMusic()
    {
        audioSource.clip = audioClip3;
        audioSource.Play();
    }
}