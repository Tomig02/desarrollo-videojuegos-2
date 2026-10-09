
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransicionBase : MonoBehaviour
{
    [SerializeField] private string escenaDestino = "BaseScene";

    private void OnTriggerEnter(Collider otro)
    {
        if (otro.GetComponent<PlayerController>() != null)
        {
            SceneManager.LoadScene(escenaDestino);
        }
    }
}
