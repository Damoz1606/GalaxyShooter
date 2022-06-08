using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Sprite[] lives;
    [SerializeField] private Image uiLives;
    [SerializeField] private Text uiScore;
    [SerializeField] private GameObject titleScreen;
    [SerializeField] private GameObject pauseScreen;

    private string SCORE_TEXT = "Score: ";
    private float score = 0;
    public void UpdateLives(int currentLives)
    {
        this.uiLives.sprite = this.lives[currentLives]; ;
    }

    public void UpdateScore(float score)
    {
        this.score += score;
        this.uiScore.text = SCORE_TEXT + this.score.ToString();
    }

    public void ShowTitleScreen()
    {
        this.titleScreen.SetActive(true);
        this.score = 0;
        this.UpdateScore(this.score);
    }

    public void HideTitleScreen()
    {
        this.titleScreen.SetActive(false);
        this.score = 0;
        this.UpdateScore(this.score);
    }

    public void ResumePlay()
    {
        Animator pauseAnimation = GameObject.Find("PauseMenu").GetComponent<Animator>();
        pauseAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
        pauseAnimation.SetBool("isPause", true);
        this.pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnMainMenu()
    {
        Animator pauseAnimation = GameObject.Find("PauseMenu").GetComponent<Animator>();
        pauseAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
        pauseAnimation.SetBool("isPause", true);
        SceneManager.LoadScene("MainMenu");
    }
}
