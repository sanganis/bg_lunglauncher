using UnityEngine;
using System.Collections;

public class BackgroundColorChange : MonoBehaviour
{

    public PlayerScreenController playerScreen;

    Color startingColor;

    float height = 90f;

    void Start()
    {
        startingColor = Camera.main.backgroundColor;
    }

    void Update()
    {
        if (playerScreen.currentHeight > height + 20)
        {
            Camera.main.backgroundColor *= 0.99f;
            height = playerScreen.currentHeight;
        }
    }
}
