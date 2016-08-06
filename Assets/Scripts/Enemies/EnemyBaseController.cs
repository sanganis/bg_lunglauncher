using UnityEngine;
using System.Collections;

public class EnemyBaseController : MonoBehaviour {

    [HideInInspector]
    public Rigidbody2D rb;

	// Use this for initialization
	public virtual void Start () {

        rb = GetComponent<Rigidbody2D>();	
	}
	


    public virtual void Movement()
    {
        
    }

}
