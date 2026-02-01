using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonMaskOptionInteraction : MonoBehaviour, IPointerClickHandler
{
    [Header("Eventos")]
    public UnityEvent OnEvent;

    public void EjecutarEvento()
    {
        OnEvent?.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.GetCurrentMask() == Masks.None) return;
        EjecutarEvento();
    }
}
