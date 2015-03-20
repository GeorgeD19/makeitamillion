/// <summary>
/// Load Script
/// Version: 1.0, Date: 19/03/2015, Blair Duncan
/// A very short script to load the saved game data from a file
/// </summary>

using UnityEngine;
using System.Collections;

public class Load : MonoBehaviour 
{
	/*
	 *	Attach me to the load button, I'll load in any data that has been saved
	 */

	void OnClick() 
	{
		GameControl.control.Load();		//	Accesses the load function from the GameControl script
	}
}