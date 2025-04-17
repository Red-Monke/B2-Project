using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoorActions
{
    #region DOOR DATA
    public DoorColour doorColour = new DoorColour();
    public OppositeColour oppositeColour = new OppositeColour();
    #endregion
    KeyItem keyItem;

    private void Start()
    {
        keyItem = FindObjectOfType<KeyItem>();
    }

    public void OpenDoor()
    {

    }

    public void CloseDoor()
    {

    }

    public enum DoorColour
    {
        Red,
        Green,
        Blue,
        Black,
        White
    }

    public enum OppositeColour
    {
        Red,
        Green,
        Blue,
        Black,
        White
    }
}

