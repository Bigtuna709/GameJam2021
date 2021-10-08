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
    public AnimationReferenceAsset idle, idle2, run, jump;
    public string currentState;
    public string previousState;
    public string currentAnimation;

    [HideInInspector] public Rigidbody2D _rigidbody;

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
        if (movement != 0 && GetComponent<AudioSource>().isPlaying == false && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
            PlayRandom();


        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            GameManager.Instance.isImaginaryWorld = !GameManager.Instance.isImaginaryWorld;
            GameManager.Instance.SwapBetweenWorlds();
        }

        if (Mathf.Abs(_rigidbody.velocity.y) == 0)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            else if (movement == 0 && !currentState.Equals("Idle2") && !currentState.Equals("Idle"))
            {
                StartCoroutine(IdleState());
            }
        }
    }

    private IEnumerator IdleState()
    {
        SetCharacterState("Idle");
        yield return new WaitForSeconds(2f);
        if (_rigidbody.velocity.y < 0.1f )
        {
            SetCharacterState("Idle2");
        }
    }

    private void Jump()
    {
        _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
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

        if(movement != 0)
        {
            transform.localScale = movement > 0 ? new Vector2(-1f, 1f) : new Vector2(1f, 1f);
        }

        if (currentState.Equals("Run") && movement == 0)
        {
            //SetCharacterState("Idle");

        } else if (movement != 0 && (currentState.Equals("Idle") || Mathf.Abs(_rigidbody.velocity.y) == 0))
        {
            SetCharacterState("Run");
        }
    }   

    void PlayRandom()
    {
        //GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
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
        currentAnimation = animation.name;
    }

    public void SetCharacterState(string state)
    {
        currentState = state;
        
        if (state.Equals("Run"))
        {
            SetAnimation(run, true, 1f);
        }
        else if (state.Equals("Idle2"))
        {
            SetAnimation(idle2, true, 1f);
        }
        else if(state.Equals("Jump"))
        {
            SetAnimation(jump, false, .5f);
        }
        else
        {
            SetAnimation(idle, true, 1f);
        }
    }
}
