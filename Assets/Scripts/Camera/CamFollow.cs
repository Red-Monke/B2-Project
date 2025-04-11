using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] GameObject pc1Obj;
    [SerializeField] GameObject pc2Obj;
     CamLerp camLerp;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = pc1Obj.transform.position;
        camLerp = GetComponent<CamLerp>();
    }

    // Update is called once per frame
    void Update()
    {
        if (camLerp.p1Active)
        {
            transform.position = pc1Obj.transform.position;
        }
        else if (!camLerp.p1Active)
        {
            transform.position = pc2Obj.transform.position;
        }
    }
}
