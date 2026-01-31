using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class ButtonInterface : MonoBehaviour
{
    bool mouseEncima = false;
    SpriteRenderer sr;
    private Color mainColor;
    public GameObject mainWindow;
    public GameObject otherWindowA;
    public GameObject otherWindowB;
    public GameObject otherWindowC;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        mainColor = sr.color;
    }
    void Update()
    {
        DetectarHover();

        if (mouseEncima && Mouse.current.leftButton.wasPressedThisFrame)
        {
            OnClick();
        }
    }

    void DetectarHover()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(
            Mouse.current.position.ReadValue()
        );

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        bool estabaEncima = mouseEncima;
        mouseEncima = hit.collider != null && hit.collider.gameObject == gameObject;

        if (mouseEncima && !estabaEncima)
            OnMouseEnter2D();

        if (!mouseEncima && estabaEncima)
            OnMouseExit2D();
    }

    void OnMouseEnter2D()
    {
        sr.color = new Color(0.5871751f, 0.6969928f, 0.8584906f, 1f);
    }

    void OnMouseExit2D()
    {
        sr.color = mainColor;
    }

    public void OnClick()
    {
        sr.color = new Color(0.1585974f, 0.2626831f, 0.4150943f, 1f);
        StartCoroutine(DespuesDelClick());
    }

    IEnumerator DespuesDelClick()
    {
        foreach (Transform child in gameObject.transform)
            child.gameObject.SetActive(true);
        foreach (Transform child in otherWindowA.transform)
            child.gameObject.SetActive(false);
        foreach (Transform child in otherWindowB.transform)
            child.gameObject.SetActive(false);
        foreach (Transform child in otherWindowC.transform)
            child.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        sr.color = mainColor;
    }
}
