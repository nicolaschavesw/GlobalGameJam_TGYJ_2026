using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_NewPlayerEvent", menuName = "Scriptable Objects/NewPlayerEvent")]
public class PlayerEventData : ScriptableObject
{
    [Header("Name")]
    public string nameEvent;

    [Header("Optional")]
    //public bool randomEvent = true;
    public int numEvent = 0; // Si es diferente que 0 se va a posicionar como esa posicion entre los eventos

    [Header("Options")]
    public List<EventOptions> posibleOptions;
}

[Serializable]
public class EventOptions
{
    [Header("Type Mask")]
    [SerializeField] public Masks CurretMask;

    [Space, Header("Text")]
    [SerializeField] public string posibleOption;
    [Space]
    [SerializeField] public string consequence;
    [Space]
    [SerializeField] int performancePoints = 0;
    [SerializeField] int burnoutPoints = 0;
}