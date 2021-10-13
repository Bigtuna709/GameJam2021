using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void PlayGame()
    {
        StartCoroutine(ChangeSceneWithDlay(1));
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void MainMenu()
    {
        StartCoroutine(ChangeSceneWithDlay(0));
    }
    public void ExitGame()
    {
        Debug.Log("Exit Game!"); 
        StartCoroutine(QuitGameWithDelay());
    }

    public void NextLevel()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private IEnumerator ChangeSceneWithDlay(int sceneId)
    {
        yield return new WaitForSecondsRealtime(0.4f);
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneId);
    }
    private IEnumerator QuitGameWithDelay()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        Application.Quit();
    }
}
