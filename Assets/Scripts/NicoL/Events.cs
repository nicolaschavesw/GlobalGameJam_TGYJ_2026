using System;
using UnityEngine;

public class Events{}

[Serializable]
public enum BasicEvents
{
    OnStartEvent,
    OnEndEvent,
    OnUseMask
}

[Serializable]
public enum GameEvents
{
    OnChangeWindow
}