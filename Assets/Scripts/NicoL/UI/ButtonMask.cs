using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonMask : MonoBehaviour, IPointerClickHandler
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer spriteIcon;
    [SerializeField] private TextMeshPro textName;
    [SerializeField] private TextMeshPro textCooldown;

    [Header("DATA")]
    [SerializeField] private MaskData dataMask;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!dataMask) return;
        Debug.Log("Hizo click");

        GameManager.Instance.SetCurrentMask(dataMask.nameMask);
        EventsManager.Instance.TriggerEvent(BasicEvents.OnChangeMaskSelection, dataMask.nameMask);
    }

    private void OnValidate()
    {
        if (dataMask != null)
        {
            if (spriteIcon != null)
                spriteIcon.sprite = dataMask.iconMask;

            if (textName != null)
                textName.text = dataMask.nameMask.ToString();
        }
    }
}
