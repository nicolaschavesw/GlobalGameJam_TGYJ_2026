using UnityEngine;
using UnityEngine.Events;

public class MaskSelection : MonoBehaviour
{
    [SerializeField] private GameObject[] masks;
    [Header("Events")]
    public UnityEvent OnEvent;
    
    void Start()
    {
        masks = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            masks[i] = transform.GetChild(i).gameObject;
        }
    }

    public void EjecutarEvento()
    {
        OnEvent?.Invoke();
    }
}
