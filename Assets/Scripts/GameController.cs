using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { idle, playing, stopped, ended, readyToReset }

public class GameController : MonoBehaviour
{
    // Background & Parallax
    [Range(0f, 1f)]
    public float parallaxSpeed = 0.001f;
    public float platformBackgroundSpeedRatio = 0.4f;
    public RawImage background;
    public RawImage platform;

    // Game State
    public GameState gameState = GameState.idle;

    // Score System
    public int points = 0;
    public GameObject scoreUI;
    public Text pointText;

    // Player
    public GameObject player;
    // Enemies
    public GameObject enemyGenerator;
    // Difficulty
    public float increaseDifficultyEach = 6f;
    public float increaseDifficultyBy = 0.25f;
    public float deadAnimationTimeScale = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        // Reset time scale (difficulty)
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState == GameState.idle && CheckStartButtons())
        {
            startGame();
        }
        else if (gameState == GameState.playing)
        {
            ParallaxMovement();
        }
        else if (gameState == GameState.ended)
        {
            endGame();
        }
        else if (gameState == GameState.readyToReset)
        {
            resetGame();
        }
    }
    private void startGame()
    {
        gameState = GameState.playing;

        player.SendMessage("isPlaying", true);
        enemyGenerator.SendMessage("StartEnemyGeneration");
        GetComponent<AudioSource>().Play();
        // Difficulty
        InvokeRepeating("increaseDifficulty", increaseDifficultyEach, increaseDifficultyEach);

        // Score UI
        scoreUI.SetActive(true);
        scoreUI.GetComponent<Animation>().Play("UiScoreBeginning");
    }

    private void endGame()
    {
        enemyGenerator.SendMessage("CancelEnemyGeneration", true);
        GetComponent<AudioSource>().Stop();
        Time.timeScale = deadAnimationTimeScale;
    }

    private void resetGame()
    {
        if (CheckStartButtons())
        {
            SceneManager.LoadScene("0_MainScene");
        }
    }

    public static bool CheckStartButtons()
    {
        return Input.GetKeyDown(KeyCode.UpArrow) || Input.GetMouseButtonDown(0);
    }

    private void increaseDifficulty()
    {
        Time.timeScale += increaseDifficultyBy;
    }

    public void increasePoints()
    {
        pointText.text = (++points).ToString();
    }

    void ParallaxMovement()
    {
        float finalSpeed = parallaxSpeed * Time.deltaTime;
        background.uvRect = new Rect(background.uvRect.x + parallaxSpeed, 0f, 1f, 1f);
        platform.uvRect = new Rect(platform.uvRect.x + parallaxSpeed * platformBackgroundSpeedRatio, 0f, 1f, 1f);
    }
}
