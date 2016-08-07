using UnityEngine;
using System.Collections;

public class EnemyBaseController : MonoBehaviour {

    public bool spawnsAtBottom, spawnsAtRightSide, spawnsAtTop;

    [HideInInspector]
    public Rigidbody2D rb;

	
	public virtual void Start () {

        rb = GetComponent<Rigidbody2D>();	
	}
	


    public virtual void Movement()
    {
        
    }

}
