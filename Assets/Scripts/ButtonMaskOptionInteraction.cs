using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System.Collections;

public class ButtonMaskOptionInteraction : MonoBehaviour
{
    bool mouseEncima = false;
    [Header("Eventos")]
    public UnityEvent OnEvent;

    void Update()
    {
        DetectarClick();
        if (mouseEncima && Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClick();
        }
    }

    void DetectarClick()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        mouseEncima = hit.collider != null && hit.collider.gameObject == gameObject;
    }

    public void OnClick()
    {
        EjecutarEvento();
    }

    public void EjecutarEvento()
    {
        OnEvent?.Invoke();
    }
}
