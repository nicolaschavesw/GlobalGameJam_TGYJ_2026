using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] float performancePoints = 0;
    [SerializeField] float burnoutPoints = 0;

    [SerializeField, Header("Basic info")]
    public int currentHour = 0;

    [SerializeField, Header("Events - ONLY VISUAL")]
    public string currentEvent = "None";
    [SerializeField] private Masks currentMask = Masks.None;

    public static GameManager Instance { get; private set; }
    public float PerformancePoints { get => performancePoints; set => performancePoints = value; }
    public float BurnoutPoints { get => burnoutPoints; set => burnoutPoints = value; }

    void Awake()
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

    private void OnEnable()
    {
        EventsManager.Instance.Subscribe(BasicEvents.OnStartEvent, StartEvent);
        EventsManager.Instance.Subscribe(BasicEvents.OnEndEvent, EndEvent);
    }
    private void OnDisable()
    {
        EventsManager.Instance.Unsubscribe(BasicEvents.OnStartEvent, StartEvent);
        EventsManager.Instance.Unsubscribe(BasicEvents.OnEndEvent, EndEvent);
    }

    public void SetCurrentEvent(string NewEvent) { currentEvent = NewEvent; }
    public void SetCurrentMask(Masks newMask) {  currentMask = newMask; }
    public Masks GetCurrentMask() => currentMask;

    void StartEvent(object newEvent)
    {
        PlayerEventData eventData = null;
        if (newEvent == null || !(PlayerEventData) newEvent)
            return;
        else eventData = (PlayerEventData) newEvent;

        SetCurrentEvent(eventData.nameEvent);
        currentHour++;
    }

    void EndEvent(object newMovement)
    {
        SetCurrentEvent(string.Empty);
    }
}
