using UnityEngine;
using System.Collections;

public class LungCharacterController : MonoBehaviour
{
    // the most lives a player can have
    public int maxLives = 5;
    // current number of lives the player has
    public int currentLives = 5;

    public void LoseLives(int lose)
    {
        currentLives -= lose;
        GameController.mainUIController.SetLivesNumber();
        if(currentLives == 0)
        {
            GameController.playerScreen.GameOverOutOfLives();
        }
    }

    public void GainLives(int gain)
    {
        currentLives += gain;        
        if (currentLives > maxLives)
        {
            currentLives = maxLives;
        }
        GameController.mainUIController.SetLivesNumber();
    }
}
