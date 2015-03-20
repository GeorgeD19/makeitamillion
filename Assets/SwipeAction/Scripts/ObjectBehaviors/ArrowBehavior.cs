using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the behavior of the
    ArrowTemplate object when in range mode
*/

public class ArrowBehavior : MonoBehaviour
{
    bool begin_removal = false;
    float alpha_mod = 1.0f;

    // Update is called once per frame
	void Update()
    {
        if (!begin_removal && transform.rigidbody.velocity.magnitude <= 0.001f)
            begin_removal = true;
        else if (begin_removal && alpha_mod > 0)
        {
            renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, alpha_mod);
            alpha_mod -= Time.deltaTime;
        }
	}

    void OnTriggerEnter(Collider other_obj)
    {
        if (other_obj.transform.name.Contains("housewall"))
        {
            rigidbody.velocity = -rigidbody.mass * rigidbody.velocity / 50;
        }
    }

    public void SetProjectileForceDirection(Vector3 force_power, Vector3 force_dir)
    {
        if (force_power.magnitude > 500)
            force_power = new Vector3(500, 0, 50);

        transform.rigidbody.velocity = new Vector3(0.1f, 0.1f, 0.1f);
        transform.rigidbody.AddForceAtPosition(force_power.magnitude * force_dir, transform.position, ForceMode.Force);
    }

    public float GetAlphaMod()
    {
        return alpha_mod;
    }
}
