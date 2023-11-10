using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Text playerWon;
    public Text caseWon;

    public void Setup(PlayerController playerController, bool hasWon)
    {
        Time.timeScale = 0;
        gameObject.SetActive(true);
        if(playerController.gameObject.name == "Player1" && !hasWon) {
            playerWon.text = "Player 2 won!";
            caseWon.text = "Player 1 has\nfallen!!!";
        } 
        else if(playerController.gameObject.name == "Player2" && !hasWon)
        {
            playerWon.text = "Player 1 won!";
            caseWon.text = "Player 2 has\nfallen!!!";
        }
        if(hasWon)
        {
            if(playerController.gameObject.name == "Player1")
            {
                playerWon.text = "Player 1 won!";
                caseWon.text = "Player 1 has\nreached the final!!!";
            }
            else if(playerController.gameObject.name == "Player2")
            {
                playerWon.text = "Player 2 won!";
                caseWon.text = "Player 2 has\nreached the final!!!";
            }
        }
    }

    public void RestartButton() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Prototype 1");
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
