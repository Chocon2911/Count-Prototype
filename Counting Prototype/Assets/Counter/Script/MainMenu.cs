using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //CountText in PlayerControl.cs
    [SerializeField] Animator menuAnimation;
    [SerializeField] GameObject restartButton;
    [SerializeField] GameObject menuButton;
    [SerializeField] GameObject gameOverText;
    [SerializeField] TMP_Text timeText;

    public bool gameOver { get; private set; }
    
    public bool menuIsOn;  

    private void Start()
    {
        menuIsOn = false;
        StartCoroutine(CountDown(60));
        gameOverText.SetActive(false);

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && gameOver == false)
        {
            if (menuIsOn == false)
            {
                menuAnimation.SetBool("isPlay", true);
                menuIsOn = true;
                restartButton.SetActive(true);
                menuButton.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                menuAnimation.SetBool("isPlay", false);
                menuIsOn = false;
                restartButton.SetActive(false);
                menuButton.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        if (gameOver == true)
        {
            Time.timeScale = 0f;
            gameOverText.SetActive(true);
            menuAnimation.SetBool("isPlay", true);
            restartButton.SetActive(true);
            menuButton.SetActive(true);
        }
    }
    public void RestartButtonClicked()
    {
            Time.timeScale = 1f;
            menuIsOn = false;
            gameOver = false;
            SceneManager.LoadScene(1);
    }
    public void MenuButtonClicked()
    {
            Time.timeScale = 1f;
            menuIsOn = false;
            gameOver = false;
            SceneManager.LoadScene(0);     
    }
    IEnumerator CountDown(int time)
    {
        for(int i = time; i >= 0; i--)
        {
            yield return new WaitForSeconds(1f);
            timeText.text = "Time: " + i;
            if(i == 0)
            {
                gameOver = true;
            }
        }
    }
}
