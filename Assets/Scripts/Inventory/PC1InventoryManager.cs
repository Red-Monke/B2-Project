using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC1InventoryManager : MonoBehaviour
{
    public GameObject pc1InventoryMenu;
    private bool menuActivated;
    public bool p1Active;

    public ItemSlot[] itemSlot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (p1Active)
        {
            if (Input.GetButtonDown("Inventory") && menuActivated)
            {
                Time.timeScale = 1;
                pc1InventoryMenu.SetActive(false);
                menuActivated = false;
            }
            else if (Input.GetButtonDown("Inventory") && !menuActivated)
            {
                Time.timeScale = 0;
                pc1InventoryMenu.SetActive(true);
                menuActivated = true;
            }
        }

    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false && itemSlot[i].name == name || itemSlot[i].itemQuantity == 0)
            {
                int leftOverItems = itemSlot[i].AddItem(itemName, quantity, itemSprite, itemDescription);
                Debug.Log("itemName = " + itemName + " Quantity = " + quantity + " Sprite = " + itemSprite);
                
                if(leftOverItems > 0)
                {
                    leftOverItems = AddItem(itemName, leftOverItems, itemSprite, itemDescription);

                    return leftOverItems;
                }
                
            }
        }
        return quantity; 
    }

    public void DeselectAllSlots()
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }

}
