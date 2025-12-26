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
    public GameObject endOfLevel;
    private bool gameIsPaused = false;
    public List<int> grades = new List<int>();
    public float mean;
    public TextMeshProUGUI meanText;
    public float behaviour;
    public TextMeshProUGUI behaviourText;
    public TextMeshProUGUI endOfLevelTitleText;
    public TextMeshProUGUI endOfLevelSubtitleText;
    public TextMeshProUGUI endOfLevelBodyText;
    public SpriteRenderer birdSprite;
    public Sprite idleBird;
    public Sprite deadBird;
    public PlayerStats playerStats;
    public GameObject nextLevelButton;




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
                    EndOfLevel();
                }
            }
        }
    }
    public void EndOfLevel()
    {
        Time.timeScale = 0;
        gameIsPaused = true;
        endOfLevel.SetActive(true);
        AudioManager.instance.PlayMusic("ScoreScreen");
        if (behaviour > 5)
        {
            nextLevelButton.SetActive(true);
            PlayerStats.instance.AddToMean(mean);

            if (PlayerStats.instance.currentLevel == 6)
            {
                endOfLevelTitleText.text = "Finalmente maturo!";
                endOfLevelBodyText.color = new Color32(39, 241, 6, 255);
                endOfLevelSubtitleText.text = "\nÈ stata dura ma ce l'hai fatta!";
                endOfLevelBodyText.text = "Voto dell'esame di stato: " + Mathf.RoundToInt(PlayerStats.instance.finalGrade);
            }
            else
            {
                endOfLevelTitleText.text = "Promosso!";
                endOfLevelBodyText.color = new Color32(39, 241, 6, 255);
                endOfLevelSubtitleText.text = "E vai!";
                endOfLevelBodyText.text = "Media dei voti: " + mean.ToString("F1") + "\nCondotta: " + behaviour + "\nMedia totale: " + ((mean + behaviour) / 2).ToString("F1");

            }

        }
        else
        {
            endOfLevelTitleText.text = "Bocciato!";
            endOfLevelTitleText.color = new Color32(241, 40, 6, 255);
            endOfLevelSubtitleText.text = "troppe assenze strategiche: devi farti interrogare di più!";
            endOfLevelBodyText.text = "Media dei voti: " + mean.ToString("F1") + "\nCondotta: " + behaviour + "\nMedia totale: " + ((mean + behaviour) / 2).ToString("F1");
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
        if (numberOfGrades <= 10)
        {
            behaviour = numberOfGrades;
            behaviourText.text = "Condotta: " + numberOfGrades.ToString();
        }
        mean = gradeSum / numberOfGrades;
        meanText.text = "Media dei voti: " + mean.ToString("F1");
    }


    public void RestartGame()
    {
        Time.timeScale = 1;
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        AudioManager.instance.PlayMusic("Level");
    }

    public void GameOver()
    {
        bird.birdIsAlive = false;
        bird.animator.enabled = false;
        birdSprite.sprite = deadBird;
        gameOverScreen.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
        gameIsPaused = false;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        gameIsPaused = false;
        SceneManager.LoadScene("Level" + PlayerStats.instance.currentLevel);
        AudioManager.instance.PlayMusic("Level");
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
