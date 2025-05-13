using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoorOperations;

public class Door : MonoBehaviour, IDoorActions
{
    #region DOOR DATA
    public KeyItem.KeyColour thisDoorColour; 
    BoxCollider doorBox;
    MeshRenderer doorMesh;
    #endregion

    void Start()
    {
        doorBox = gameObject.GetComponent<BoxCollider>();
        doorMesh = gameObject.GetComponent<MeshRenderer>();
    }
    public void OpenDoor()
    {
        doorBox.enabled = false;
        doorMesh.enabled = false;
        Debug.Log($"Door {gameObject.name} opened.");
    }

    public void CloseDoor()
    {
        doorBox.enabled = true;
        doorMesh.enabled = true;
        Debug.Log($"Door {gameObject.name} closed.");
    }
}


