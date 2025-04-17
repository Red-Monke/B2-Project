using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public DoorToOpen doorToOpen = new DoorToOpen();
    public DoorToClose doorToClose = new DoorToClose();

    public enum DoorToOpen
    {
        Red,
        Blue,
        Green,
        Black,
        White
    };

    public enum DoorToClose
    {
        Red,
        Blue,
        Green,
        Black,
        White
    };
}
