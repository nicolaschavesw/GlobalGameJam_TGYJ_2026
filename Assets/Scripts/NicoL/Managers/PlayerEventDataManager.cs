using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem.LowLevel;

public class PlayerEventDataManager : MonoBehaviour
{
    private static PlayerEventDataManager Instance;

    [Header("All events")]
    public int currentEvent = -1;
    public List<PlayerEventData> events = new List<PlayerEventData>();
    [HideInInspector] public OrderedEvent[] eventsRandom;
    [HideInInspector] public List<MaskInfo> allMasksData;

    //Guardar todas las mascaras y sus datos
    [Header("All Masks")]
    public List<MaskData> allMasks = new List<MaskData>();

    [System.Serializable]
    public class OrderedEvent
    {
        public PlayerEventData data;
        public int order;
    }

    [System.Serializable]
    public class MaskInfo
    {
        public MaskData data;
        public int currentCooldown = 0;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        currentEvent = -1;
        Debug.Log("[EVENTS] - Seteando orden de los eventos...");
        RandomEvents();
        MasksCooldown();
    }

    private void OnEnable()
    {
        EventsManager.Instance.Subscribe(BasicEvents.OnUseMask, UseMask);

        //EventsManager.Instance.TriggerEvent(BasicEvents.OnUseMask, Masks.Indiferente);
        //EventsManager.Instance.TriggerEvent(BasicEvents.OnEndEvent);
    }
    private void OnDisable()
    {
        EventsManager.Instance.Unsubscribe(BasicEvents.OnUseMask, UseMask);
    }


    private void RandomEvents()
    {
        int total = events.Count;
        eventsRandom = new OrderedEvent[total];

        HashSet<int> usedOrders = new HashSet<int>();
        List<PlayerEventData> zeroEvents = new List<PlayerEventData>();

        foreach (var e in events)
        {
            if (e.numEvent > 0)
            {
                int index = e.numEvent - 1;
                eventsRandom[index] = new OrderedEvent
                {
                    data = e,
                    order = e.numEvent
                };
                usedOrders.Add(e.numEvent);
            }
            else
            {
                zeroEvents.Add(e);
            }
        }

        List<int> availableOrders = new List<int>();
        for (int i = 1; i <= total; i++)
        {
            if (!usedOrders.Contains(i))
                availableOrders.Add(i);
        }

        for (int i = 0; i < availableOrders.Count; i++)
        {
            int rand = Random.Range(i, availableOrders.Count);
            (availableOrders[i], availableOrders[rand]) =
            (availableOrders[rand], availableOrders[i]);
        }

        int zeroIndex = 0;
        for (int i = 0; i < eventsRandom.Length; i++)
        {
            if (eventsRandom[i] == null)
            {
                eventsRandom[i] = new OrderedEvent
                {
                    data = zeroEvents[zeroIndex],
                    order = availableOrders[zeroIndex]
                };
                zeroIndex++;
            }
        }
    }
    private void MasksCooldown()
    {
        foreach (var m in allMasks)
        {
            MaskInfo newData = new MaskInfo()
            {
                data = m,
            };

            allMasksData.Add(newData);
        }
    }


    #region Get Event Data
    public PlayerEventData GetEventData(int numEvent) { return eventsRandom[numEvent].data; }
    public PlayerEventData GetCurrentEventData() { return GetEventData(currentEvent); }

    #endregion

    #region Get Mask data

    private bool isExistMask(Masks currentMask) => allMasks.Any(x => x.nameMask == currentMask);

    public MaskData GetMaskData(Masks mask)
    {
        if (!isExistMask(mask))
        {
            Debug.Log($"No se encontro datos para la mascara {mask}");
            return new MaskData();
        }

        return allMasks.First(x => x.nameMask == mask);
    }

    public int GetMaskCooldown(Masks mask)
    {
        MaskData maskData = GetMaskData(mask);
        if (maskData.nameMask == Masks.None) return 0;

        return maskData.cooldown;
    }

    public Sprite GetSpriteMask(Masks mask)
    {
        MaskData maskData = GetMaskData(mask);
        if (maskData.nameMask == Masks.None) return null;

        return maskData.iconMask;
    }

    #endregion


    public void StartNextEvent()
    {
        currentEvent++;
        if (currentEvent >= eventsRandom.Length)
        {
            Debug.LogWarning("NO SE ENCONTRARON MAS EVENTOS. AÑADIR MAS");
            return;
        }

        PlayerEventData data = GetEventData(currentEvent);
        EventsManager.Instance.TriggerEvent(BasicEvents.OnStartEvent, data);
        CheckCooldown();
    }
    public void StopEvent()
    {
        EventsManager.Instance.TriggerEvent(BasicEvents.OnEndEvent);
        CheckCooldown();
    }

    #region Masks
    MaskInfo FindMaskData(Masks mask)
    {
        return allMasksData.First(m => m.data.nameMask == mask);
    }

    private void UseMask(object mask)
    {
        if (mask == null) return;
        Masks currentMask = (Masks) mask;

        if (IsAbleToUse(currentMask))
            ActivateCooldown(currentMask);
    }

    public bool IsAbleToUse(Masks mask)
    {
        MaskInfo maskInfo = FindMaskData(mask);
        if (maskInfo == null) return false;

        return maskInfo.currentCooldown == 0;
    }

    void ActivateCooldown(Masks mask)
    {
        MaskInfo maskInfo = FindMaskData(mask);
        if (maskInfo == null) return;

        maskInfo.currentCooldown = maskInfo.data.cooldown + 1;
    }

    void CheckCooldown()
    {
        foreach (MaskInfo maskInfo in allMasksData)
        {
            if (maskInfo.currentCooldown <= 0) continue;
            maskInfo.currentCooldown--;

            if (maskInfo.currentCooldown <= 0)
                maskInfo.currentCooldown = 0;
        }
    }

    #endregion
}