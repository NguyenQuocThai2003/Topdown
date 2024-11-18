using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverCanvas; 
    private bool isGameOver = false;

   
    public void TriggerGameOver()
    {
        if (isGameOver) return; 
        isGameOver = true;
        Time.timeScale = 0f; 
        gameOverCanvas.SetActive(true); 
    }

    
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("MainMenu"); 
    }
}
