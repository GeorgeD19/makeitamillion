using UnityEngine;
using System.Collections;

public class NewSpawmMaster : MonoBehaviour 
{
	//Public variables
	public float obstaclePct;			//Overall chance of an obstacle spawning

	//The combined % of the patterns should be 100%
	public float pattern1Pct;			//Chance of pattern one spawning
	public float pattern2Pct;			//Chance of pattern two spawning
	public float pattern3Pct;			//Chance of pattern three spawning

	public bool spawnSideLanes;			//Conditions for lanes 4 and 5 to spawn

	public Rigidbody2D[] foodObj;		//Array contaning food prefabs
	public Rigidbody2D[] obstacleObj;	//Array contaning obstacle prefabs
	public GameObject[] spawnPoint;		//Array contaning spawn points
	public float timeBetweenSpawns;		//The delay between each spawn (both obst and food)
	public float dropSpeed;				//The acceleration of the dropped objects
	
	//Private variables
	private float spawnDelay;			//Used for the purpose of delay
	private int rand;					//Used for randomisation
	private int rand2;					//Used for randomisation
	public int rand3;					//Lane randomiser
	private bool spawning;				//Fixing a bug with overlapping

	//Lane references
	private GameObject l1;
	private GameObject l5;
	private GameObject bl1;
	private GameObject bl5;

	// Use this for initialization
	private IEnumerator Start () 
	{
		//Find the lanes
		l1 = GameObject.FindGameObjectWithTag("Lane 1");
		l5 = GameObject.FindGameObjectWithTag("Lane 5");
		bl1 = GameObject.FindGameObjectWithTag("BLane 1");
		bl5 = GameObject.FindGameObjectWithTag("BLane 5");

		//Properly randomise the "randomness" by generating a new seed every time
		Random.seed = System.Environment.TickCount;

		//Starts CoUpdate(), which essentially acts the same as Update()
		yield return StartCoroutine ( CoUpdate() ); 
	}
	
	// Update is called once per frame
	private IEnumerator CoUpdate () 
	{
		//Stuff in here happens once

		//This while loop is pretty much Update() with a few extra perks
		//and the ability to bluescreen your machine. 
		while (true)
		{
			//Lane and spawn addition handles
			if(spawnPoint[0] == null)
				rand3 = Random.Range (1,4);
			else if(spawnPoint[0] != null)
				rand3 = Random.Range (0,5);

			if(spawnSideLanes)
				SpawnLanes();

			//Generate a random number (tested - passed)
			rand = Random.Range (0, 100);
			/*
			 *A second random number is neccessary due to the following scenario:
			 * E.g. rand = 43, obstaclePct = 30. This means an obstacle will spawn 
			 * however since the checks after also use a random number, it also
			 * means only random patterns will be generated (using the example below)
			 */
			rand2 = Random.Range (0, 100);



			//If it's time to go again
			if(spawnDelay < Time.time && !spawning)
			{
				//Check if an obstacle will spawn
				if(rand <= obstaclePct)
				{
					//E.g. pattern1Pct = 10, if(rand <= 10)
					if(rand2 <= pattern1Pct)
					{
						yield return StartCoroutine ( PatOne(rand3) );
					}

					//E.g. pat1Pct = 10, pat2Pct = 10, if(rand <= 20 && rand > 10)
					else if(rand2 <= (pattern1Pct + pattern2Pct) && rand > pattern1Pct)
					{
						yield return StartCoroutine ( PatTwo(rand3) );
					}

					//E.g. pat1Pct = 10, pat2Pct = 10, pat3Pct = 10
					//if(rand <= 30 && rand > 20)
					else if(rand2 <= (pattern3Pct + pattern2Pct + pattern1Pct) && 
					   rand > (pattern1Pct + pattern2Pct))
					{
						yield return StartCoroutine ( PatThree (rand3) );
					}

					//E.g. a random pattern has 70% chance of appearing as a result
					else
						yield return StartCoroutine ( RandPattern(rand3) );
				}

				//If an obstacle isn't spawning
				else
				{
					//Spawn a food item in a random lane
					SpawnFood (Random.Range (0, foodObj.Length), rand3);
					//Update the delay
					spawnDelay = Time.time + timeBetweenSpawns;
				}
			}
			//Always!!!
			yield return null;
		}
	}

	//Pattern one
	private IEnumerator PatOne(int n)
	{
		/*
		 * Spawn an obstacle and two food items in the same lane
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		 * 
		 */
		spawning = true;
		SpawnObstacle (Random.Range (0, obstacleObj.Length), n);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);
		
		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Pattern two
	private IEnumerator PatTwo(int n)
	{
		/*
		 * Spawn an obstacle and two food items in the same lane
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		 */
		spawning = true;
		SpawnFood (Random.Range (0, foodObj.Length), n);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnObstacle (Random.Range (0, obstacleObj.Length), n);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);
		
		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Pattern three
	private IEnumerator PatThree(int n)
	{
		/*
		 * Spawn an obstacle and two food items in the same lane
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		 */
		spawning = true;
		SpawnObstacle (Random.Range (0, obstacleObj.Length), n);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);
		
		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Randomised pattern
	private IEnumerator RandPattern(int n)
	{
		/*
		 * In short this script spawns one obstacle and three food items all in random lanes
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		*/
		spawning = true;
		SpawnObstacle (Random.Range (0, obstacleObj.Length), n);

		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);

		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);

		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, foodObj.Length), n);

		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Code to spawn lanes
	private void SpawnLanes()
	{
		//If the lanes haven't been spawned yet
		if(spawnPoint[0] == null)
		{
			//Activate them
			l1.SetActive (true);
			l5.SetActive (true);
			bl1.SetActive (true);
			bl5.SetActive (true);

			//Assign them
			spawnPoint[0] = l1;
			spawnPoint[4] = l5;
		}

		//Else if the lanes are spawned
		else if(spawnPoint[0] != null)
		{
			//Disable them
			l1.SetActive (false);
			l5.SetActive (false);
			bl1.SetActive (false);
			bl5.SetActive (false);

			//Clean up the array
			spawnPoint[0] = null;
			spawnPoint[4] = null;
		}

		//Set the condition to false so that the method does not get called
		spawnSideLanes = false;
	}

	//Spawns food in a random lane
	private void SpawnFood(int sprite, int lane )
	{
		//Instantiate at random location and add force downwards
		Rigidbody2D clone;
		clone = Instantiate (foodObj[sprite], spawnPoint[lane].transform.position, Quaternion.identity) as Rigidbody2D;
		clone.rigidbody2D.velocity = new Vector2 (0, -dropSpeed);
	}
	
	//Spawns obstacles in a random lane
	private void SpawnObstacle(int sprite, int lane)
	{
		//Instantiate at random location and add force downwards
		Rigidbody2D clone;
		clone = Instantiate (obstacleObj[sprite], spawnPoint[lane].transform.position, Quaternion.identity) as Rigidbody2D;
		clone.rigidbody2D.velocity = new Vector2 (0, -dropSpeed);
	}
}
