using UnityEngine;
using System.Collections;

public class EnemyBaseController : MonoBehaviour {

    // a base controller for all enemies. not fully utilized yet but eventually could be useful

    [HideInInspector]
    public Rigidbody2D rb;

	
	public virtual void Start () {

        rb = GetComponent<Rigidbody2D>();	
	}
	


    public virtual void Movement()
    {
        
    }

}
