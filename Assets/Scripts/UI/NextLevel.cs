using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    // Функция старта следующего уровня
    public void OpenNextLevel()
    {
        SaveParameters.levelActive++;
        if (SceneManager.sceneCountInBuildSettings > SaveParameters.levelActive)
        {
            SceneManager.LoadScene(SaveParameters.levelActive);
            return;
        }
        SaveParameters.levelActive = 0;
        SceneManager.LoadScene(0);
    }
}
