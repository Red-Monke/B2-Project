using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NightLightPreserve : MonoBehaviour
{
    public static NightLightPreserve Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //keep this instance across all scenes

        }
        else if (Instance != this)
        {
            Destroy(gameObject); // erase any duplicates in scene before scene loads.
        }
    }
}
