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
    
    public AudioClip doorOpenSFX;
    public float doorVolume = 1f;

    public static bool doorSoundPlayed = false;
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

        if (!doorSoundPlayed) 
        {
            //makes sure only one instance of the door sound is played if there are multiple of the same door in each scene
            AudioManager.Instance.PlaySFX(doorOpenSFX);
            doorSoundPlayed = true;
        }

        Debug.Log($"Opened {gameObject.name} with sound at volume {doorVolume}.");
    }

    private void LateUpdate()
    {
        doorSoundPlayed=false;
    }

    public void CloseDoor()
    {
        doorBox.enabled = true;
        doorMesh.enabled = true;

        Debug.Log($"Door {gameObject.name} closed.");
    }
}


