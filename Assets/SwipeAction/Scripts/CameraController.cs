using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the behavior of the
    Main Camera
*/

public class CameraController : MonoBehaviour
{
    RaycastHit target;

    Transform previous_transform;
    Transform current_transform;

    public Transform FollowTarget; // Set this within the Camera Inspector settings to the MainPlayer
	
	// Update is called once per frame
	void Update ()
    {
        if (Camera.mainCamera.isOrthoGraphic)
        {
            transform.position = new Vector3(FollowTarget.position.x, transform.position.y, FollowTarget.position.z);

            Ray ray = new Ray(transform.position, Vector3.down);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);

            if (Physics.Raycast(ray, out target))
            {
                if (previous_transform == null)
                    previous_transform = target.transform;
                else
                    previous_transform = current_transform;

                current_transform = target.transform;

                // These if statements are used for making the roof of the house transparent when the MainPlayer is inside
                // make sure to name your roof object to the value bellow if you want this feature, feel free to change
                // the name to your desire.
                if (target.transform.name.Contains("house_roof") && target.transform.renderer.material.color.a != 0)
                {
                    target.transform.renderer.material.color = new Color(1, 1, 1, 0);
                }

                if (previous_transform.name == "house_roof" && current_transform.name != "house_roof")
                {
                    previous_transform.renderer.material.color = new Color(1, 1, 1, 1);
                }
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(FollowTarget.position.x, FollowTarget.position.y + (60 - FollowTarget.localScale.y), FollowTarget.position.z - 30), 1);
        }
	}
}
