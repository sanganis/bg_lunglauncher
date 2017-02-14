using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomPlaceableItemController : MonoBehaviour {
          

    public enum ItemID
    {
        HAT,
        SUNGLASSES,
        BIKE,
        CAT,
        CHAIR,
        DOG,
        LAMP,
        BALL
    }

    public ItemID itemID;

    public int cost;

    SpriteRenderer rend;

    [HideInInspector]
    public bool placing;

    public bool itemScalesWithHeight;

    float minScaleSize = 0.5f;
    float maxScaleSize =  1.5f;
    float scaleRate = 0.02f;

    float lastScaledHeight;

    
    void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
    }


    void Update()
    {
        if (placing && itemScalesWithHeight)
        {
            ScaleImageAccordingToHeight();
        }
    }


    public void ScaleImageAccordingToHeight()
    {
        if(transform.position.y > lastScaledHeight)
        {
            transform.localScale = new Vector3(transform.localScale.x - scaleRate, transform.localScale.y - scaleRate, transform.localScale.z - scaleRate);

            if (transform.localScale.x < minScaleSize)
            {
                transform.localScale = new Vector3(minScaleSize, minScaleSize, minScaleSize);
            }
        }       
        if (transform.position.y < lastScaledHeight)
        {
            transform.localScale = new Vector3(transform.localScale.x + scaleRate, transform.localScale.y + scaleRate, transform.localScale.z + scaleRate);
            if (transform.localScale.x > maxScaleSize)
            {
                transform.localScale = new Vector3(maxScaleSize, maxScaleSize, maxScaleSize);
            }
        }        
        lastScaledHeight = transform.position.y;
    }


    /* 
     * make a method to convert the games local y dimension into a %, so I can more effectively do scaling and sprite layers
    void CalculateRoomHeight()
    {

    }
    */
}
