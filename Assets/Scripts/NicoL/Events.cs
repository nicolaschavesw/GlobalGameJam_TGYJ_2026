using System;
using UnityEngine;

public class Events{}

[Serializable]
public enum BasicEvents
{
    OnStartEvent,
    OnEndEvent,

    OnChangeMaskSelection,
    OnUseMask,
}

[Serializable]
public enum GameEvents
{
    OnChangeWindow
}