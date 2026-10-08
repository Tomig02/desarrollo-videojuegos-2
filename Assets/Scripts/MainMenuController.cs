using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void Creditos()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void Opciones()
    {
        SceneManager.LoadScene("Opciones");
    }
}