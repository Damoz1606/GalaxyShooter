using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject uiManager;
    [SerializeField] private GameObject pauseManager;
    [SerializeField] public bool isCoopMode = false;
    private bool gameOver = true;

    private void Update()
    {
        this.UpdateGameOver();
        this.GameCycle();
    }

    private void GameCycle()
    {
        if(!this.gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.HideTitleScreen();
                this.InitPlayer();
            } else if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MainMenu");
            }
            if(Input.GetKeyDown(KeyCode.P))
            {
                this.pauseManager.SetActive(true);
                Animator pauseAnimation = GameObject.Find("PauseMenu").GetComponent<Animator>();
                pauseAnimation.updateMode = AnimatorUpdateMode.UnscaledTime;
                pauseAnimation.SetBool("isPause", true);
                Time.timeScale = 0;
            }
        }
    }

    private void InitPlayer()
    {
        if (!this.isCoopMode)
        {
            this.InitPlayerSingleMode();
        }
    }

    private void InitPlayerSingleMode()
    {
        Instantiate(this.player, Vector3.zero, Quaternion.identity);
    }

    private void HideTitleScreen()
    {
        if (this.uiManager.GetComponent<UIManager>())
        {
            this.uiManager.GetComponent<UIManager>().HideTitleScreen();
        }
    }
    public void UpdateGameOver()
    {
        this.gameOver = !this.gameOver;
    }
}
