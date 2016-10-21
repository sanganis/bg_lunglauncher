using UnityEngine;
using System.Collections;

public class BackgroundColorChange : MonoBehaviour
{

    public PlayerScreenController playerScreen;

    Color startingColor;

    
    void Start()
    {
        startingColor = Camera.main.backgroundColor;
    }

    void Update()
    {
        if (playerScreen.currentHeight > GameController.spawnerController.spawnStarsHeight)
        {
            Camera.main.backgroundColor *= 0.995f;            
        }
    }
}
