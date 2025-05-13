using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public GameObject pairedGoalBox;
    GoalManager pairedGM;
    CharacterSwitch cSwitch;
    public bool thisGoalReached = false;
    public bool levelComplete = false;

    private void Start()
    {
        cSwitch = FindObjectOfType<CharacterSwitch>();
        pairedGM = pairedGoalBox.GetComponent<GoalManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (cSwitch.p1Active)
        {
            if (other.gameObject.CompareTag("Character1"))
            {
                thisGoalReached = true;
                LevelCompletionDetection();
            }
        }
        else if (!cSwitch.p1Active)
        {
            if (other.gameObject.CompareTag("Character2"))
            {
                thisGoalReached = true;
                LevelCompletionDetection();
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (cSwitch.p1Active)
        {
            if (other.gameObject.CompareTag("Character1"))
            {
                thisGoalReached = false;
            }
        }
        else if (!cSwitch.p1Active)
        {
            if (other.gameObject.CompareTag("Character2"))
            {
                thisGoalReached = false;
            }
        }

    }

    public void LevelCompletionDetection()
    {
        if(thisGoalReached && pairedGM.thisGoalReached)
        {
            levelComplete = true;
        }

        if (levelComplete) { Debug.LogWarning("level Complete, insert game progression behaviour here"); }

    }

}
