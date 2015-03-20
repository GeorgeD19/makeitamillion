using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: The TouchRecorder is the object
    that determines the direction the player
    has swiped, this also allows for drawing
    of the SwipeTrail.
*/

enum ShapeType
{
    Undefined = 0x0,
    SinglePoint,
    UpRight,
    UpLeft,    
    DownRight,
    DownLeft,
    Left,
    Down,
    Right,
    Up,
};

public class TouchRecorder
{
    private System.Collections.Generic.List<Vector2> touchPoints = new System.Collections.Generic.List<Vector2>();
    private bool newTouch = false;
    private bool canAdd = true;
    private Touch mainTouch;
    private float elapsedTime = 0.0f;

    private GameObject mainTrailObject;
    private ShapeType determinedShape;

    public void UpdateRecorder()
    {
        if (newTouch)
        {
            if (elapsedTime > .05)
            {
                canAdd = true;
                elapsedTime = 0.0f;
            }
            else if (canAdd && elapsedTime < 1)
                canAdd = false;

            elapsedTime += Time.deltaTime;
        }
    }

    // This determines the direction based off the average of the inputted swipe points
    public void determineShapeApproximation()
    {
        System.Collections.Generic.List<string> moveDirectionList = new System.Collections.Generic.List<string>();
        Vector2 average_dir = Vector2.zero;
        string lastMovementDir = "";

        for (int i = 0; i < touchPoints.Count; i++)
        {
            if (i != 0)
            {                
                Vector2 drawDirection = (touchPoints[i] - touchPoints[i - 1]).normalized;
                average_dir += drawDirection;

                string active_direction = DetermineDirection(drawDirection);

                if (lastMovementDir != active_direction)
                    moveDirectionList.Add(active_direction);
            }
        }

        average_dir.x /= touchPoints.Count - 1;
        average_dir.y /= touchPoints.Count - 1;

        string averageMovementDir = DetermineDirection(average_dir);

        if (touchPoints[touchPoints.Count - 1] == touchPoints[0])
        {
            determinedShape = ShapeType.SinglePoint;
            return;
        }

        if (averageMovementDir == "Moving Down Left")
            determinedShape = ShapeType.DownLeft;
        else if (averageMovementDir == "Moving Down Right")
            determinedShape = ShapeType.DownRight;
        else if (averageMovementDir == "Moving Up Right")
            determinedShape = ShapeType.UpRight;
        else if (averageMovementDir == "Moving Up Left")
            determinedShape = ShapeType.UpLeft;
        else if (averageMovementDir == "Moving Left")
            determinedShape = ShapeType.Left;
        else if (averageMovementDir == "Moving Down")
            determinedShape = ShapeType.Down;
        else if (averageMovementDir == "Moving Right")
            determinedShape = ShapeType.Right;
        else if (averageMovementDir == "Moving Up")
            determinedShape = ShapeType.Up;
    }

    private string DetermineDirection(Vector2 drawDirection)
    {
        string activeMoveDir = "";

        if ((drawDirection.x >= -1.0 && drawDirection.x <= -0.1) && (drawDirection.y >= -1.0 && drawDirection.y <= -0.1))
            activeMoveDir = "Moving Down Left";
        else if ((drawDirection.x >= 0.1 && drawDirection.x <= 1.0) && (drawDirection.y >= -1.0 && drawDirection.y <= -0.1))
            activeMoveDir = "Moving Down Right";
        else if ((drawDirection.x >= 0.1 && drawDirection.x <= 1.0) && (drawDirection.y >= 0.1 && drawDirection.y <= 1.0))
            activeMoveDir = "Moving Up Right";
        else if ((drawDirection.x >= -1.0 && drawDirection.x <= -0.1) && (drawDirection.y >= 0.1 && drawDirection.y <= 1.0))
            activeMoveDir = "Moving Up Left";
        else if (drawDirection.x <= -0.9 && (drawDirection.y >= -0.1 && drawDirection.y <= 0.1))
            activeMoveDir = "Moving Left";
        else if ((drawDirection.x >= -0.1 && drawDirection.x <= 0.1) && drawDirection.y <= -0.9)
            activeMoveDir = "Moving Down";
        else if (drawDirection.x >= 0.9 && (drawDirection.y >= -0.1 && drawDirection.y <= 0.1))
            activeMoveDir = "Moving Right";
        else if ((drawDirection.x >= -0.1 && drawDirection.x <= 0.1) && drawDirection.y >= 0.9)
            activeMoveDir = "Moving Up";

        return activeMoveDir;
    }

    // This can be used to determine the total movements that took place within a swipe
    private int[] CountMoveDirections(string[] movements)
    {
        string[] directions = { "Moving Down Left", "Moving Down Right", "Moving Up Right", "Moving Up Left", "Moving Left", "Moving Down", "Moving Right", "Moving Up" };
        int[] moveDirCountList = new int[8];
        for (int i = 0; i < movements.Length; i++)
        {
            for (int j = 0; j < directions.Length; j++)
            {
                if (movements[i] == directions[j])
                    moveDirCountList[j] += 1;
            }
        }

        return moveDirCountList;
    }

    public void Reset()
    {        
        determinedShape = ShapeType.Undefined;
		// mainTrailObject.GetComponent<SwipeTrailBehavior>().SendMessage("ActivateRemoval");
        mainTrailObject = null;
        newTouch = false;
        elapsedTime = 0;
        touchPoints.Clear();
    }

    public void addTouch(Vector2 newTouchPos)
    {
        if (!touchPoints.Contains(newTouchPos))
        {
            touchPoints.Add(newTouchPos);
        }
    }

    public void setTrailRendererObject(Object new_trailRender)
    {
        mainTrailObject = (GameObject)GameObject.Instantiate(new_trailRender);
    }

    public void setMainTouch(Touch new_touch)
    {
        mainTouch = new_touch;
    }

    public void setNewTouch(bool newBool)
    {
        newTouch = newBool;
    }

    public int getDeterminedShape
    {
        get { return (int)determinedShape; }
    }

    public GameObject getTrailRenderer()
    {
        return mainTrailObject;
    }

    public Touch getActiveTouch()
    {
        return mainTouch;
    }

    public bool isAllowedAdd()
    {
        return canAdd;
    }

    public bool isNewTouch()
    {
        return newTouch;
    }
}
