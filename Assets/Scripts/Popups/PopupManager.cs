
using UnityEngine;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PopupEventSO popupEvent;
    [SerializeField] private GameObject popupPanel;

    private void OnEnable()
    {
        if (popupEvent != null)
            popupEvent.OnPopupRequested += MostrarPopup;
    }

    private void OnDisable()
    {
        if (popupEvent != null)
            popupEvent.OnPopupRequested -= MostrarPopup;
    }

    private void MostrarPopup(string titulo, string mensaje)
    {
        if (popupPanel == null)
        {
            Debug.LogWarning("No se asignó el panel del pop-up.");
            return;
        }

        popupPanel.SetActive(true);
        Debug.Log("Pop-up: " + titulo + " - " + mensaje);
    }

    public void CerrarPopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}
