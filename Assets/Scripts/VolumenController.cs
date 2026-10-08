using UnityEngine;

public class VolumenController : MonoBehaviour
{
    public void CambiarVolumen(float volumen)
    {
        AudioListener.volume = volumen;
    }
}