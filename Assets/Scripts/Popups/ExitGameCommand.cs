
using UnityEngine;

public class ExitGameCommand : IPopupCommand
{
    public void Execute()
    {
        Time.timeScale = 1f;

        Debug.Log("Saliendo del juego...");

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
