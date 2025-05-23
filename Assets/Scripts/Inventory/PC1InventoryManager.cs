using System.Collections;
using System.Collections.Generic;
using TransportingObject;
using UnityEngine;
using UnityEngine.EventSystems;

public class PC1InventoryManager : MonoBehaviour
{
    public CharacterSwitch cSwitch;
    public ItemSlot[] itemSlot;
    public int currentItemIndex;

    private void Start()
    {
        //upon scene initialization, assign the first itemslot to be the currently selected slot 
        itemSlot[0].SelectedSlot();

        for (int i = 0; i < itemSlot.Length; i++)
        {
            ItemSlot itemSlots = itemSlot[i].GetComponent<ItemSlot>();
            if(itemSlots != null)
            {
                itemSlots.GetIndex(i);
            }
        }
    }

    void Update()
    {
        if (cSwitch.p1Active)
        {
            ItemSelection();
        }    
    }

    void ItemSelection()
    {
        #region ARROW KEYS CONTROL
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentItemIndex >= itemSlot.Length -1)
            {
                //if the next slot to be selected's index would be over the amount of slots available
                //select the first slot in the array instead when key is pressed
                DeselectAllSlots();
                currentItemIndex = 0;
                itemSlot[currentItemIndex].SelectedSlot();
            }
            else
            {
                DeselectAllSlots();
                currentItemIndex++;
                itemSlot[currentItemIndex].SelectedSlot();
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentItemIndex == 0)
            {
                //if the current selected item is first in the array
                //select the last slot in the array instead when key is pressed
                DeselectAllSlots();
                currentItemIndex = itemSlot.Length -1;
                itemSlot[currentItemIndex].SelectedSlot();
            }
            else
            {
                DeselectAllSlots();
                currentItemIndex--;
                itemSlot[currentItemIndex].SelectedSlot();
            }
        }
        #endregion
    }

    public void AddItem(string itemName, Sprite itemSprite, GameObject itemObject)
    { 
        for (int i = 0; i < itemSlot.Length; i++)
        {
            if (itemSlot[i].isFull == false)
            {
                Debug.Log("itemName = " + itemName + " Sprite = " + itemSprite);
                itemSlot[i].AddItem(itemName, itemSprite, itemObject);
                return;
            }
        }
    }

    public void DeselectAllSlots()
    {
        for(int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].DeselectSlot();
        }
    }
}
