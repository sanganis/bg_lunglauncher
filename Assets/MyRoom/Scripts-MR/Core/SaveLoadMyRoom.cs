using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveLoadMyRoom : MonoBehaviour {

    public static SaveLoadMyRoom instance;

    public MyRoomPlaceableItemController[] allItems;
      
    public List<int> myItemsList = new List<int>();

    public List<Vector3> myItemsPositionsList = new List<Vector3>();


    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("MyItems"))
        {
            LoadMyItems();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerPrefs.DeleteKey("MyItems");
        }
    }

    
    public void AddToMyItems(int itemID, Vector3 position)
    {
        myItemsList.Add(itemID);
        myItemsPositionsList.Add(position);
        SaveMyItems();
    }

    public void LoadMyItems()
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
        for(int i =0; i < myItemsPositionsStorage.Length; i++)
        {
            myItemsPositionsList.Add(myItemsPositionsStorage[i]);
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
        tempSaveString = "";        
        PlayerPrefs.SetString("MyItemsPositions", null);
                
        tempSaveString = SerializeVector3Array(myItemsPositionsList.ToArray());
        Debug.Log(tempSaveString);

        PlayerPrefs.SetString("MyItemsPositions", tempSaveString);
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

}
