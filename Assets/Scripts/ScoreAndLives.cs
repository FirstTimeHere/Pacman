using UnityEngine;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreAndLives : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text roundsText;
    public Text recordText;
    public Text gameOver;
    public GameManager gameManger;

    private void Update()
    {
        if (this.gameManger.lives <= 0)
        {
            gameOver.enabled = true;
            gameOver.text = "Game Over!\n Press any key \nto start the game again";
        }
        else
        {
            gameOver.enabled = false;
        }
        if (this.gameManger.score > this.gameManger.recordScore) 
        {
            recordText.text = "Best: " + this.gameManger.score;
        }
        else
        {
            recordText.text = "Best: " + this.gameManger.recordScore;
        }
        scoreText.text = "Score: " + this.gameManger.score;
        livesText.text = "Lives: " + this.gameManger.lives;
        roundsText.text ="Rounds: "+this.gameManger.rounds;
       
    }


}



