using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGame : MonoBehaviour
{
    // Функция перезапуска уровня
    public void ReloadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SaveParameters.levelActive);
    }
}
