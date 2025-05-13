using System.Collections;
using System.Collections.Generic;
using TransportingObject;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Sprite sprite;
    [TextArea]public string itemDescription;
    private GameObject itemObject;
    private CharacterSwitch cSwitch;
    private PC1InventoryManager p1InventoryManager;
    private PC2InventoryManager p2InventoryManager;
    public bool collected;
    public TransportPlatform assignedPlatform;


    // Start is called before the first frame update
    void Start()
    {
        p1InventoryManager = GameObject.FindGameObjectWithTag("Character1UI").GetComponent<PC1InventoryManager>();
        p2InventoryManager = GameObject.FindGameObjectWithTag("Character2UI").GetComponent<PC2InventoryManager>();
        cSwitch = FindObjectOfType<CharacterSwitch>();
        collected = false;
        itemObject = gameObject;

    }

    public void ItemCollection()
    {
        if (cSwitch.p1Active)
        {
            collected = true;
            p1InventoryManager.AddItem(itemName, sprite, itemDescription, itemObject);
            gameObject.SetActive(false);
        }
        else if (!cSwitch.p1Active)
        {
            collected = true;
            p2InventoryManager.AddItem(itemName, sprite, itemDescription, itemObject);
            gameObject.SetActive(false);
        }
    }

}
