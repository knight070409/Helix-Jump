using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private TextMeshProUGUI scoreGameOverText;
    [SerializeField] private TextMeshProUGUI highScoreGameOverText;


    [SerializeField] private TextMeshProUGUI highScoreMainMenuText;
    [SerializeField] private GameObject startPanel;

    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        scoreText.text = "  Score: " + GameManager.instance.score;
        highScoreText.text = "  High Score: " + GameManager.instance.highScore;
        highScoreMainMenuText.text = "  High Score: " + GameManager.instance.highScore;
    }

    public void ShowLevelText(int level)
    {
        StartCoroutine(ShowLevelMessage("Level " + (level+1)));
    }

    private IEnumerator ShowLevelMessage(string message)
    {
        levelText.text = message;
        levelText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        levelText.gameObject.SetActive(false);
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);
        highScoreText.gameObject.SetActive(false);

        scoreGameOverText.text = "  Score: " + GameManager.instance.score;
        highScoreGameOverText.text = "  High Score: " + GameManager.instance.highScore;

        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        GameManager.instance.RestartLevel();
        gameOverPanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
    }

    public void Play()
    {
        startPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }

    public void Home()
    {
        startPanel.SetActive(true);
        Time.timeScale = 0f;
        GameManager.instance.RestartLevel();
        gameOverPanel.SetActive(false);
        scoreText.gameObject.SetActive(true);
        highScoreText.gameObject.SetActive(true);
    }
}
