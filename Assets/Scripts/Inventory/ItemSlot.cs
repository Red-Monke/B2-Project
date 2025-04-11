using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    #region ITEM DATA
    public string itemName;
    public int itemQuantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;

    [SerializeField] int maxNumberOfItems;
    #endregion

    #region ITEM SLOT
    [SerializeField] private TMP_Text quantityText;
    [SerializeField] private Image itemImage;
    #endregion

    #region ITEM SELECTION
    public GameObject selectedShader;
    public bool thisItemSelected;
    #endregion

    #region ITEM DESCRIPTION SLOT
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;
    #endregion

    private PC1InventoryManager p1inventoryManager;
    private PC2InventoryManager p2inventoryManager;

    private void Start()
    {
        p1inventoryManager = GameObject.FindGameObjectWithTag("Character1UI").GetComponent<PC1InventoryManager>();
        p2inventoryManager = GameObject.FindGameObjectWithTag("Character2UI").GetComponent<PC2InventoryManager>();
        itemSprite = emptySprite;
    }

    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription)
    {
        //check to see if slot is full
        if (isFull)
        {
            return quantity;
        }
        //update name
        this.itemName = itemName;
        
        //update image
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;

        //update description
        this.itemDescription = itemDescription;

        //update quantity
        this.itemQuantity += quantity;
        if(this.itemQuantity >= maxNumberOfItems)
        {
            quantityText.text = quantity.ToString();
            quantityText.enabled = true;
            isFull = true;
        
            //return leftover items
            int extraItems = this.itemQuantity - maxNumberOfItems;
            this.itemQuantity = maxNumberOfItems;
            return extraItems;
        }

        //update quatity text
        quantityText.text = this.itemQuantity.ToString();
        quantityText.enabled = true;

        return 0;

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
        itemDescriptionImage.sprite = itemSprite;
        if(itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = emptySprite;  
        }
    }

    public void OnRightClick()
    {

    }
}