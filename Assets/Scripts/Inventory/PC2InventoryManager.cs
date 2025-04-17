using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC2InventoryManager : MonoBehaviour
{
    public GameObject pc2InventoryMenu;
    private bool menuActivated;
    public CharacterSwitch cSwitch;
    public ItemSlot[] itemSlot;

    private void Start()
    {

    }

    void Update()
    {
        if (!cSwitch.p1Active)
        {
            if (Input.GetButtonDown("Inventory") && menuActivated)
            {
                Time.timeScale = 1;
                pc2InventoryMenu.SetActive(false);
                menuActivated = false;
            }
            else if (Input.GetButtonDown("Inventory") && !menuActivated)
            {
                Time.timeScale = 0;
                pc2InventoryMenu.SetActive(true);
                menuActivated = true;
            }
        }
    }

    public void AddItem(string itemName, Sprite itemSprite, string itemDescription, GameObject itemObject)
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                Debug.Log("itemName = " + itemName + " Sprite = " + itemSprite);
                itemSlot[i].AddItem(itemName, itemSprite, itemDescription, itemObject);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
    }
}
