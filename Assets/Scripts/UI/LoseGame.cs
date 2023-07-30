using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    public void ReloadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SaveParameters.levelActive);
    }
}
