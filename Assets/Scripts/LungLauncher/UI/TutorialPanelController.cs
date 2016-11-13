using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialPanelController : MonoBehaviour {

    public GameObject panel1;
    public GameObject panel2;

    public Text panel1Text;
    public Text panel2Text;

    public string message1, message2, message3, message4;

    public float messageDuration = 2f;
    public float timeBetweenMessages = 1f;
	
	void Start ()
    {
        if (PlayerPrefs.HasKey("LungLauncherRun") == false)
        {
            StartCoroutine("RunTutorial");
            PlayerPrefs.SetInt("LungLauncherRun", 1);
        }
        else
        {
            PlayerPrefs.SetInt("LungLauncherRun", (PlayerPrefs.GetInt("LungLauncherRun") + 1));
        }

        if(PlayerPrefs.GetInt("LungLauncherRun") >= 3)
        {
            PlayerPrefs.DeleteKey("LungLauncherRun");
        }
    }

    IEnumerator RunTutorial()
    {
        yield return new WaitForSeconds(timeBetweenMessages);
        panel1.SetActive(true);
        panel1Text.text = message1;
        yield return new WaitForSeconds(messageDuration);
        panel1.SetActive(false);
        yield return new WaitForSeconds(timeBetweenMessages);
        panel2.SetActive(true);
        panel2Text.text = message2;
        yield return new WaitForSeconds(messageDuration);
        panel2.SetActive(false);
        yield return new WaitForSeconds(timeBetweenMessages);
        panel1.SetActive(true);
        panel1Text.text = message3;
        yield return new WaitForSeconds(messageDuration);
        panel1.SetActive(false);
        yield return new WaitForSeconds(timeBetweenMessages);
        panel2.SetActive(true);
        panel2Text.text = message4;
        yield return new WaitForSeconds(messageDuration);
        panel2.SetActive(false);
    }

    
}
