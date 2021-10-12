using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    public GameObject sound;
    public Animator animator;

    public void Collect()
    {
        sound.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
        GameManager.Instance.CollectObjective();
    }
}
