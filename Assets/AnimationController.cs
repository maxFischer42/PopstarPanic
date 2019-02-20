using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerController;

public class AnimationController : MonoBehaviour {

    private Controller controller;
    private float rate;
    private Sprite[] currentCells;
    private int currentCell;
    public AnimClip Idle;
    public AnimClip Walking;
    public AnimClip Ducking;
    public AnimClip Jumping;
    public AnimClip Falling;
    public AnimClip Door;
    public AnimClip Cutter;
    public AnimClip currentAnim;
    private Controller.State currentState;
    private float timer;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = GetComponent<Controller>();
        currentAnim = Idle;
        ResetAnim();
        Color color = new Color(PlayerPrefs.GetFloat("R"), PlayerPrefs.GetFloat("G"), PlayerPrefs.GetFloat("B"));
        spriteRenderer.color = color;
    }
    
    void Update ()
    {
        ChangeController();
        timer++;
        if (timer > rate)
        {
            if (currentCell + 1 > currentCells.Length - 1)
            {
                if (currentAnim == Jumping)
                    controller.finishJump = true;
                currentCell = 0;
            }
            else
                currentCell++;
            spriteRenderer.sprite = currentCells[currentCell];
            timer = 0;
        }
	}

    public void ChangeController()
    {
        if (currentState != controller.currentState)
        {
            currentState = controller.currentState;
            switch (currentState)
            {
                case Controller.State.Idle:
                    currentAnim = Idle;
                    break;
                case Controller.State.Walking:
                    currentAnim = Walking;
                    break;
                case Controller.State.Ducking:
                    currentAnim = Ducking;
                    break;
                case Controller.State.Jumping:
                    currentAnim = Jumping;
                    break;
                case Controller.State.Falling:
                    currentAnim = Falling;
                    break;
                case Controller.State.Door:
                    currentAnim = Door;
                    break;
                case Controller.State.Cut:
                    currentAnim = Cutter;
                    break;
            }
            ResetAnim();
        }
    }

    void ResetAnim()
    {
        currentCells = currentAnim.cells;
        currentCell = 0;
        timer = 0;
        spriteRenderer.sprite = currentCells[currentCell];
        rate = currentAnim.frameRate;
    }
}
