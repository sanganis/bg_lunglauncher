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
        BALL,
        BABYBUG,
        PETBUG,
        TEDDYBEAR,
        SHORTS_GREEN,
        SKIRT_PINK,
        TSHIRT_BG,
        TSHIRT_TOC
    }

    public ItemID itemID;

    public int cost;

    SpriteRenderer rend;

    [HideInInspector]
    public bool placing;

    public bool itemScalesWithHeight;
    float heightFactor;
    float totalHeight = 15;
    float minScaleSize = 0.5f;
    float maxScaleSize = 1f;


    void Start()
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        if (itemScalesWithHeight)
        {
            ScaleImageAccordingToHeight();
        }
    }


    void Update()
    {
        if (placing && itemScalesWithHeight)
        {                       
           ScaleImageAccordingToHeight();
        }
    }

    void ScaleImageAccordingToHeight()
    {
        if (transform.position.y < 0)
        {
            heightFactor = (transform.position.y / 100) * ((totalHeight - transform.position.y) / 2);
            Vector3 newScale = new Vector3(heightFactor, heightFactor, heightFactor);
            newScale *= -1;
            transform.localScale = newScale;
            CalculateSpriteSortingOrder(newScale);            
        }
       
            if(transform.localScale.x < minScaleSize)
        {
            transform.localScale = new Vector3(minScaleSize, minScaleSize, minScaleSize);
        }
            if(transform.localScale.x > maxScaleSize)
        {
            transform.localScale = new Vector3(maxScaleSize, maxScaleSize, maxScaleSize);
        }
        
    }

    void CalculateSpriteSortingOrder(Vector3 roomHeight)
    {
        int sortingOrder = Mathf.RoundToInt(roomHeight.x * 100);
        rend.sortingOrder = sortingOrder;
    }




    
}
