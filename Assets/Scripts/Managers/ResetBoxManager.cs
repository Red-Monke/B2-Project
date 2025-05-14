using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoxManager : MonoBehaviour
{
    [SerializeField] GameObject resetScriptHolder;
    PauseAndEndGameUI resetControl;
    private void Start()
    {
      resetControl = resetScriptHolder.GetComponent<PauseAndEndGameUI>();  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Character1")||other.gameObject.CompareTag("Character2")|| other.gameObject.CompareTag("Item"))
        {
            ResetIfTouched();
        }
    }

    public void ResetIfTouched()
    {
        resetControl.RestartLevel();
    }
}
