using UnityEngine;
using System.Collections;

public class NewSpawnxMaster : MonoBehaviour 
{
	//Public variables
	public float obstaclePct;			//Overall chance of an obstacle spawning

	//The combined % of the patterns should be 100%
	public float pattern1Pct;			//Chance of pattern one spawning
	public float pattern2Pct;			//Chance of pattern two spawning
	public float pattern3Pct;			//Chance of pattern three spawning



	public GameObject[] _collectable;		//Array contaning food prefabs
	public GameObject[] obstacleObj;	//Array contaning obstacle prefabs
	public GameObject[] spawnPoint;		//Array contaning spawn points
	public float timeBetweenSpawns;		//The delay between each spawn (both obst and food)
	public float dropSpeed;				//The acceleration of the dropped objects
	
	//Private variables
	private float spawnDelay;			//Used for the purpose of delay
	private int rand;					//Used for randomisation
	private int rand2;
	private bool spawning;				//Fixing a bug with overlapping
	private LevelData ld;

	// Use this for initialization
	private IEnumerator Start () 
	{


		//Properly randomise the "randomness" by generating a new seed every time
		Random.seed = System.Environment.TickCount;

		//Starts CoUpdate(), which essentially acts the same as Update()
		yield return StartCoroutine ( CoUpdate() ); 
	}
	
	// Update is called once per frame
	private IEnumerator CoUpdate () 
	{
		//This while loop is pretty much Update() with a few extra perks
		//and the ability to bluescreen your machine. 
		while (true)
		{


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
						yield return StartCoroutine ( PatOne() );
					}

					//E.g. pat1Pct = 10, pat2Pct = 10, if(rand <= 20 && rand > 10)
					else if(rand2 <= (pattern1Pct + pattern2Pct) && rand > pattern1Pct)
					{
						yield return StartCoroutine ( PatTwo() );
					}

					//E.g. pat1Pct = 10, pat2Pct = 10, pat3Pct = 10
					//if(rand <= 30 && rand > 20)
					else if(rand2 <= (pattern3Pct + pattern2Pct + pattern1Pct) && 
					   rand > (pattern1Pct + pattern2Pct))
					{
						yield return StartCoroutine ( PatThree () );
					}

					//E.g. a random pattern has 70% chance of appearing as a result
					else
						yield return StartCoroutine ( RandPattern() );
				}

				//If an obstacle isn't spawning
				else
				{
					//Spawn a food item in a random lane
					SpawnFood (Random.Range (0, _collectable.Length), Random.Range (0, spawnPoint.Length));
					//Update the delay
					spawnDelay = Time.time + timeBetweenSpawns;
				}
			}
			//Always!!!
			yield return null;
		}
	}

	//Pattern one
	private IEnumerator PatOne()
	{
		/*
		 * Spawn an obstacle and two food items in the same lane
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		 * 
		 */
		spawning = true;
		SpawnObstacle (Random.Range (0, obstacleObj.Length), 1);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), 1);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), 1);
		
		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Pattern two
	private IEnumerator PatTwo()
	{
		/*
		 * Spawn an obstacle and two food items in the same lane
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		 */
		spawning = true;
		SpawnObstacle (Random.Range (0, obstacleObj.Length), 2);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), 2);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), 2);
		
		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Pattern three
	private IEnumerator PatThree()
	{
		/*
		 * Spawn an obstacle and two food items in the same lane
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		 */
		spawning = true;
		SpawnObstacle (Random.Range (0, obstacleObj.Length), 3);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), 3);
		
		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), 3);
		
		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Randomised pattern
	private IEnumerator RandPattern()
	{
		/*
		 * In short this script spawns one obstacle and three food items all in random lanes
		 * as it is a CoRoutine I use WaitForSeconds as a delay rather than the usual method.
		*/
		spawning = true;
		SpawnObstacle (Random.Range (0, obstacleObj.Length), Random.Range (0, spawnPoint.Length));

		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), Random.Range (0, spawnPoint.Length));

		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), Random.Range (0, spawnPoint.Length));

		yield return new WaitForSeconds(timeBetweenSpawns);
		SpawnFood (Random.Range (0, _collectable.Length), Random.Range (0, spawnPoint.Length));

		//Making sure we stay on track
		spawnDelay = Time.time + timeBetweenSpawns;
		spawning = false;
	}

	//Spawns food in a random lane
	private void SpawnFood(int sprite, int lane )
	{
		//Instantiate at random location and add force downwards
		GameObject clone;
		clone = Instantiate (_collectable[sprite], spawnPoint[lane].transform.position, Quaternion.identity) as GameObject;
		clone.GetComponent<ObjectMovementScript> ()._speed = dropSpeed;
		//clone.rigidbody2D.velocity = new Vector2 (0, -dropSpeed);
	}


	
	//Spawns obstacles in a random lane
	private void SpawnObstacle(int sprite, int lane)
	{
		//Instantiate at random location and add force downwards
		GameObject clone;
		clone = Instantiate (obstacleObj[sprite], spawnPoint[lane].transform.position, Quaternion.identity) as GameObject;
		clone.GetComponent<ObjectMovementScript> ()._speed = dropSpeed;
		//clone.rigidbody2D.velocity = new Vector2 (0, -dropSpeed);
	}
}
