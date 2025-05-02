using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour
{
    [SerializeField] PC1InventoryManager p1InventoryMan;
    [SerializeField] PC2InventoryManager p2InventoryMan;
    [SerializeField] GameObject playerCharacter1;
    [SerializeField] GameObject playerCharacter2;
    [SerializeField] GameObject p1ItemBar;
    [SerializeField] GameObject p2ItemBar;
    [SerializeField] GameObject p1ItemDescription;
    [SerializeField] GameObject p2ItemDescription;
    PlayerController p1Controller;
    PlayerController p2Controller;
    public bool p1Active;

    // Start is called before the first frame update
    void Start()
    {
        p1Controller = playerCharacter1.GetComponent<PlayerController>();
        p2Controller = playerCharacter2.GetComponent<PlayerController>();
        
        p1Active = true;
        
        p1Controller.enabled = true;
        p2Controller.enabled = false;
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
        p1Controller.enabled = false;
        p1InventoryMan.enabled = false;
        p1ItemBar.SetActive(false);
        p1ItemDescription.SetActive(false);

        p2Controller.enabled = true;
        p2InventoryMan.enabled = true;
        p2ItemBar.SetActive(true);

    }

    void SwitchToCharacter1()
    {
        p1Controller.enabled = true;
        p1InventoryMan.enabled = true;
        p1ItemBar.SetActive(true);


        p2Controller.enabled = false;
        p2InventoryMan.enabled = false;
        p2ItemBar.SetActive(false);
        p2ItemDescription.SetActive(false);
    }
}
