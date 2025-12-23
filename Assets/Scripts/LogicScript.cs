using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int playerScore;
    public TextMeshProUGUI scoreText;
    public GameObject gameOverScreen;
    public BirdScript bird;
    public GameObject pauseMenu; // Trascina qui il tuo pannello UI nell'Inspector
    private bool gameIsPaused = false;

    public List<int> grades = new List<int>();
    public TextMeshProUGUI mean;

    void Update()
    {
        // Controlla se l'utente preme il tasto Esc o P
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (gameIsPaused)
                Resume();
            else
                Pause();
        }
    }

    public void AddScore(int score)
    {
        if (bird.birdIsAlive)
        {
            playerScore += score;
            scoreText.text = playerScore.ToString();
        }
    }
    public void AddGradeToMean(int grade)
    {
        grades.Add(grade);
        float gradeSum = 0;
        float numberOfGrades = 0;
        foreach (var g in grades)
        {
            numberOfGrades += 1;
            gradeSum += g;
        }
        mean.text = "media: " + (gradeSum / numberOfGrades).ToString("F1");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void GameOver()
    {
        bird.birdIsAlive = false;
        gameOverScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }




    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }






}
