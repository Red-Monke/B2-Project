using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    #region ITEM DATA
    [Header("Item Data")]
    public string itemName;
    public Sprite itemSprite;
    public string itemDescription;
    public GameObject itemObject;
    #endregion

    #region ITEM SLOT
    [Header("Item Slot")]
    [SerializeField] private Image itemImage;
    public Sprite emptySprite;
    public bool isFull;
    #endregion

    #region ITEM SELECTION
    [Header("Item Selection")]
    public GameObject selectedShader;
    public bool thisItemSelected;
    #endregion

    #region ITEM DESCRIPTION SLOT
    [Header("Item Description")]
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;
    #endregion

    public Transform playerTransform;
    [SerializeField] float distance;
    private PC1InventoryManager p1inventoryManager;
    private PC2InventoryManager p2inventoryManager;


    private void Start()
    {
        p1inventoryManager = GameObject.FindGameObjectWithTag("Character1UI").GetComponent<PC1InventoryManager>();
        p2inventoryManager = GameObject.FindGameObjectWithTag("Character2UI").GetComponent<PC2InventoryManager>();
        itemSprite = emptySprite;
    }

    public void AddItem(string itemName, Sprite itemSprite, string itemDescription, GameObject itemObject)
    {
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        this.itemDescription = itemDescription;
        this.itemObject = itemObject;

        Debug.Log("slot update: item sprite = " + itemSprite);

        isFull = true;

        itemImage.sprite = itemSprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        p1inventoryManager.DeselectAllSlots();
        p2inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);

        thisItemSelected = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = itemImage.sprite;

        Debug.Log("item description updated: item sprite = " + itemSprite);
        if(itemDescriptionImage == null)
        {
            itemDescriptionImage.sprite = emptySprite;
        }
    }

    public void EmptySlot()
    {
        //resets cached data in item slot object as well as item description objects
        //resets boolean to allow for another item to use that slot 
        itemImage.sprite = emptySprite;
        itemObject = null;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemName = "";
        itemDescription = "";
        itemDescriptionImage.sprite = emptySprite;

        isFull = false;
    }

    void RespawnItem()
    {
        if (itemObject != null)
        {
            Vector3 spawnPos = playerTransform.position + playerTransform.forward * distance;
            itemObject.GetComponent<InventoryItem>().collected = false;
            itemObject.transform.position = spawnPos;
            itemObject.SetActive(true);
        }
    }

    public void OnRightClick()
    {
        RespawnItem();
        EmptySlot();
    }

}