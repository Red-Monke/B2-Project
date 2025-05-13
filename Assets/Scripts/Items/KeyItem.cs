namespace DoorOperations
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class KeyItem : MonoBehaviour
    {
        public KeyColour keyColourOpen = new KeyColour();
        public KeyColour keyColourClose = new KeyColour();
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

