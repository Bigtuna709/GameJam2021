using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ResetAllProgress : MonoBehaviour
{

    public void ResetUnlocks()
    {
        PlayerPrefs.DeleteAll();

        //SceneManager.LoadScene("LevelSelect");

    }
}