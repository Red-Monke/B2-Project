using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IDoorActions
{
    #region DOOR DATA
    public DoorColour doorColour = new DoorColour();

    
    #endregion


    private void Start()
    {

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

}

