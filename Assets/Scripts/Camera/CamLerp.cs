using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLerp : MonoBehaviour
{
    public bool p1Active;

    [SerializeField] float lerpDuration = 1f;
    [SerializeField] GameObject pc1Pos;
    [SerializeField] GameObject pc2Pos;
    CamFollow camFollow;
    public CharacterSwitch cSwitch;

    // Start is called before the first frame update
    void Start()
    {
        p1Active = true;
        transform.position = pc1Pos.transform.position;
        camFollow = GetComponent<CamFollow>();
    }

    // Update is called once per frame
    void Update()
    {
        if(cSwitch.p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Starting LerpToPC2...");
            StartCoroutine(LerpToPC2(transform.position, pc2Pos.transform.position, lerpDuration));
        }

        if(!cSwitch.p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("Starting LerpToPC1...");
            StartCoroutine(LerpToPC1(transform.position, pc1Pos.transform.position, lerpDuration));
        }
    }

    IEnumerator LerpToPC2(Vector3 startPos, Vector3 targetPos, float duration)
    {
        camFollow.enabled = false;
        camFollow.isLerping = true;
        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            Debug.Log($"Time: {time}, Lerp Factor: {time / duration}, Position: {transform.position}");
            yield return null;
        }
        transform.position = targetPos;
        camFollow.isLerping = false;
        camFollow.enabled = true;
    }

    IEnumerator LerpToPC1(Vector3 startPos, Vector3 targetPos, float duration)
    {
        camFollow.enabled = false;
        camFollow.isLerping = true;
        float time = 0;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, time / duration);
            time += Time.deltaTime;
            Debug.Log($"Time: {time}, Lerp Factor: {time / duration}, Position: {transform.position}");
            yield return null;
        }
        transform.position = targetPos;
        camFollow.isLerping = false;
        camFollow.enabled = true;
    }

}
