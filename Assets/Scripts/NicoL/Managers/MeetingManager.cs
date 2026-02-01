using System.Collections.Generic;
using UnityEngine;

public class MeetingManager : MonoBehaviour
{
    [Header("PlayerFrame")]
    [SerializeField] SpriteRenderer framePlayer;

    [Header("Frames characters")]
    [SerializeField] SpriteRenderer[] characters;

    [Header("All Characters")]
    [SerializeField] List<Sprite> charactersList;

    private void OnEnable()
    {
        EventsManager.Instance.Subscribe(BasicEvents.OnStartEvent, RandomCharacters);
    }

    private void OnDisable()
    {
        EventsManager.Instance.Unsubscribe(BasicEvents.OnStartEvent, RandomCharacters);
    }

    void RandomCharacters(object call)
    {
        int count = Mathf.Min(characters.Length, charactersList.Count);

        List<int> indices = new List<int>();
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
}
