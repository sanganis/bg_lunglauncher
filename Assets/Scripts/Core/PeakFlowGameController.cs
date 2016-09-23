using UnityEngine;
using System.Collections;

public class PeakFlowGameController : MonoBehaviour
{

    public static PeakFlowPlayerController playerScreen;
    public static PeakFlowMainUIController mainUIController;
    public static MusicController musicController;

    // A class to keep static references for all major game elements, to easily facilitate referencing between classes

    void Awake()
    {
        playerScreen = GameObject.Find("PeakFlowScreen").GetComponent<PeakFlowPlayerController>();
        mainUIController = GameObject.Find("MainUI").GetComponent<PeakFlowMainUIController>();
        musicController = GetComponent<MusicController>();
    }


}
