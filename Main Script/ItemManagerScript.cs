using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;
using TMPro;    

public class ItemManagerScript : MonoBehaviour
{
    Action<string> _createItemsCallback;

    // Start is called before the first frame update
    void Start()
    {
        //Define Callback
        _createItemsCallback = (jsonArrayString) =>
        {
            StartCoroutine(CreateItemsRoutine(jsonArrayString));
        };


        CreateItems();  
    }

    public void CreateItems()
    {
        string userId = MainScript.Instance.UserInfo.UserID;
        StartCoroutine(MainScript.Instance.WebManagerScript.GetItemsIDs(userId, _createItemsCallback));
    }

    IEnumerator CreateItemsRoutine(string jsonArrayString)
    {
        //Parsing json array string as an array
        JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

        for (int i = 0; i < jsonArray.Count; i++)
        {
            //Create local variables
            bool isDone = false;  //are we done downloading?
            string itemId = jsonArray[i].AsObject["itemID"];
            string id = jsonArray[i].AsObject["ID"];
            JSONObject itemInfoJson = new JSONObject();

            //create a callback to get the information form WebManagerScript.cs
            Action<string> getItemInfoCallback = (itemInfo) =>
            {
                isDone = true;
                JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
                itemInfoJson = tempArray[0].AsObject;
            };

            //Wait until WebManagerScript.cs calls the callback we passed as parameter
            StartCoroutine(MainScript.Instance.WebManagerScript.GetItem(itemId, getItemInfoCallback));

            //wait until the callback is called from WebManagerScript (info finished downloading)
            yield return new WaitUntil(() => isDone == true);

            //Instantiate GameObject (item prefab)
            GameObject itemGo = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
            ItemScript item = itemGo.AddComponent<ItemScript>();

            item.ID = id;
            item.ItemID = itemId;

            itemGo.transform.SetParent(this.transform);
            itemGo.transform.localScale = Vector3.one;
            itemGo.transform.localPosition = Vector3.zero;

            //Fill information
            itemGo.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
            itemGo.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];
            itemGo.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];

            // TODO
            // 1. Get bytes instead of sprite
            // 2. Try to get Image
            // 3. Download image only if we couldn't get image
            // 4. Save image in our device if we downloaded it
            // 5. Convert bytes into sprite here

            byte[] bytes = ImageManager.Instance.LoadImage(itemId);

            // Download from WebManagerScript
            if (bytes.Length == 0)
            {
                //create a callback to get the SPRITE form WebManagerScript.cs
                Action<byte[]> getItemIconCallback = (downloadedBytes) =>
                {
                    Sprite sprite = ImageManager.Instance.BytesToSprite(downloadedBytes);
                    itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
                    ImageManager.Instance.SaveImage(itemId, downloadedBytes);
                };
                StartCoroutine(MainScript.Instance.WebManagerScript.GetItemIcon(itemId, getItemIconCallback));
            }
            // Load from device
            else
            {
                Sprite sprite = ImageManager.Instance.BytesToSprite(bytes);
                itemGo.transform.Find("Image").GetComponent<Image>().sprite = sprite;
            }

            //Set Sell button
            itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                string idInInventory = id;
                string iId = itemId;
                string userId = MainScript.Instance.UserInfo.UserID;
                StartCoroutine(MainScript.Instance.WebManagerScript.SellItem(idInInventory, itemId, userId));
            });

            //continue to the next item

        }

        //yield return null;
    }
}
