using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] GameObject pc1Obj;
    [SerializeField] GameObject pc2Obj;
     CamLerp camLerp;
    public bool isLerping = false;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pc1Obj.transform.position;
        camLerp = GetComponent<CamLerp>();
    }

    void Update()
    {
        if (isLerping)
        {
            return; // Skip updates during lerp
        }
        else
        {
            // Smoothly interpolate the camera's position
            Vector3 targetPosition = camLerp.p1Active ? pc1Obj.transform.position : pc2Obj.transform.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f); // Adjust smoothing speed
        }

    }

}
