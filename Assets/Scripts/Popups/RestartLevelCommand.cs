
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevelCommand : IPopupCommand
{
    public void Execute()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
