using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the behavior of the
    MainPlayer object, as well as the objects
    the player interacts with through the gui.
    The TouchRecorder is utilized here as well.
*/

public class PlayerController : MonoBehaviour
{
    enum ActiveSkillMode
    {
        Melee,
        Ranged
    };

    public enum OrientationType
    {
        Portrait,
        Landscape
    };

    public float PlayerSpeed = 20.0f;
    public bool DesktopTestMode = true;
    public OrientationType Orientation;
    public bool ReverseOrientation;

    Vector3 displacement_mod = Vector2.zero;

    Vector2 center_point = Vector2.zero;
    Vector2 active_mousepos = Vector2.zero;
    float active_angle = 0.0f;
    bool active_movement_toggle = false;
    Texture2D[] movementControls = new Texture2D[2];
    Texture2D movementControllerPoint;

    ActiveSkillMode skill_mode = ActiveSkillMode.Melee;
    Texture2D[] melee_skill_toggle = new Texture2D[2];
    Texture2D[] ranged_skill_toggle = new Texture2D[2];
    Rect arrow_rect = new Rect();
    Rect[] skill_toggle_pos = new Rect[2];
    int[,] active_skill_toggle = new int[2,1];

    Vector2 init_pos = Vector2.zero;  // initial position of a swipe
    Vector2 final_pos = Vector2.zero; // final position of a swipe
    TouchRecorder touchRecorder = new TouchRecorder();
    RaycastHit directedRayInfo = new RaycastHit();

    // Use this for initialization
    void Start()
    {
        movementControls[0] = (Texture2D)Resources.Load("Textures/MovementControlsRotated");
        movementControls[1] = (Texture2D)Resources.Load("Textures/MovementControls");
        movementControllerPoint = (Texture2D)Resources.Load("Textures/movementPoint");

        if (Orientation.Equals(OrientationType.Portrait))
        {
            melee_skill_toggle[0] = (Texture2D)Resources.Load("Textures/MeleeUnSelected_Rotated");
            melee_skill_toggle[1] = (Texture2D)Resources.Load("Textures/MeleeSelected_Rotated");

            ranged_skill_toggle[0] = (Texture2D)Resources.Load("Textures/RangedUnSelected_Rotated");
            ranged_skill_toggle[1] = (Texture2D)Resources.Load("Textures/RangedSelected_Rotated");
        }
        else if (Orientation.Equals(OrientationType.Landscape))
        {
            melee_skill_toggle[0] = (Texture2D)Resources.Load("Textures/MeleeUnSelected");
            melee_skill_toggle[1] = (Texture2D)Resources.Load("Textures/MeleeSelected");

            ranged_skill_toggle[0] = (Texture2D)Resources.Load("Textures/RangedUnSelected");
            ranged_skill_toggle[1] = (Texture2D)Resources.Load("Textures/RangedSelected");
        }

        // Set initial position for Movement Controls, Melee and Ranged toggles based on Orientation Settings
        if (Orientation.Equals(OrientationType.Portrait) && !ReverseOrientation)
        {
            arrow_rect = new Rect(0, 0, movementControls[0].width, movementControls[0].height);
            
            skill_toggle_pos[0] = new Rect(0, (melee_skill_toggle[0].width / 2) * 4, melee_skill_toggle[0].width / 2, melee_skill_toggle[0].height / 2);
            skill_toggle_pos[1] = new Rect(0, (ranged_skill_toggle[0].width / 2) * 5, ranged_skill_toggle[0].width / 2, ranged_skill_toggle[0].height / 2);
        }
        else if (Orientation.Equals(OrientationType.Portrait) && ReverseOrientation)
        {
            arrow_rect = new Rect(0, Screen.height - movementControls[0].height, movementControls[0].width, movementControls[0].height);

            skill_toggle_pos[0] = new Rect(0, Screen.height - (melee_skill_toggle[0].width / 2) * 5, melee_skill_toggle[0].width / 2, melee_skill_toggle[0].height / 2);
            skill_toggle_pos[1] = new Rect(0, Screen.height - (ranged_skill_toggle[0].width / 2) * 6, ranged_skill_toggle[0].width / 2, ranged_skill_toggle[0].height / 2);
        }
        else if (Orientation.Equals(OrientationType.Landscape) && !ReverseOrientation)
        {
            arrow_rect = new Rect(0, Screen.height - movementControls[0].height, movementControls[0].width, movementControls[0].height);

            skill_toggle_pos[0] = new Rect((melee_skill_toggle[0].width / 2) * 4, Screen.height - 64, melee_skill_toggle[0].width / 2, melee_skill_toggle[0].height / 2);
            skill_toggle_pos[1] = new Rect((ranged_skill_toggle[0].width / 2) * 5, Screen.height - 64, ranged_skill_toggle[0].width / 2, ranged_skill_toggle[0].height / 2);
        }
        else if (Orientation.Equals(OrientationType.Landscape) && ReverseOrientation)
        {
            arrow_rect = new Rect(Screen.width - movementControls[0].width, Screen.height - movementControls[0].height, movementControls[0].width, movementControls[0].height);

            skill_toggle_pos[0] = new Rect(Screen.width - (melee_skill_toggle[0].width / 2) * 5, Screen.height - 64, melee_skill_toggle[0].width / 2, melee_skill_toggle[0].height / 2);
            skill_toggle_pos[1] = new Rect(Screen.width - (ranged_skill_toggle[0].width / 2) * 6, Screen.height - 64, ranged_skill_toggle[0].width / 2, ranged_skill_toggle[0].height / 2);
        }

        center_point = new Vector2(arrow_rect.x + arrow_rect.width / 2, arrow_rect.y + arrow_rect.height / 2);
        SetSelectedToggle(skill_mode);        
    }

    void OnGUI()
    {
        if (Orientation.Equals(OrientationType.Portrait) && !ReverseOrientation)
            GUI.DrawTexture(arrow_rect, movementControls[0]);
        else if (Orientation.Equals(OrientationType.Portrait) && ReverseOrientation)
            GUI.DrawTexture(arrow_rect, movementControls[0]);
        else if (Orientation.Equals(OrientationType.Landscape) && !ReverseOrientation)
            GUI.DrawTexture(arrow_rect, movementControls[1]);
        else if (Orientation.Equals(OrientationType.Landscape) && ReverseOrientation)
            GUI.DrawTexture(arrow_rect, movementControls[1]);

        GUI.DrawTexture(skill_toggle_pos[(int)ActiveSkillMode.Melee], melee_skill_toggle[active_skill_toggle[(int)ActiveSkillMode.Melee, 0]]);
        GUI.DrawTexture(skill_toggle_pos[(int)ActiveSkillMode.Ranged], ranged_skill_toggle[active_skill_toggle[(int)ActiveSkillMode.Ranged, 0]]);

        if (DesktopTestMode)
        {
            if ((Orientation.Equals(OrientationType.Portrait) && ReverseOrientation) || Orientation.Equals(OrientationType.Landscape))
                GUI.Label(new Rect(0, 0, 320, 40), "Desktop Test Mode\n(Disable In MainPlayer -> PlayerController Inspector)");
            else
                GUI.Label(new Rect(0, Screen.height - 40, 320, 40), "Desktop Test Mode\n(Disable In MainPlayer -> PlayerController Inspector)");
        }

        if (active_movement_toggle)
            GUI.DrawTexture(new Rect(active_mousepos.x - movementControllerPoint.width / 2, active_mousepos.y - movementControllerPoint.height / 2, movementControllerPoint.width, movementControllerPoint.height), movementControllerPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        if (Camera.mainCamera.isOrthoGraphic)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.MoveUp);
                displacement_mod.z = PlayerSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.Idle);
                displacement_mod.z = -PlayerSpeed * Time.deltaTime;
            }
            else
                displacement_mod.z = 0;

            if (Input.GetKey(KeyCode.D))
            {
                transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.MoveRight);
                displacement_mod.x = PlayerSpeed * Time.deltaTime;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.MoveLeft);
                displacement_mod.x = -PlayerSpeed * Time.deltaTime;
            }
            else
                displacement_mod.x = 0;

            transform.position = transform.position + displacement_mod;
        }

        if (active_movement_toggle)
        {
            if (transform.rigidbody.drag != 1)
                transform.rigidbody.drag = 1;

            UpdatePositionThroughInputs();
        }
        else
        {
            if (transform.rigidbody.drag != 10)
                transform.rigidbody.drag = 10;
        }

        if (DesktopTestMode)
        {
            Vector2 active_position = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
            if (Input.GetMouseButton(0) && Vector2.Distance(center_point, active_position) < movementControls[0].width / 2)
            {
                active_mousepos = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
                active_movement_toggle = true;
            }

            // Only do this if the swipe isn't within the Movement Controller
            if (Vector2.Distance(center_point, active_position) > movementControls[0].width / 2)
            {
                if (Camera.mainCamera.isOrthoGraphic)
                    active_mousepos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                else
                {
                    Ray directedRay = Camera.mainCamera.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
                    if (Physics.Raycast(directedRay, out directedRayInfo))
                    {
                        //Debug.DrawRay(directedRay.origin, directedRay.direction, Color.red, 1);
                        active_mousepos = new Vector2(directedRayInfo.point.x, directedRayInfo.point.z);
                    }
                }

                if (skill_mode == ActiveSkillMode.Melee)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Camera.mainCamera.isOrthoGraphic)
                            init_pos = active_position;
                        else
                            init_pos = active_mousepos;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        if (Camera.mainCamera.isOrthoGraphic)
                            final_pos = active_position;
                        else
                            final_pos = active_mousepos;

                        // Only do a MeleeSwipe if length of swipe is greater than a value
                        if (((final_pos - init_pos).magnitude >= 30) && (PlayerSpecificObject.isAllowedCreation() && !SkillTogglesContains(active_position)))
                            new PlayerSpecificObject((GameObject)Resources.Load("Prefabs/MeleeSwipe"), "Melee", transform.position, init_pos, final_pos);
                    }
                }
                else if (skill_mode == ActiveSkillMode.Ranged)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (Camera.mainCamera.isOrthoGraphic)
                            init_pos = active_position;
                        else
                            init_pos = active_mousepos;
                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        if (Camera.mainCamera.isOrthoGraphic)
                            final_pos = active_position;
                        else
                            final_pos = active_mousepos;

                        if (PlayerSpecificObject.isAllowedCreation() && !SkillTogglesContains(active_position))
                            new PlayerSpecificObject((GameObject)Resources.Load("Prefabs/ArrowTemplate"), "Arrow", transform.position, init_pos, final_pos);
                    }
                }

                if (Input.GetMouseButton(0))
                {
                    if (!touchRecorder.isNewTouch())
                    {
                        touchRecorder.setNewTouch(true);
                        touchRecorder.setTrailRendererObject(Resources.Load("Prefabs/SwipeTrail"));
                        touchRecorder.addTouch(active_mousepos);

                        Vector3 calculatedPoint = Vector3.zero;
                        if (Camera.mainCamera.isOrthoGraphic)
                            calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
                        else
                            calculatedPoint = new Vector3(active_mousepos.x, .5f, active_mousepos.y);

                        touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
                    }

                    if (touchRecorder.isAllowedAdd())
                    {
                        touchRecorder.addTouch(active_mousepos);

                        Vector3 calculatedPoint = Vector3.zero;
                        if (Camera.mainCamera.isOrthoGraphic)
                            calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
                        else
                            calculatedPoint = new Vector3(active_mousepos.x, .5f, active_mousepos.y);

                        touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
                    }
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    touchRecorder.addTouch(active_mousepos);

                    Vector3 calculatedPoint = Vector3.zero;
                    if (Camera.mainCamera.isOrthoGraphic)
                        calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
                    else
                        calculatedPoint = new Vector3(active_mousepos.x, .5f, active_mousepos.y);

                    touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
                    touchRecorder.determineShapeApproximation();

                    // Determines the Direction of a particular swipe
                    DirectionDetermination();

                    touchRecorder.Reset();
                }
            }

            if (Input.GetMouseButtonUp(0) && skill_toggle_pos[0].Contains(active_position))
            {
                SetSelectedToggle(ActiveSkillMode.Melee);
            }
            else if (Input.GetMouseButtonUp(0) && skill_toggle_pos[1].Contains(active_position))
            {
                SetSelectedToggle(ActiveSkillMode.Ranged);
            }
        }
        else
        {
            // Used for Multi-Touch Swiping Determination
            for (int t = 0; t < Input.touchCount; t++)
            {
                Vector2 active_position = new Vector2(Input.GetTouch(t).position.x, Screen.height - Input.GetTouch(t).position.y);
                if (Vector2.Distance(center_point, active_position) < movementControls[0].width / 2)
                {
                    active_mousepos = new Vector2(Input.GetTouch(t).position.x, Screen.height - Input.GetTouch(t).position.y);
                    active_movement_toggle = true;
                }

                // Only do this if the swipe isn't within the Movement Controller
                if (Vector2.Distance(center_point, active_position) > movementControls[0].width / 2)
                {
                    if (Camera.mainCamera.isOrthoGraphic)
                        active_mousepos = new Vector2(Input.GetTouch(t).position.x, Input.GetTouch(t).position.y);
                    else
                    {
                        Ray directedRay = Camera.mainCamera.ScreenPointToRay(new Vector2(Input.GetTouch(t).position.x, Input.GetTouch(t).position.y));
                        if (Physics.Raycast(directedRay, out directedRayInfo))
                        {
                            //Debug.DrawRay(directedRay.origin, directedRay.direction, Color.red, 1);
                            active_mousepos = new Vector2(directedRayInfo.point.x, directedRayInfo.point.z);
                        }
                    }

                    if (skill_mode == ActiveSkillMode.Melee)
                    {
                        if (Input.GetTouch(t).phase == TouchPhase.Began)
                        {
                            if (Camera.mainCamera.isOrthoGraphic)
                                init_pos = active_position;
                            else
                                init_pos = active_mousepos;
                        }
                        else if (Input.GetTouch(t).phase == TouchPhase.Ended)
                        {
                            if (Camera.mainCamera.isOrthoGraphic)
                                final_pos = active_position;
                            else
                                final_pos = active_mousepos;

                            // Only Do a MeleeSwipe if the magnitude is greater than a value
                            if (((final_pos - init_pos).magnitude >= 30) && (PlayerSpecificObject.isAllowedCreation() && !SkillTogglesContains(active_position)))
                                new PlayerSpecificObject((GameObject)Resources.Load("Prefabs/MeleeSwipe"), "Melee", transform.position, init_pos, final_pos);
                        }
                    }
                    else if (skill_mode == ActiveSkillMode.Ranged)
                    {
                        if (Input.GetTouch(t).phase == TouchPhase.Began)
                        {
                            if (Camera.mainCamera.isOrthoGraphic)
                                init_pos = active_position;
                            else
                                init_pos = active_mousepos;
                        }
                        else if (Input.GetTouch(t).phase == TouchPhase.Ended)
                        {
                            if (Camera.mainCamera.isOrthoGraphic)
                                final_pos = active_position;
                            else
                                final_pos = active_mousepos;

                            if (PlayerSpecificObject.isAllowedCreation() && !SkillTogglesContains(active_position))
                                new PlayerSpecificObject((GameObject)Resources.Load("Prefabs/ArrowTemplate"), "Arrow", transform.position, init_pos, final_pos);
                        }
                    }

                    if (Input.GetTouch(t).phase == TouchPhase.Moved)
                    {
                        if (!touchRecorder.isNewTouch())
                        {
                            touchRecorder.setNewTouch(true);
                            touchRecorder.setTrailRendererObject(Resources.Load("Prefabs/SwipeTrail"));
                            touchRecorder.addTouch(active_mousepos);

                            Vector3 calculatedPoint = Vector3.zero;
                            if (Camera.mainCamera.isOrthoGraphic)
                                calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
                            else
                                calculatedPoint = new Vector3(active_mousepos.x, .5f, active_mousepos.y);

                            touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
                        }

                        if (touchRecorder.isAllowedAdd())
                        {
                            touchRecorder.addTouch(active_mousepos);

                            Vector3 calculatedPoint = Vector3.zero;
                            if (Camera.mainCamera.isOrthoGraphic)
                                calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
                            else
                                calculatedPoint = new Vector3(active_mousepos.x, .5f, active_mousepos.y);

                            touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
                        }
                    }
                    else if (Input.GetTouch(t).phase == TouchPhase.Ended)
                    {
                        touchRecorder.addTouch(active_mousepos);

                        Vector3 calculatedPoint = Vector3.zero;
                        if (Camera.mainCamera.isOrthoGraphic)
                            calculatedPoint = Camera.mainCamera.ScreenToWorldPoint(active_mousepos);
                        else
                            calculatedPoint = new Vector3(active_mousepos.x, .5f, active_mousepos.y);

                        touchRecorder.getTrailRenderer().transform.position = new Vector3(calculatedPoint.x, .5f, calculatedPoint.z);
                        touchRecorder.determineShapeApproximation();

                        // Determines the Direction of a particular swipe
                        DirectionDetermination();

                        touchRecorder.Reset();
                    }
                }

                if (Input.GetTouch(t).phase == TouchPhase.Ended && skill_toggle_pos[0].Contains(active_position))
                {
                    SetSelectedToggle(ActiveSkillMode.Melee);
                }
                else if (Input.GetTouch(t).phase == TouchPhase.Ended && skill_toggle_pos[1].Contains(active_position))
                {
                    SetSelectedToggle(ActiveSkillMode.Ranged);
                }
            }
        }

        PlayerSpecificObject.UpdateGameSpecificObjects();
        if (touchRecorder.isNewTouch())
            touchRecorder.UpdateRecorder();
    }

    private void DirectionDetermination()
    {
        // Change these particular basis behaviors to your desired outcome within a particular case statement
        // NOTE: Within a Portrait Perspective (Android Build), the Directions are rotated 90 degrees. 
        // Meaning, the ShapeType.Right will now behave like the ShapeType.Up, ShapeType.Down will now behave
        // like ShapeType.Left.
        switch ((ShapeType)touchRecorder.getDeterminedShape)
        {
            case ShapeType.SinglePoint:
                Debug.Log("Single Point.");
                break;

            case ShapeType.DownLeft:
                Debug.Log("Down Left");
                break;

            case ShapeType.DownRight:
                Debug.Log("Down Right");
                break;

            case ShapeType.UpRight:
                Debug.Log("Up Right");
                break;

            case ShapeType.UpLeft:
                Debug.Log("Up Left");
                break;

            case ShapeType.Left:
                Debug.Log("Left");
                break;

            case ShapeType.Down:
                Debug.Log("Down");
                break;

            case ShapeType.Right:
                Debug.Log("Right");
                break;

            case ShapeType.Up:
                Debug.Log("Up");
                break;

            default:
                Debug.Log("Undefined.");
                break;
        }
    }

    // Changes the Frame of the MainPlayer Texture based off the normalized direction values
    private void ChangeMovementStateFromGridDir(Vector3 direction)
    {
        if (direction.x > .5)
            transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.MoveRight);
        else if (direction.x < -.5)
            transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.MoveLeft);
        else if (direction.z > 0)
            transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.MoveUp);
        else if (direction.z < 0)
            transform.GetComponent<AnimationBehavior>().SendMessage("setMovementState", MovementState.Idle);
    }

    private bool SkillTogglesContains(Vector2 mouse_pos)
    {
        for (int i = 0; i < skill_toggle_pos.Length; i++)
        {
            if (skill_toggle_pos[i].Contains(mouse_pos))
                return true;
        }

        return false;
    }

    private void SetSelectedToggle(ActiveSkillMode s_mode)
    {
        active_skill_toggle[(int)skill_mode, 0] = 0;
        skill_mode = s_mode;
        active_skill_toggle[(int)skill_mode, 0] = 1;
    }

    // Updates the Movement Direction based on position pressed in the Controller
    private void UpdatePositionThroughInputs()
    {
        float angle = 0.0f;
        float dx = 0.0f;
        float dy = 0.0f;

        if (center_point.x <= active_mousepos.x)
        {
            dy = center_point.y - active_mousepos.y;
            dx = active_mousepos.x - center_point.x;

            if (dx == 0)
                dx = 0.01f;

            angle = Mathf.Atan(dy / dx);
            active_angle = angle;
        }
        else
        {
            dy = active_mousepos.y - center_point.y;
            dx = center_point.x - active_mousepos.x;

            if (dx == 0)
                dx = 0.01f;

            angle = Mathf.PI + Mathf.Atan(dy / dx);
            active_angle = angle;
        }

        if (!Camera.mainCamera.isOrthoGraphic)
            transform.rotation = Quaternion.Euler(0, -(Mathf.Rad2Deg * active_angle) + 90, 0);

        displacement_mod.z = (PlayerSpeed * Mathf.Sin(active_angle)) * Time.deltaTime;
        displacement_mod.x = (PlayerSpeed * Mathf.Cos(active_angle)) * Time.deltaTime;

        if (Camera.mainCamera.isOrthoGraphic)
            transform.position += displacement_mod;
        else
            transform.rigidbody.AddForce(PlayerSpeed * displacement_mod.normalized, ForceMode.Force);

        if (this.GetComponent<AnimationBehavior>() != null)
            ChangeMovementStateFromGridDir(displacement_mod.normalized);

        displacement_mod = Vector3.zero;
        active_movement_toggle = false;
    }

    // This class controls the creation of particular MainPlayer specific objects
    private class PlayerSpecificObject
    {
        static ArrayList game_objects = new ArrayList();
        static bool allowed_creation = true;
        static float delay_time = 0.0f;
        Vector3 object_displacement = Vector3.zero;
        GameObject game_object;

        public PlayerSpecificObject(GameObject new_object, string type, Vector3 position, Vector3 init_m_pos, Vector3 final_m_pos)
        {
            game_object = new_object;

            if (type.Equals("Arrow"))
                FireArrow(position, init_m_pos, final_m_pos);
            else if (type.Equals("Melee"))
                DisplayMeleeSwipe(position, init_m_pos, final_m_pos);
        }

        // This converts the position pressed on the Screen to a World Point, determing
        // the proper position and angle to place the MeleeSwipe.
        private void DisplayMeleeSwipe(Vector3 position, Vector3 init_m_pos, Vector3 final_m_pos)
        {
            float angle = 0.0f;
            Vector3 s2wp_finalpos = Vector3.zero;
            Vector3 s2wp_initpos = Vector3.zero;

            if (Camera.mainCamera.isOrthoGraphic)
            {
                s2wp_finalpos = Camera.mainCamera.ScreenToWorldPoint(final_m_pos);
                s2wp_initpos = Camera.mainCamera.ScreenToWorldPoint(init_m_pos);
            }
            else
            {
                s2wp_finalpos = new Vector3(final_m_pos.x, .5f, final_m_pos.y);
                s2wp_initpos = new Vector3(init_m_pos.x, .5f, init_m_pos.y);
            }

            s2wp_finalpos.y = game_object.transform.position.y;
            s2wp_initpos.y = game_object.transform.position.y;

            Vector3 new_pos = new Vector3((s2wp_finalpos.x - (s2wp_finalpos.x - s2wp_initpos.x) / 2), s2wp_finalpos.y, (s2wp_finalpos.z - (s2wp_finalpos.z - s2wp_initpos.z) / 2));
            
            if (Camera.mainCamera.isOrthoGraphic)
                object_displacement = 3 * (new Vector3(new_pos.x - position.x, 0, position.z - new_pos.z)).normalized;
            else
                object_displacement = 5 * (new Vector3(new_pos.x - position.x, 0, new_pos.z - position.z)).normalized;

            object_displacement = position + object_displacement;
            object_displacement.y = Camera.mainCamera.isOrthoGraphic ? 0.5f : 3.0f;

            if (object_displacement.z <= position.z)
                angle = Mathf.Rad2Deg * Mathf.Atan((position.x - object_displacement.x) / (position.z - object_displacement.z));
            else
                angle = Mathf.Rad2Deg * (Mathf.PI + Mathf.Atan((position.x - object_displacement.x) / (position.z - object_displacement.z)));

            GameObject new_game_object = (GameObject)GameObject.Instantiate(game_object, object_displacement, Quaternion.Euler(0, angle, 0));
            new_game_object.GetComponent<SwipeBehavior>().setTexture((Texture2D)Resources.Load("Textures/MeleeSwipe_0"));
            game_objects.Add(new_game_object);

            allowed_creation = false;
        }

        // This converts the position pressed on the Screen to a World Point, determing
        // the proper position and angle to place the Arrow.
        private void FireArrow(Vector3 position, Vector3 init_m_pos, Vector3 final_m_pos)
        {
            float angle = 0.0f;
            Vector3 s2wp = Vector3.zero;

            if (Camera.mainCamera.isOrthoGraphic)
            {
                s2wp = Camera.mainCamera.ScreenToWorldPoint(final_m_pos);

                object_displacement.x = s2wp.x - position.x;
                object_displacement.y = 0;
                object_displacement.z = position.z - s2wp.z;
            }
            else
            {
                s2wp = new Vector3(final_m_pos.x, .5f, final_m_pos.y);

                object_displacement.x = s2wp.x - position.x;
                object_displacement.y = 0;
                object_displacement.z = s2wp.z - position.z;
            }

            if (s2wp.z <= position.z)
                angle = Mathf.Rad2Deg * Mathf.Atan(object_displacement.x / object_displacement.z);
            else
                angle = Mathf.Rad2Deg * (Mathf.PI + Mathf.Atan(object_displacement.x / object_displacement.z));

            GameObject new_game_object = (GameObject)GameObject.Instantiate(game_object, position, Quaternion.Euler(0, Camera.mainCamera.isOrthoGraphic ? angle : angle + 180, 0));
            new_game_object.GetComponent<ArrowBehavior>().SetProjectileForceDirection((Camera.mainCamera.isOrthoGraphic ? 1 : 5) * (init_m_pos - final_m_pos), object_displacement.normalized);
            game_objects.Add(new_game_object);

            allowed_creation = false;
        }

        // Updates all the active objects that MainPlayer has controlled, deleting them after a particular time interval
        public static void UpdateGameSpecificObjects()
        {
            for (int i = 0; i < game_objects.Count; i++)
            {
                GameObject selected_object = (GameObject)game_objects[i];
                
                if (selected_object.name.Contains("Arrow") && selected_object.GetComponent<ArrowBehavior>().GetAlphaMod() < 0)
                {
                    GameObject.Destroy(selected_object);
                    game_objects.Remove(selected_object);
                }
                else if (selected_object.name.Contains("MeleeSwipe") && selected_object.GetComponent<SwipeBehavior>().GetAlphaMod() < 0)
                {
                    GameObject.DestroyObject(selected_object);
                    game_objects.Remove(selected_object);
                }
            }

            // 0.5 second delay between a MeleeSwipe or Arrow
            if (delay_time > 0.25f && !allowed_creation)
            {
                delay_time = 0;
                allowed_creation = true;
            }
            else if (!allowed_creation)
                delay_time += Time.deltaTime;
        }

        public static bool isAllowedCreation()
        {
            return allowed_creation;
        }
    }
}
