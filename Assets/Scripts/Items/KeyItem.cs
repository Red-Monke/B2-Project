namespace DoorOperations
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class KeyItem : MonoBehaviour
    {
        public KeyColour keyColourOpen = new KeyColour();
        public KeyColour keyColourClose = new KeyColour();

        public AudioClip keySFX;

        public void KeyPickup()
        {
            AudioManager.Instance.PlaySFX(keySFX);
            Debug.Log($"Used key {gameObject.name}.");
        }

        public enum KeyColour
        {
            Red,
            Green,
            Blue,
            Black,
            White
        }
    }
}

