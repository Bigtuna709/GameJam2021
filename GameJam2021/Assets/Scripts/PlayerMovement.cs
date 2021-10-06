using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float movement;
    public float JumpForce = 1;
    public AudioClip[] audioClips;

    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, run, jump;
    public string currentState;
    public string previousState;
    public string currentAnimation;

    private Rigidbody2D _rigidbody;

    public Vector2 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
        currentState = "Idle";
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        //if (movement != 0 && GetComponent<AudioSource>().isPlaying == false && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
            //PlayRandom();


        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.isImaginaryWorld = !GameManager.Instance.isImaginaryWorld;
            GameManager.Instance.SwapBetweenWorlds();
        }

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.01f)
        {
            Jump();
        }

        if (movement != 0)
        {
            if (!currentState.Equals("Jump"))
            {
                SetCharacterState("Run");
            }
            if (movement > 0)
            {
                transform.localScale = new Vector2(-1f, 1f);
            }
            else
            {
                transform.localScale = new Vector2(1f, 1f);
            }
        }
        else if (!currentState.Equals("Jump"))
        {
            SetCharacterState("Idle");
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);

        if (!currentState.Equals("Jump"))
        {
            previousState = currentState;
        }
        SetCharacterState("Jump");
        
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
    }   

    void PlayRandom()
    {
        GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
        GetComponent<AudioSource>().Play();
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            collision.GetComponent<ObjectiveController>().Collect();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava") && GameManager.Instance.isImaginaryWorld)
        {
            Death();
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
    }

    public void Death()
    {
        print("You died!");
        GameManager.Instance.ResetLevel();
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if(animation.name.Equals(currentAnimation))
        {
            return;
        }
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }

    private void AnimationEntry_Complete(TrackEntry trackEntry)
    {
        if(currentState.Equals("Jump"))
        {
            SetCharacterState(previousState);
        }
    }

    public void SetCharacterState(string state)
    {

        if(state.Equals("Run"))
        {
            SetAnimation(run, true, 1f);
        }
        else if(state.Equals("Jump"))
        {
            SetAnimation(jump, false, 1f);
        }
        else
        {
            SetAnimation(idle, true, 1f);
        }
        currentState = state;
    }
}
