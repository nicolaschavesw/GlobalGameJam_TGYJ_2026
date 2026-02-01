using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEvent : MonoBehaviour, IPointerClickHandler
{
    [Header("Event")]
    [SerializeField] UnityEvent OnEvent;
    public bool IsAble = true;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnEvent?.Invoke();
    }
}