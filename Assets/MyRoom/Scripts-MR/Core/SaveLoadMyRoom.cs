﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveLoadMyRoom : MonoBehaviour {

    public static SaveLoadMyRoom instance;

    public MyRoomPlaceableItemController[] allItems;
    public MyRoomBackgroundController[] allBackgrounds;
      
    public List<int> myItemsList = new List<int>();   

    public List<Vector3> myItemsPositionsList = new List<Vector3>();
    public List<Vector3> playerPositionList = new List<Vector3>();

    int savedBackground;


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
       
            LoadMyItems();
        
    }

    void Update()
    {
        // debug controls
        if (Input.GetKeyDown(KeyCode.C))
        {
            ClearRoom();
            //PlayerPrefs.DeleteKey("MyItems");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            GiveMoney();
            //MyRoomController.instance.currentStars += 100;
        }
    }

    public void ClearRoom()
    {
        MyRoomPlaceableItemController[] myCurrentItems = FindObjectsOfType<MyRoomPlaceableItemController>();
        for(int i = 0; i < myCurrentItems.Length; i++)
        {
            Destroy(myCurrentItems[i].gameObject);
        }
        PlayerPrefs.DeleteKey("MyItems");
        PlayerPrefs.DeleteKey("MyItemsPositions");
        PlayerPrefs.DeleteKey("MyPlayerPosition");
        MyRoomController.instance.FindPlayer().transform.position = new Vector3(4, -5, 0);
    }

    public void GiveMoney()
    {
        MyRoomController.instance.currentStars += 100;
        MyRoomMainUIController.instance.ShowCurrentStars();
    }

    public void RemoveFromMyItems(int itemID, Vector3 position)
    {
        myItemsList.Remove(itemID);
        myItemsPositionsList.Remove(position);
        SaveMyItems();
    }

    public void AddToMyItems(int itemID, Vector3 position)
    {
        myItemsList.Add(itemID);
        myItemsPositionsList.Add(position);
        SaveMyItems();
    }

    public void RemovePlayerPosition(Vector3 position)
    {
        playerPositionList.Remove(position);
        SaveMyItems();
    }

    public void AddToPlayerPositions(Vector3 position)
    {
        playerPositionList.Add(position);
        SaveMyItems();
    }

    public void AddToMyBackground(int backgroundID)
    {
        savedBackground = backgroundID;
        SaveMyItems();
    }

    public void LoadMyItems()
    {
        if (PlayerPrefs.HasKey("MyItems"))
        {
            string itemTemp = PlayerPrefs.GetString("MyItems");
            string[] myItemsTempStorage;
            myItemsTempStorage = itemTemp.Split("*".ToCharArray());
            for (int i = 0; i < myItemsTempStorage.Length; i++)
            {
                myItemsList.Add(int.Parse(myItemsTempStorage[i]));
            }

            string posTemp = PlayerPrefs.GetString("MyItemsPositions");
            Vector3[] myItemsPositionsStorage;
            myItemsPositionsStorage = DeserializeVector3Array(posTemp);
            for (int i = 0; i < myItemsPositionsStorage.Length; i++)
            {
                myItemsPositionsList.Add(myItemsPositionsStorage[i]);
            }            
        }

        if (PlayerPrefs.HasKey("MyPlayerPosition"))
        {
            string playerPos = PlayerPrefs.GetString("MyPlayerPosition");
            Vector3[] playerPosStorage;
            playerPosStorage = DeserializeVector3Array(playerPos);
            playerPositionList.Add(playerPosStorage[0]);
            MyRoomController.instance.FindPlayer().transform.position = playerPositionList[0];
        }

        if (PlayerPrefs.HasKey("MyBackground"))
        {
            savedBackground = PlayerPrefs.GetInt("MyBackground");
        }

        InstantiateMySavedItems();       

    }


    public void InstantiateMySavedItems()
    {
        for (int i = 0; i < myItemsList.Count; i++)
        {
            for (int x = 0; x < allItems.Length; x++)
            {
                if (myItemsList[i] == (int)allItems[x].itemID)
                {
                    Instantiate(allItems[x], myItemsPositionsList[i], transform.rotation);
                }
            }
        }
        for (int b = 0; b < allBackgrounds.Length; b++)
        {
            if (savedBackground == (int)allBackgrounds[b].backgroundID)
            {
                MyRoomBackgroundController bg = Instantiate(allBackgrounds[b], Vector3.zero, transform.rotation);
                MyRoomController.instance.currentBackground = bg;
            }
        }        
    }


    void SaveMyItems()
    {
        string tempSaveString = "";
        // the save file is first cleared
        PlayerPrefs.SetString("MyItems", null);   
        // loop through the array and add a * between each owned item number and convert it to a string
        for (int i = 0; i < myItemsList.Count; i++)
        {
            if (i != myItemsList.Count - 1)
            {
                tempSaveString += myItemsList[i].ToString() + "*";
            }
           else
            {
                tempSaveString += myItemsList[i].ToString();
            }
        }        
        PlayerPrefs.SetString("MyItems", tempSaveString);

              
        PlayerPrefs.SetString("MyItemsPositions", null);                
        tempSaveString = SerializeVector3Array(myItemsPositionsList.ToArray());  
        PlayerPrefs.SetString("MyItemsPositions", tempSaveString);


        if (playerPositionList.Count > 0)
        {
            PlayerPrefs.SetString("MyPlayerPosition", null);
            tempSaveString = SerializeVector3Array(playerPositionList.ToArray());
            PlayerPrefs.SetString("MyPlayerPosition", tempSaveString);
        }
        else
        {
            PlayerPrefs.DeleteKey("MyPlayerPosition");
        }


        PlayerPrefs.SetInt("MyBackground", savedBackground);
    }

    public string SerializeVector3Array(Vector3[] aVectors)
    {
        StringBuilder sb = new StringBuilder();
        foreach (Vector3 v in aVectors)
        {
            sb.Append(v.x).Append(" ").Append(v.y).Append(" ").Append(v.z).Append("|");
        }
        if (sb.Length > 0) // remove last "|"
            sb.Remove(sb.Length - 1, 1);
        return sb.ToString();
    }
    public Vector3[] DeserializeVector3Array(string aData)
    {
        string[] vectors = aData.Split('|');
        Vector3[] result = new Vector3[vectors.Length];
        for (int i = 0; i < vectors.Length; i++)
        {
            string[] values = vectors[i].Split(' ');
            if (values.Length != 3)
                throw new System.FormatException("component count mismatch. Expected 3 components but got " + values.Length);
            result[i] = new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
        }
        return result;
    }


    public int LoadStars()
    {
        return PlayerPrefs.GetInt("Stars");
    }

    public void SaveStars(int stars)
    {
        PlayerPrefs.SetInt("Stars",stars);
    }

}
