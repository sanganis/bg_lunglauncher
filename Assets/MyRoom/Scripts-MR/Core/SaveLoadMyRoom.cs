using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadMyRoom : MonoBehaviour {

    public static SaveLoadMyRoom instance;

    public MyRoomPlaceableItemController[] allItems;

    string [] myItemsTempStorage;

    public List<string> myItemsList = new List<string>();


    void Awake()
    {
        instance = this;
    }

    public void AddToMyItems(string itemID)
    {
        myItemsList.Add(itemID);
    }

    public void LoadMyItems()
    {
        string temp = PlayerPrefs.GetString("MyItems");
        myItemsTempStorage = temp.Split("*".ToCharArray());
        for (int i = 0; i < myItemsTempStorage.Length; i++)
        {
            myItemsList.Add(temp);
        }
    }


    public void SaveMyItems()
    {
        string tempSaveString = "";

        // the save file is first cleared
        PlayerPrefs.SetString("MyItems", null);
      
        // loop through the array and add a * between each owned item number and convert it to a string
        for (int i = 0; i < myItemsList.Count; i++)
        {
            if (i != myItemsList.Count)
            {
                tempSaveString += myItemsList[i].ToString() + "*";
            }
            else
                tempSaveString += myItemsList[i].ToString();
        }
        PlayerPrefs.SetString("MyItems", tempSaveString);
    }



}
