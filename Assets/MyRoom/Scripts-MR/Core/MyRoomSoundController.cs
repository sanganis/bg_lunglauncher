using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomSoundController : MonoBehaviour {

    public static MyRoomSoundController instance;

    AudioSource source;

    public enum MyRoomSounds
    {
        PLACEITEM,
        SCRAPITEM
    }

    public MyRoomSounds soundTrackOrder;

    public AudioClip[] myRoomSounds;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }


    public void PlaySound(int sound)
    {
        source.PlayOneShot(myRoomSounds[sound]);
    }

}
