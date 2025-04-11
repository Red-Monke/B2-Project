using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamLerp : MonoBehaviour
{
    public bool p1Active;

    [SerializeField] float lerpDuration = 1f;
    [SerializeField] float lerpTime;
    [SerializeField] GameObject pc1Pos;
    [SerializeField] GameObject pc2Pos;
    CamFollow camFollow;

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
        
        if(p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(LerpToPC2(pc2Pos.transform.position, lerpDuration));
        }

        if(!p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(LerpToPC1(pc1Pos.transform.position, lerpDuration));
        }
        
    }

    IEnumerator LerpToPC2(Vector3 targetPos, float duration)
    {
        camFollow.enabled = false;
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        camFollow.enabled = true;
    }

    IEnumerator LerpToPC1(Vector3 targetPos, float duration)
    {
        camFollow.enabled = false;
        float time = 0;
        Vector3 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPos, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPos;
        camFollow.enabled = true;
    }

}
