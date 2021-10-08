using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompletedAnim : MonoBehaviour
{
    public RectTransform Button;

    // Start is called before the first frame update
    void Start()
    {
        Button.GetComponent<Animator>().Play("CompletedIMG");
    }

}
