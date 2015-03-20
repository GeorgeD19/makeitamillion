using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the behavior of the
    creature objects, collision handling, etc
*/

public class CreatureBehavior : MonoBehaviour
{
    public Texture2D BackgroundTexture;
    public Texture2D HealthTexture;
    public Transform MainPlayerReference;

    private int currentHealth = 200;
    private int maxHealth = 200;

	void OnGUI()
    {
        Vector3 topOfEnemy = Camera.mainCamera.WorldToScreenPoint(transform.position + new Vector3(0, 0, transform.localScale.z / 2));

        // 100 is the width
        GUI.DrawTexture(new Rect(topOfEnemy.x - 50, Screen.height - (topOfEnemy.y + 15), 100, 10), BackgroundTexture);
        GUI.DrawTexture(new Rect(topOfEnemy.x - 50, Screen.height - (topOfEnemy.y + 15), (100 * currentHealth) / maxHealth, 10), HealthTexture);
    }

    void OnTriggerEnter(Collider other_obj)
    {
        if (other_obj.transform.name.Contains("MeleeSwipe"))
        {
            if (currentHealth > 0)
                currentHealth -= Random.Range(10, 50);

            if (currentHealth <= 0)
                GameObject.Destroy(transform.gameObject);
        }
        else if (other_obj.transform.name.Contains("ArrowTemplate"))
        {
            if (currentHealth > 0)
                currentHealth -= Random.Range(10, 50);

            if (currentHealth <= 0)
                GameObject.Destroy(transform.gameObject);

            other_obj.rigidbody.velocity = -other_obj.rigidbody.mass * other_obj.rigidbody.velocity / 10;
        }
    }

	// Update is called once per frame
	void Update () {
        if (MainPlayerReference.position.z < transform.position.z)
            transform.position = new Vector3(transform.position.x, MainPlayerReference.position.y - .01f, transform.position.z);
        else if (MainPlayerReference.position.z > transform.position.z)
            transform.position = new Vector3(transform.position.x, MainPlayerReference.position.y + .01f, transform.position.z);
	}
}
