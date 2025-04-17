using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public KeyColour keyColour = new KeyColour(); 
    public enum KeyColour
    {
        Red,
        Green,
        Blue,
        Black,
        White
    }
}
