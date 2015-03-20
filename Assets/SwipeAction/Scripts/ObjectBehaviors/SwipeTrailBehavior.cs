using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the behavior of the
    swipe trail prefab
*/

public class SwipeTrailBehavior : MonoBehaviour
{
    bool activateRemoval = false;
    float elapsedTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (activateRemoval)
        {
            if (elapsedTime > 5)
                GameObject.Destroy(this.gameObject);
            else
                elapsedTime += Time.deltaTime;
        }
    }

    private void ActivateRemoval()
    {
        activateRemoval = true;
    }
}
