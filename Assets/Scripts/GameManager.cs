using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int highScore;
    public int score;

    [SerializeField] private int currentLevel = 0;

    public static GameManager instance;

    private void Start()
    {
        FindObjectOfType<UIManager>().ShowLevelText(currentLevel);
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("destroy");
            Destroy(gameObject);
        }

        highScore = PlayerPrefs.GetInt("Highscore");
    }

    public void NextLevel()
    {
        currentLevel++;
        FindObjectOfType<Ball>().ResetBall();
        FindObjectOfType<HelixController>().LoadLevel(currentLevel);
        FindObjectOfType<UIManager>().ShowLevelText(currentLevel);
    }

    public void RestartLevel()
    {
        instance.score = 0;
        FindObjectOfType<Ball>().ResetBall();
        FindObjectOfType<UIManager>().ShowLevelText(currentLevel);
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("Highscore", highScore);
        }
    }
}
