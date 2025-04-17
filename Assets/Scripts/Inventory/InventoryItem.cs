using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Sprite sprite;
    [TextArea]public string itemDescription;
    public GameObject itemObject;

    private PC1InventoryManager p1InventoryManager;
    private PC2InventoryManager p2InventoryManager;
    public bool collected;

    // Start is called before the first frame update
    void Start()
    {
        p1InventoryManager = GameObject.FindGameObjectWithTag("Character1UI").GetComponent<PC1InventoryManager>();
        p2InventoryManager = GameObject.FindGameObjectWithTag("Character2UI").GetComponent<PC2InventoryManager>();
        collected = false;
        itemObject = gameObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character1" && collected == false)
        {
            collected = true;
            p1InventoryManager.AddItem(itemName, sprite, itemDescription, itemObject);
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "Character2" && collected == false)
        {
            collected = true;
            p2InventoryManager.AddItem(itemName, sprite, itemDescription, itemObject);
            gameObject.SetActive(false);
        }
    }
}
