using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] PC1InventoryManager p1InventoryMan;
    [SerializeField] PC2InventoryManager p2InventoryMan;
    [SerializeField] GameObject playerCharacter1;
    [SerializeField] GameObject playerCharacter2;
    PlayerMove p1Move;
    PlayerMove p2Move;
    public bool p1Active;

    // Start is called before the first frame update
    void Start()
    {
        p1Move = playerCharacter1.GetComponent<PlayerMove>();  
        p2Move = playerCharacter2.GetComponent<PlayerMove>();
        
        p1Active = true;
        
        p1Move.enabled = true;
        p2Move.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToCharacter2();
            Debug.Log("switching to PC2");
        }
        
        if(!p1Active && Input.GetKeyDown(KeyCode.Q))
        {
            SwitchToCharacter1();
            Debug.Log("switching to PC1");
        }
    }

    void SwitchToCharacter2()
    {
        p1Move.enabled = false;
        p1InventoryMan.enabled = false;

        p2Move.enabled = true;
        p2InventoryMan.enabled = true;
    }

    void SwitchToCharacter1()
    {
        p1Move.enabled = true;
        p1InventoryMan.enabled = true;

        p2Move.enabled = false;
        p2InventoryMan.enabled = false;
    }
}
