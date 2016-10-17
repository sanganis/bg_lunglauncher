using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{

    public float speed = 0.1F;

    
    // Update is called once per frame
    void Update()
    {        

        MouseClickDetect();
    }

    void TouchDetect()
    {
        Vector3 touchPosWorld;
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchPosWorld = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPosWorld2D = new Vector2(touchPosWorld.x, touchPosWorld.y);
            RaycastHit2D hitInformation = Physics2D.Raycast(touchPosWorld2D, Camera.main.transform.forward);

            if (hitInformation.collider != null)
            {
                if (hitInformation.transform.gameObject.tag == "Enemy")
                {
                    hitInformation.transform.gameObject.GetComponent<EnemyBaseController>().TapDamage();
                }
                if (hitInformation.transform.gameObject.tag == "Powerup")
                {
                    hitInformation.transform.gameObject.GetComponent<PowerupBaseController>().TapDamage();
                }
            }
        }
    }

    void MouseClickDetect()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.transform.gameObject.tag == "Enemy")
                {
                    hit.transform.gameObject.GetComponent<EnemyBaseController>().TapDamage();
                }
                if (hit.transform.gameObject.tag == "Powerup")
                {
                    hit.transform.gameObject.GetComponent<PowerupBaseController>().TapDamage();
                }
            }
        }
    }

}
