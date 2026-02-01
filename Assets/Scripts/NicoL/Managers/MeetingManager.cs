using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeetingManager : MonoBehaviour
{
    [Header("PlayerFrame")]
    [SerializeField] Color colorBlockOption = Color.white;
    [SerializeField] SpriteRenderer framePlayer;

    [Header("Frames characters")]
    [SerializeField] SpriteRenderer[] characters;

    [Header("All Characters")]
    [SerializeField] List<Sprite> charactersList;

    [Header("All texts")]
    [SerializeField] List<GameObject> textOptions;
    [SerializeField] private List<TextOptions> textOptionsList;

    [System.Serializable]
    public class TextOptions
    {
        public SpriteRenderer sr_baseSprite;
        public TextMeshPro tmp_text;
        public SpriteRenderer sr_frame;
        public Masks typeAnswer;
    }

    [ContextMenu("FindComponents")]
    public void FindComponents()
    {
        foreach (var item in textOptions)
        {
            TextOptions newDataText = new TextOptions()
            {
                sr_baseSprite = item.GetComponent<SpriteRenderer>(),
                sr_frame = item.GetComponentInChildren<SpriteRenderer>(),
                tmp_text = item.GetComponentInChildren<TextMeshPro>()
            };

            textOptionsList.Add(newDataText);
        }
    }

    private void OnEnable()
    {
        EventsManager.Instance.Subscribe(BasicEvents.OnStartEvent, StartEvent);
    }

    private void OnDisable()
    {
        EventsManager.Instance.Unsubscribe(BasicEvents.OnStartEvent, StartEvent);
    }

    void StartEvent(object call)
    {
        if (call == null) return;
        PlayerEventData eventData = (PlayerEventData)call;

        RandomCharacters();
        TextInData(eventData);
    }

    void RandomCharacters()
    {
        List<int> indices = new List<int>();
        int count = Mathf.Min(characters.Length, charactersList.Count);

        for (int i = 0; i < charactersList.Count; i++)
            indices.Add(i);

        for (int i = 0; i < indices.Count; i++)
        {
            int rand = Random.Range(i, indices.Count);
            (indices[i], indices[rand]) = (indices[rand], indices[i]);
        }

        for (int i = 0; i < count; i++)
        {
            characters[i].sprite = charactersList[indices[i]];
        }
    }

    void TextInData(PlayerEventData data)
    { 
        Masks currentMask = GameManager.Instance.GetCurrentMask();

        foreach (var option in data.posibleOptions)
        {
            foreach(var baseData in textOptionsList)
            {
                baseData.tmp_text.text = option.posibleOption;

                Sprite spriteIcon = PlayerEventDataManager.Instance.GetSpriteMask(option.CurretMask);
                if (spriteIcon != null)
                    baseData.sr_frame.sprite = spriteIcon;

                if (currentMask != option.CurretMask)
                    baseData.sr_baseSprite.color = colorBlockOption;
                else
                    baseData.sr_baseSprite.color = Color.white;
            }
        }
    }
}
