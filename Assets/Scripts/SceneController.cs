using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void LoadNextScene()
    {
        var currentSceneIDX = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentSceneIDX);
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

	public void QuitApplication()
	{
		Application.Quit();
	}
}
