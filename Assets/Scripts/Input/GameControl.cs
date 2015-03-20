/// <summary>
/// GameControl Script
/// Version: 1.0, Date: 19/03/2015, Blair Duncan
/// A script that acts as a game controller
/// Contains functions to Save and Load the players score to and from a file, also creates the GameControl static reference variable
/// Serializes the data into binary format so it makes it difficult to read/change/alter/tamper with
/// </summary>

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/*
 *	GameControl class that utilises C# serialization to save and load data.
 *	Empty game object called GameControl is in prefabs and has this script attached
 */

public class GameControl : MonoBehaviour 
{
	public static GameControl control;			//	A public static reference variable
	
	public float score;							//	The players score
	
	/*
	 *	On awake, if there is no current control then make this the current game control
	 *	Otherwise destroy it, there can be only one. Follows the singleton theory.
	 */
	
	void Awake() 
	{
		if (control == null) 
		{
			DontDestroyOnLoad(gameObject);		//	This wil allow the game object to persist from scene to scene
			control = this;						//	Set the control to this
		} 
		
		else if (control != this) 
		{
			Destroy(gameObject);				//	Destroy the game object, we only want one.
		}
	}

	/*
	 *	Test function to show persistence of score through the different scenes when you save and then load
	 *	Creates a label to view the score each time the cup/backpack collects a coin
	 */
	
	void OnGUI()
	{
		GUI.Label (new Rect (10, 2, 100, 100), "Score = " + score);
	}
	
	/*
	 *	Function to save the data
	 *	Converts the data into binary format, safer than using PlayerPrefs
	 */
	
	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();												// Create a new BinaryFormatter instance
		FileStream file = File.Create(Application.persistentDataPath + "/playerScore.dat");		// Create the file to save
		PlayerInfo pinfo = new PlayerInfo();													// Create a new PlayerInfo instance
		pinfo.score = score;																	// Set pinfo.score equal to local score
		bf.Serialize(file, pinfo);																// Write the container to the file created earlier
		file.Close();																			// Close the file
	}
	
	/*
	 *	Function to load the data
	 *	Loads the data from the saved binary format
	 */
	
	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerScore.dat"))									// Check if the file exists
		{
			BinaryFormatter bf = new BinaryFormatter();															// Create new BinaryFormatter instance
			FileStream file = File.Open(Application.persistentDataPath + "/playerScore.dat", FileMode.Open);	// Open the file that was previously saved
			PlayerInfo pinfo = (PlayerInfo)bf.Deserialize(file);												// Cast to a PlayerInfo object then deserialize it
			file.Close();																						// Close the file
			score = pinfo.score;																				// Set the score equal to the score loaded from the file
		}
	}
}

/*
*	Clean player information class that mirrors the data
*	Needs to be made serializable and is also private to GameControl class
*/

[Serializable]
class PlayerInfo
{
	public float score;
}