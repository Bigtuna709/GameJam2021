using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public PlayerMovement player;

    public List<PlatformController> allPlatforms = new List<PlatformController>();
    public List<ObjectiveController> objectives = new List<ObjectiveController>();

    public int collectedObjectives;
    public int totalObjectives;
    public int levelToUnlock = 2;

    public bool isImaginaryWorld;
    public bool isPaused;

    public string nextLevel = "Level02";

    public Text collectedObjectiveText;
    public Text totalObjectivesText;

    public GameObject win;
    public GameObject winCanvas;
    public GameObject pauseCanvas;
    public GameObject realWorldText;
    public GameObject imaginaryWorldText;

    private void Start()
    {
        collectedObjectives = 0;
        totalObjectives = objectives.Count;
        totalObjectivesText.text = totalObjectives.ToString();
        isImaginaryWorld = false;
        SwapBetweenWorlds();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
  
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button7))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button9))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseCanvas.SetActive(false);
        isPaused = false;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseCanvas.SetActive(true);
        isPaused = true;
    }

    public void SwapBetweenWorlds()
    {
        foreach (PlatformController platform in allPlatforms)
        {
            platform.gameObject.GetComponent<SpriteRenderer>().sprite = isImaginaryWorld ? platform.imaginaryWorldSprite : platform.realWorldSprite;
            if(platform.GetComponent<BoxCollider2D>() != null)
            platform.GetComponent<BoxCollider2D>().enabled = platform.isImaginary ? isImaginaryWorld : !isImaginaryWorld;

            if(platform.animator != null && isImaginaryWorld)
            {
                platform.animator.enabled = true;
            }
            else if(platform.animator != null && !isImaginaryWorld)
            {
                platform.animator.enabled = false;
            }
        }
        if(!isImaginaryWorld)
        {
            realWorldText.SetActive(true);
            imaginaryWorldText.SetActive(false);
        }
        else
        {
            realWorldText.SetActive(false);
            imaginaryWorldText.SetActive(true);
        }
    }

    public void CollectObjective()
    {
        collectedObjectives++;
        collectedObjectiveText.text = collectedObjectives.ToString();
        print(collectedObjectives + " / " + totalObjectives + " items collected!");
        if(collectedObjectives == totalObjectives)
        {
            StartCoroutine(Victory());
            PlayerPrefs.SetInt("levelReached", levelToUnlock);
        }
    }

    public IEnumerator Victory()
    {
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(.5f);
        Time.timeScale = 0f;
        win.GetComponent<AudioSource>().Play();
        print("You won!");
        winCanvas.SetActive(true);
        //ResetLevel();
    }

    public void ResetLevel()
    {
        winCanvas.SetActive(false);
        player.ResetPosition();
        objectives.ForEach(o => o.gameObject.SetActive(true));
        collectedObjectives = 0;
        collectedObjectiveText.text = collectedObjectives.ToString();
        isImaginaryWorld = false;
        SwapBetweenWorlds();
    }
}
