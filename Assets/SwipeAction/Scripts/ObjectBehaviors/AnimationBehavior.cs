using UnityEngine;
using System.Collections;

/*
@Author: Chazix Scripts
@Revision: 10/22/12
@Description: This controls the frames of a
    particular sprite sheet
*/

enum MovementState
{
    Idle,
    MoveUp = 1,
    MoveRight,
    MoveLeft
};

public class AnimationBehavior : MonoBehaviour
{
    enum EmoState
    {
        Sad,
        Nuetral = 1,
        Happy
    };

    public Texture2D TextureSheet;
    public float FrameTime;
    public int MaxFrames;

    private Texture2D active_frame;
    private MovementState mv_state = MovementState.Idle;
    private EmoState emo_state = EmoState.Happy;

    private int activeFrame = 0;
    private bool frame_changed = false;
    private float elapsedTime = 0.0f;

	// Use this for initialization
	void Start () {
        active_frame = new Texture2D(59, 64);
        setTextureFrame(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (frame_changed)
        {
            transform.renderer.material.mainTexture = active_frame;
            frame_changed = false;
        }

        if (elapsedTime > FrameTime)
        {
            setTextureFrame(activeFrame++);

            if (activeFrame >= MaxFrames)
                activeFrame = 0;

            elapsedTime = 0.0f;
        }

        elapsedTime += Time.deltaTime;
	}

    private void setMovementState(MovementState new_state)
    {
        mv_state = new_state;
        setTextureFrame(activeFrame);
    }

    private void setTextureFrame(int frm)
    {
        // (four states (in order))
        // x = 59 * frm_n + ((944/4) * (mv_state))
        // 59        : frame_width
        // frm_n     : frame_number (0, 1, 2, 3, ..)
        // 944       : frame_sheet_width
        // 4         : four different animations
        // mv_state  : the current active MovementState enum int value

        // y = 0 + (64 * (emo_state))
        // 0         : initial_position_y
        // 64        : frame_height
        // emo_state : current active EmoState int value

        active_frame.SetPixels(TextureSheet.GetPixels(active_frame.width * frm + ((TextureSheet.width / 4) * (int)mv_state), active_frame.height * (int)emo_state, active_frame.width, active_frame.height));
        active_frame.Apply();
        frame_changed = true;
    }

    public int getActiveFrame()
    {
        return activeFrame;
    }
}
