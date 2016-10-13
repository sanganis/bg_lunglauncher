using UnityEngine;
using System.Collections;

public class LungCharacterController : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {


        }
    }
}
