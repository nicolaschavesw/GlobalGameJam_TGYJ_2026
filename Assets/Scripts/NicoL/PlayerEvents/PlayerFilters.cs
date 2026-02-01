using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerFilters;

public class PlayerFilters : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private SpriteRenderer SR_PlayerWindow;
    [SerializeField] private float changeTime = .5f;

    [Header("Meeting")]
    [SerializeField] private List<Filter> AllFilters = new List<Filter>();

    [System.Serializable]
    public class Filter
    {
        public Masks mask;
        public Sprite filter;
    }


    private void OnEnable()
    {
        EventsManager.Instance.Subscribe(BasicEvents.OnChangeMaskSelection, SelectFilter);
    }

    private void OnDisable()
    {
        EventsManager.Instance.Unsubscribe(BasicEvents.OnChangeMaskSelection, SelectFilter);
    }

    Filter GetFilter(Masks mask)
    {
        return AllFilters.Find(filter => filter.mask == mask);
    }

    void SelectFilter(object newMask)
    {
        if (newMask == null) return;

        Masks selectedMask = (Masks)newMask;
        StartCoroutine(ChangeFilterCoroutine(selectedMask));
    }

    void ChangeFilter(Masks mask)
    {
        Filter filter = GetFilter(mask);
        if (filter == null) return;

        SR_PlayerWindow.sprite = filter.filter;
    }

    IEnumerator ChangeFilterCoroutine(Masks mask)
    {
        ChangeFilter(Masks.None);
        yield return new WaitForSeconds(changeTime);
        ChangeFilter(mask);
    }
}
