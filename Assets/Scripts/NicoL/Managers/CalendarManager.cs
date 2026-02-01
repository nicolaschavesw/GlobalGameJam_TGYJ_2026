using UnityEngine;

public class CalendarManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventsManager.Instance.Subscribe(BasicEvents.OnEndEvent, GetData);
    }
    private void OnDisable()
    {
        EventsManager.Instance.Unsubscribe(BasicEvents.OnEndEvent, GetData);


        EventsManager.Instance.TriggerEvent(BasicEvents.OnEndEvent, 50);
    }


    void GetData(object data)
    {
        if (data == null) return;
        PlayerEventData newData = (PlayerEventData)data;

        string name = newData.nameEvent;




        Masks mask = GameManager.Instance.GetCurrentMask();
        PlayerEventDataManager.Instance.GetMaskData(mask);
    }
}
