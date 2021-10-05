using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MovementSpeed = 1;
    public float movement;
    public float JumpForce = 1;
    public AudioClip[] audioClips;

    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (movement != 0 && GetComponent<AudioSource>().isPlaying == false && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
            PlayRandom();

        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rigidbody.velocity.y) < 0.001f)
        {
            _rigidbody.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.isImaginaryWorld = !GameManager.Instance.isImaginaryWorld;
            GameManager.Instance.SwapBetweenWorlds(GameManager.Instance.allPlatforms);
        }
    }
    private void FixedUpdate()
    {
        movement = Input.GetAxis("Horizontal");
        transform.position += new Vector3(movement, 0, 0) * Time.deltaTime * MovementSpeed;
    }

    void PlayRandom()
    {
        GetComponent<AudioSource>().clip = audioClips[Random.Range(0, audioClips.Length)];
        GetComponent<AudioSource>().Play();
    }
}
