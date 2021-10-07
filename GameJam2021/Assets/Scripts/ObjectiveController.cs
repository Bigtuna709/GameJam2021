using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    public GameObject sound;
    public Animator animator;

    public void Awake()
    {
        animator.Play("MicToy_a");    
    }

    public void Collect()
    {
        gameObject.SetActive(false);
        GameManager.Instance.CollectObjective();
        sound.GetComponent<AudioSource>().Play();
    }
}
