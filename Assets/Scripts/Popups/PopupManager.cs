
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    [SerializeField] private PopupEventSO popupEvent;
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI tituloTexto;
    [SerializeField] private TextMeshProUGUI mensajeTexto;
    [SerializeField] private Button botonAceptar;
    [SerializeField] private Button botonCancelar;

    private IPopupCommand comandoAceptar;
    private IPopupCommand comandoCancelar;

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

    private void MostrarPopup(
        string titulo,
        string mensaje,
        IPopupCommand aceptar,
        IPopupCommand cancelar)
    {
        if (popupPanel == null)
        {
            Debug.LogWarning("No se asignó el panel del pop-up.");
            return;
        }

        comandoAceptar = aceptar;
        comandoCancelar = cancelar;

        if (tituloTexto != null)
            tituloTexto.text = titulo;

        if (mensajeTexto != null)
            mensajeTexto.text = mensaje;

        botonAceptar.onClick.RemoveListener(Aceptar);
        botonCancelar.onClick.RemoveListener(Cancelar);

        botonAceptar.onClick.AddListener(Aceptar);
        botonCancelar.onClick.AddListener(Cancelar);

        popupPanel.SetActive(true);
    }

    private void Aceptar()
    {
        IPopupCommand comando = comandoAceptar;
        CerrarPopup();
        comando?.Execute();
    }

    private void Cancelar()
    {
        IPopupCommand comando = comandoCancelar;
        CerrarPopup();
        comando?.Execute();
    }

    public void CerrarPopup()
    {
        if (popupPanel != null)
            popupPanel.SetActive(false);
    }
}
