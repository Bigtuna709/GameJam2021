using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    public GameObject sound;

    public void Collect()
    {
        gameObject.SetActive(false);
        GameManager.Instance.CollectObjective();
        sound.GetComponent<AudioSource>().Play();
    }
}
