using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using SimpleJSON;

public class ItemManager : MonoBehaviour {

	Action<string> _createItemsCallback;

	// Use this for initialization
	void Start () 
	{
		_createItemsCallback = (jsonArrayString) => {
			StartCoroutine(CreateItemsRoutine(jsonArrayString));
		};

		CreateItems();
	}

	public void CreateItems()
	{
		string userId = Main.Instance.UserInfo.UserID;
		StartCoroutine(Main.Instance.Web.GetItemsIDs(userId, _createItemsCallback));
	}

	IEnumerator CreateItemsRoutine(string jsonArrayString)
	{
		//Parsing json array as an array
		JSONArray jsonArray = JSON.Parse(jsonArrayString) as JSONArray;

		for (int i = 0; i < jsonArray.Count; i++)
		{
			//Create local variables
			bool isDone = false; //Are we done downloading?
			string itemId = jsonArray[i].AsObject["itemID"];
			string id = jsonArray[i].AsObject["ID"];

			JSONObject itemInfoJson = new JSONObject();

			//Create a callback to get the info from Web.cs
			Action<string> getItemInfoCallback = (itemInfo) => {
				isDone = true;
				JSONArray tempArray = JSON.Parse(itemInfo) as JSONArray;
				itemInfoJson = tempArray[0].AsObject;
			};

			StartCoroutine(Main.Instance.Web.GetItem(itemId, getItemInfoCallback));

			//Wait until callback is called from WEB (info finished downloading)
			yield return new WaitUntil(() => isDone == true);

			//Instantiate GameObject
			GameObject itemGo = Instantiate(Resources.Load("Prefabs/Item") as GameObject);
			Item item = itemGo.AddComponent<Item>();

			item.ID = id;
			item.ItemID = itemId;

			itemGo.transform.SetParent(this.transform);
			itemGo.transform.localScale = Vector3.one;
			itemGo.transform.localPosition = Vector3.zero;

			//Fill info
			itemGo.transform.Find("Name").GetComponent<Text>().text = itemInfoJson["name"];
			itemGo.transform.Find("Price").GetComponent<Text>().text = itemInfoJson["price"];
			itemGo.transform.Find("Description").GetComponent<Text>().text = itemInfoJson["description"];

			//Set Sell button
			itemGo.transform.Find("SellButton").GetComponent<Button>().onClick.AddListener(() => {
				string idInInventory = id;
                string iId = itemId;
                string userId = Main.Instance.UserInfo.UserID;

				StartCoroutine(Main.Instance.Web.SellItem(idInInventory, itemId, userId));
			});

			//continue to next item
        }
	}
}
