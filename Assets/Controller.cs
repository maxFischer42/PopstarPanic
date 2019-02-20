using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerController
{

    public class Controller : MonoBehaviour
    {

        public float mySpeed = 1.0f;

        public enum State { Idle, Walking, Jumping, Damage, FireDamage, Ducking, Falling, Door, Cut};
        public State currentState = State.Idle;
        public float maxDistanceDelta = 0.5f;
        private Vector3 direction;
        public float jumpAccelerate = 2f;
        public Collider2D defaultHitBox;
        public Collider2D colliderHitBox;
        public bool isGrounded;
        public bool finishJump = false;
        public float jumpHorizontal = 2f;
        public AudioClip jumpSound;
        public bool atDoor;
        public Cutter cutter;
        public float fallTimer;

        void Flip(float _dir)
        {
            if(_dir > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if(_dir < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }

        private void Start()
        {
            GetComponent<Rigidbody2D>().gravityScale = 1.5f;

        }

        void Update()
        {
            Inputs();
            if(!isGrounded && !finishJump)
            {
                direction = new Vector2(new GetInputs().horizontalInput, 0f);
                direction *= mySpeed * jumpHorizontal;
                transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, maxDistanceDelta);
            }

            if (currentState != State.Ducking)
                Duck(false);

            switch (currentState)
            {
                case State.Cut:
                    Cut();
                    break;
                case State.Walking:
                    Walk();
                    break;
                case State.Idle:
                    Idle();
                    break;
                case State.Ducking:
                    Duck(true);
                    break;
                case State.Jumping:
                    Jump();
                    break;
                case State.Falling:
                    Fall();
                    break;
                case State.Door:
                    Door();
                    break;                
            }
        }

        void Inputs()
        {
            GetInputs myInput = new GetInputs();
            Flip(myInput.horizontalInput);
            if (myInput.attackInput)
            {
                GetState(0f, State.Cut);
            }
            if (cutter.throwing)
                return;
            if (Input.GetButtonDown("Jump") && isGrounded)
                Camera.main.GetComponent<AudioSource>().PlayOneShot(jumpSound);
            if (myInput.jumpInput && isGrounded && !finishJump)
                GetState(myInput.horizontalInput, State.Jumping);
            else if (!isGrounded && finishJump)
                GetState(myInput.horizontalInput, State.Falling);
            else if (myInput.horizontalInput != 0 && isGrounded)
                GetState(myInput.horizontalInput, State.Walking);
            else if (myInput.horizontalInput == 0f && myInput.verticalInput == 0f && !myInput.attackInput && isGrounded)
                GetState(0f, State.Idle);
            else if (myInput.verticalInput < 0f && myInput.horizontalInput == 0f && !myInput.attackInput && isGrounded)
                GetState(0f, State.Ducking);
            else if (myInput.verticalInput > 0 && atDoor)
                GetState(0f, State.Door);
            

    }

        void GetState(float _float, State _state)
        {
            currentState = _state;
            direction = new Vector2(_float, 0f);
        }

        void Door()
        {
            
        }

        void Idle()
        {
            
        }

        void Fall()
        {
            finishJump = true;
            isGrounded = false;
            direction *= mySpeed / 2;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, maxDistanceDelta);
            if (GetComponent<Rigidbody2D>().velocity.y < -6f)
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, -6f);
            fallTimer += Time.deltaTime;
            if(fallTimer > 2f)
            {
                fallTimer = 0f;
                currentState = State.Jumping;
            }
        }

        void Jump()
        { 
            isGrounded = false;
            Vector3 dir = new Vector2(0f, 1f);
            dir *= jumpAccelerate;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, maxDistanceDelta);

        }

        void Cut()
        {
            cutter.CutCommand();
        }

        void Walk()
        {
            direction *= mySpeed;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, maxDistanceDelta);
        }

        void Duck(bool _isDucking)
        {
            defaultHitBox.enabled = !_isDucking;
            colliderHitBox.enabled = _isDucking;
        }

    }

    public class GetInputs
    {
        public float horizontalInput;
        public float verticalInput;
        public bool attackInput;
        public bool jumpInput;

        public GetInputs()
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            attackInput = Input.GetButton("Attack");
            jumpInput = Input.GetButtonDown("Jump");
        }
    }

}