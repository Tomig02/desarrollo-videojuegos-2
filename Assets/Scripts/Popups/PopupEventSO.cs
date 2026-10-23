
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PopupEvent", menuName = "Events/Popup Event")]
public class PopupEventSO : ScriptableObject
{
    public event Action<string, string, IPopupCommand, IPopupCommand> OnPopupRequested;

    public void Raise(
        string titulo,
        string mensaje,
        IPopupCommand comandoAceptar,
        IPopupCommand comandoCancelar = null)
    {
        OnPopupRequested?.Invoke(
            titulo,
            mensaje,
            comandoAceptar,
            comandoCancelar
        );
    }
}
