using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

    public AudioSource source;

    public AudioClip startingFanfare;
    public AudioClip music;    
    public AudioClip victoryJingle;
    public AudioClip failureJingle;

    public static MusicController instance;

    void Awake()
    {
        instance = this;
    }

    	
	void Start ()
    {
        source.PlayOneShot(startingFanfare);
        CheckForStartingJingleFinish();
	}
	
    void CheckForStartingJingleFinish()
    {
        if (source.isPlaying)
        {
            Invoke("CheckForStartingJingleFinish", 0.1f);
            return;
        }
        else
        {
            source.PlayOneShot(music);
            source.loop = true;
        }
    }

    public void PlayVictoryJingle()
    {
        source.PlayOneShot(victoryJingle);
        source.loop = false;
    }

    public void PlayFailureJingle()
    {
        source.PlayOneShot(failureJingle);
        source.loop = false;
    }

}
