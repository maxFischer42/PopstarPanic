using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PlayerController
{

    public class Controller : MonoBehaviour
    {

        public bool isMap;
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
        public float jumpDistanceDelta = 0.25f;
        public DetectGround feet;
        public WallJumpDetection leftWall;
        public WallJumpDetection rightWall;
        public int currentJumps;
        public float jumpBoost = 3f;
        public AudioClip boostedJumpSound;
        public GameObject boostedJumpEffect;


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
            feet = GetComponentInChildren<DetectGround>();
            GetComponent<Rigidbody2D>().gravityScale = 2f;
            mySpeed = 0.4f;
            maxDistanceDelta = 0.15f;
            jumpAccelerate = 0.2f;
            jumpDistanceDelta = 0.15f;
            GetComponent<PolygonCollider2D>().offset = new Vector2(-0.1f,0f);
            if(isMap)
            {
                float x = PlayerPrefs.GetFloat("memoryX");
                float y = PlayerPrefs.GetFloat("memoryY");
                Vector2 startPos = new Vector2(x, y);
                transform.localPosition = startPos;
            }
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
            if(GetComponent<SpriteRenderer>().flipX)
            {
                GetComponent<PolygonCollider2D>().offset = new Vector2(-0.12f, 0f);
            }
            else
            {
                GetComponent<PolygonCollider2D>().offset = new Vector2(-0.1f, 0f);
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
            {
                AudioClip aa = jumpSound;
                if (Grounded() && currentJumps >= 1)
                {
                    aa = boostedJumpSound;
                    GameObject eff = (GameObject)Instantiate(boostedJumpEffect,feet.transform);
                    eff.transform.parent = null;
                    Destroy(eff, 2.5f);
                }
                Camera.main.GetComponent<AudioSource>().PlayOneShot(aa);
                currentJumps++;
            }
            if (myInput.jumpInput && isGrounded && !finishJump && currentState != State.Falling)
                GetState(myInput.horizontalInput, State.Jumping);
            else if (myInput.jumpInput && finishJump && currentState == State.Falling && feet.hitGround)
            {
                GetState(myInput.horizontalInput, State.Jumping);
            }
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
            transform.position = Vector2.MoveTowards(transform.position, transform.position + direction, jumpDistanceDelta);
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
            
            if (Grounded() && currentJumps > 1)
            {
                Vector2 vel = GetComponent<Rigidbody2D>().velocity;
                //vel = new Vector2(0f, Mathf.Abs(vel.y));
                vel = new Vector2(0, jumpBoost);
                GetComponent<Rigidbody2D>().velocity = vel;
                Debug.Log("Boosted jump");
                
            }
            isGrounded = false;
            Vector3 dir = new Vector2(0f, 1f);
            dir *= jumpAccelerate;
            transform.position = Vector2.MoveTowards(transform.position, transform.position + dir, jumpDistanceDelta);

        }

        void Cut()
        {
            cutter.CutCommand();
            finishJump = true;
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

        bool Grounded()
        {
            Vector2 position = transform.position;
            Vector2 direction = Vector2.down;
            float distance = 0.1f;

            RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, LayerMask.NameToLayer("Ground"));
            if (hit.collider != null)
            {
                
                return false;
            }
            float dis = feet.transform.position.y - hit.point.y;
       //     Debug.Log(dis.ToString());
            return true;
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
            if (Input.GetButtonDown("Jump"))
                jumpInput = true;
            else
                jumpInput = false;
        }
    }

}