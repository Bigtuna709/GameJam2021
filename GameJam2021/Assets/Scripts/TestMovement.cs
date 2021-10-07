using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TestMovement : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, run, jump;

    public string currentState;
    public float speed;
    public float movement;
    private Rigidbody2D _rigidbody;

    public string currentAnimation;
    public float jumpSpeed;
    public string previousState;

    public AudioClip[] audioClips;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        currentState = "Idle";
        SetCharacterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }

    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        if (animation.name.Equals(currentAnimation))
        {
            return;
        }
        Spine.TrackEntry animationEntry = skeletonAnimation.state.SetAnimation(0, animation, loop);
        animationEntry.TimeScale = timeScale;
        animationEntry.Complete += AnimationEntry_Complete;
        currentAnimation = animation.name;
    }

    //Do something after animation completes
    private void AnimationEntry_Complete(Spine.TrackEntry trackEntry)
    {
        if (currentState.Equals("Jump"))
        {
            SetCharacterState(previousState);
        }
    }

    public void SetCharacterState(string state)
    {
         if (state.Equals("Run"))
        {
            SetAnimation(run, true, 1f);
        }
        else if (state.Equals("Jump"))
        {
            SetAnimation(jump, false, 0.5f);
        }
        else
        {
            SetAnimation(idle, true, 1f);
        }

        currentState = state;
    }

    public void Move()
    {
        movement = Input.GetAxis("Horizontal");
        _rigidbody.velocity = new Vector2(movement * speed, _rigidbody.velocity.y);
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
        else
        {
            if (!currentState.Equals("Jump"))
            {
                SetCharacterState("Idle");
            }
                
        }


    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, jumpSpeed);
        if (!currentState.Equals("Jump"))
        {
            previousState = currentState;
        }
        SetCharacterState("Jump");
    }

    void PlayRandom()
    {
        GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
        GetComponent<AudioSource>().Play();
    }
}
