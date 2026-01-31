using UnityEngine;

[CreateAssetMenu(fileName = "MaskData", menuName = "Scriptable Objects/MaskData")]
public class MaskData : ScriptableObject
{
    [Header("Data")]
    public Masks nameMask;
    public Sprite iconMask;

    public int cooldown = 0;
}