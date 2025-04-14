using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    [SerializeField] GameObject pc1Obj;     // Player Character 1
    [SerializeField] GameObject pc2Obj;     // Player Character 2
    [SerializeField] float lerpDuration = 1f; // Duration for the transition
    [SerializeField] CharacterSwitch cSwitch;
    private bool isLerping = false;         // Flag to prioritize lerp

    void Start()
    {
        // Set initial camera position to PC1's position
        transform.position = pc1Obj.transform.position;
    }

    void Update()
    {
        // Check for input to start lerp
        if (!isLerping && cSwitch.p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(LerpToPosition(pc2Obj.transform.position, lerpDuration));
            cSwitch.p1Active = false; // Switch active player
        }
        else if (!isLerping && !cSwitch.p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(LerpToPosition(pc1Obj.transform.position, lerpDuration));
            cSwitch.p1Active = true; // Switch active player
        }

        // Regular camera follow (only if not lerping)
        if (!isLerping)
        {   
            //If cSwitch.p1Active is true, set targetPosition to pc1Obj.transform.position (pc2Obj.transform.position if false)
            Vector3 targetPosition = cSwitch.p1Active ? pc1Obj.transform.position : pc2Obj.transform.position; 
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f); // Smooth catch-up
        }
    }

    IEnumerator LerpToPosition(Vector3 targetPos, float duration)
    {
        isLerping = true; // Stop regular follow logic
        Vector3 startPos = transform.position;
        float time = 0;

        while (time < duration)
        {
            float t = Mathf.Clamp01(time / duration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            time += Time.deltaTime;
            yield return null; // Wait for next frame
        }

        transform.position = targetPos; // Ensure accurate final position
        isLerping = false;              // Resume regular follow logic

        // Update the active player state after lerping
        cSwitch.p1Active = (targetPos == pc1Obj.transform.position);
    }
}
