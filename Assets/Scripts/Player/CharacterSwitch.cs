using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] PC1InventoryManager p1InventoryMan;
    [SerializeField] PC2InventoryManager p2InventoryMan;
    [SerializeField] GameObject playerCharacter1;
    [SerializeField] GameObject playerCharacter2;

    [SerializeField] CamLerp camLerp;
    [SerializeField] GameObject camFollow;
    PlayerMove p1Move;
    PlayerMove p2Move;
    

    // Start is called before the first frame update
    void Start()
    {
        p1Move = playerCharacter1.GetComponent<PlayerMove>();  
        p2Move = playerCharacter2.GetComponent<PlayerMove>();
        camLerp = camFollow.GetComponent<CamLerp>();

        p1Move.enabled = true;
        p2Move.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(camLerp.p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            camLerp.p1Active = false;

            p1Move.enabled = false;
            p1InventoryMan.enabled = false;
            p1InventoryMan.p1Active = false;
            camLerp.p1Active = false;

            p2Move.enabled = true;
            p2InventoryMan.enabled = true;
            p2InventoryMan.p2Active = true;
        }
        else if(!camLerp.p1Active && Input.GetKeyDown(KeyCode.Q))
        { 
            camLerp.p1Active = true;
            
            p1Move.enabled = true;
            p1InventoryMan.enabled = true;
            p1InventoryMan.p1Active = true;
            camLerp.p1Active = true;

            p2Move.enabled = false;
            p2InventoryMan.enabled = false;
            p2InventoryMan.p2Active = false;
        }
    }

}
