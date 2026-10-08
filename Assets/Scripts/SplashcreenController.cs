using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SplashscreenController : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CargarMenu());
    }

    IEnumerator CargarMenu()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("MainMenu");
    }
}