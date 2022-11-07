using UnityEngine;
using UnityEngine.UI;

public class ScoreAndLives : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text roundsText;
    public GameManager gameManger;
    private void Update()
    {
        scoreText.text = "Score: " + this.gameManger.score;
        livesText.text = "Lives: " + this.gameManger.lives;
        roundsText.text ="Rounds: "+this.gameManger.rounds;
    }
}
