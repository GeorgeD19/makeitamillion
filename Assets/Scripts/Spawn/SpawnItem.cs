// Comments to be added later. Code needs to be refactored. This is a basic implementation
// that spawns in two items at two different locations on a two second timer.

using UnityEngine;
using System.Collections;

public class SpawnItem : MonoBehaviour 
{

	public int maxTotalNoOfItems = 10;
	public int MaxNoOfItemsAtOnce = 5;
	public int currentNoOfItems = 0;
	public int totalNoOfItems = 0;
	public int initialTarget = 0;

	public Transform spawnPoints;

	public GameObject itemTemplate;
	public GameObject[] item;
	
	string currentScene;
	int limiter = 0;

	void Start () 
	{
		currentScene = Application.loadedLevelName;
		if(currentScene == "main-game")
		{
			spawnPoints = GameObject.Find("SpawnPoints").transform;
			item = new GameObject[MaxNoOfItemsAtOnce];
			AddItemToList();
		}
	}

	void Update()
	{	
		if(currentScene == "main-game")
		{
			StartCoroutine("spawnItemsRegularly");
		}
	}
	
	public bool AddItemToList()
	{
		bool added = false;

		if(item[MaxNoOfItemsAtOnce - 1] == null && limiter == 0)
		{
			GameObject newItem = Instantiate(itemTemplate) as GameObject;
			for(int i = 0; i < MaxNoOfItemsAtOnce; i++)
			{
				if(item[i] == null && !added)
				{
					item[i] = newItem;
					Transform targetWaypoint = spawnPoints.GetChild(initialTarget);
					item[i].transform.position = targetWaypoint.transform.position;
					added = true;
					
					if (initialTarget + 1 < spawnPoints.childCount)
					{
						initialTarget++;
					}

					currentNoOfItems++;
					totalNoOfItems++;
				}
			}
		}

		return added;
	}
	
	IEnumerator spawnItemsRegularly()
	{
		yield return new WaitForSeconds(2.0f);
		if(totalNoOfItems <= maxTotalNoOfItems)
		{
			if(currentNoOfItems < MaxNoOfItemsAtOnce)
			{
				AddItemToList();
			}
		}
	}
}