using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyRoomPlayerController : MonoBehaviour {

    SpriteRenderer[] rend;
    public Transform spriteTrans;

    public bool placing;
    float heightFactor;
    float totalHeight = 15;
    float minScaleSize = 0.8f;
    float maxScaleSize = 1f;


    void Start()
    {
        rend = GetComponentsInChildren<SpriteRenderer>();        
        //ScaleImageAccordingToHeight();
    }

    
    void Update()
    {
        if (placing)
        {
            //ScaleImageAccordingToHeight();
        }
    }

    void ScaleImageAccordingToHeight()
    {
        if (transform.position.y < 0)
        {
            heightFactor = (spriteTrans.position.y / 100) * ((totalHeight - spriteTrans.position.y) / 2);
            Vector3 newScale = new Vector3(heightFactor, heightFactor, heightFactor);
            newScale *= -1;
            transform.localScale = newScale;
            CalculateSpriteSortingOrder(newScale);
        }

        if (transform.localScale.x < minScaleSize)
        {
            transform.localScale = new Vector3(minScaleSize, minScaleSize, minScaleSize);
        }
        if (transform.localScale.x > maxScaleSize)
        {
            transform.localScale = new Vector3(maxScaleSize, maxScaleSize, maxScaleSize);
        }

    }

    void CalculateSpriteSortingOrder(Vector3 roomHeight)
    {
        int sortingOrder = Mathf.RoundToInt(roomHeight.x * 100);
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].sortingOrder = sortingOrder;
        }
    }


}
