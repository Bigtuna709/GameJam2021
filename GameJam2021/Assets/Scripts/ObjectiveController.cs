using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
    public void Collect()
    {
        gameObject.SetActive(false);
        GameManager.Instance.CollectObjective();
    }
}
