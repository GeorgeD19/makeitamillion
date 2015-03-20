///// <summary>
/// Save Script
/// Version: 1.0, Date: 19/03/2015, Blair Duncan
/// A very short script to save the game data to a file
/// </summary>

using UnityEngine;
using System.Collections;

public class Save : MonoBehaviour 
{
	/*
	 *	Attach me to the save button, I'll save the current progress
	 */

	void OnClick() 
	{
		GameControl.control.Save();		//	Accesses the save function from the GameControl script
	}
}