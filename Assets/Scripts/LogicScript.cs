using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public int passedWeeks;
    public int monthLenght = 2;
    private int indexOfCurrentMonth = 0;
    private string[] monthNames = { "Settembre", "Ottobre", "Novembre", "Dicembre", "Gennaio", "Febbraio", "Marzo", "Aprile", "Maggio", "Giugno" };
    public TextMeshProUGUI currentMonth;
    public GameObject gameOverScreen;
    public BirdScript bird;
    public GameObject pauseMenu;
    public GameObject winScreen;
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

    public void AddWeek(int week)
    {
        if (bird.birdIsAlive)
        {
            passedWeeks += week;
            if (passedWeeks == monthLenght)
            {
                if (indexOfCurrentMonth < 9)
                {
                    passedWeeks = 0;
                    indexOfCurrentMonth += 1;
                    currentMonth.text = monthNames[indexOfCurrentMonth];
                }
                else
                {
                    Time.timeScale = 0;
                    winScreen.SetActive(true);
                }

            }
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
        Time.timeScale = 1;
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
