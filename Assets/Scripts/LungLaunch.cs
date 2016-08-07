using UnityEngine;
using System.Collections;

public class LungLaunch : MonoBehaviour {

    Animator anim;
    AudioSource source;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Fire");
            source.Play();
        }
	}
}
