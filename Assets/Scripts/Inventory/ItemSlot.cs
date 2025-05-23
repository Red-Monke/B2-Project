using TMPro;
using TransportingObject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    #region ITEM DATA
    [Header("Item Data")]
    public Sprite itemSprite;
    public GameObject itemObject;
    #endregion

    #region ITEM SLOT
    [Header("Item Slot")]
    [SerializeField] private Image itemImage;
    public Sprite emptySprite;
    public bool isFull;
    public int thisIndex;
    #endregion

    #region ITEM SELECTION
    [Header("Item Selection")]
    public GameObject selectedShader;
    public bool thisItemSelected;
    #endregion

    #region ITEM TRANSPORTATION
    public GameObject playerObj;
    public GameObject p1Obj;
    public GameObject p2Obj;
   // public GameObject cSwitchObj;
    public CharacterSwitch cSwitch;
    [SerializeField] float respawnDistance;
    public float raycastDistance;
    #endregion

    private PC1InventoryManager p1inventoryManager;
    private PC2InventoryManager p2inventoryManager;

    public void GetIndex(int index) { thisIndex = index; }

    private void Start()
    {
        p1inventoryManager = GameObject.FindGameObjectWithTag("Character1UI").GetComponent<PC1InventoryManager>();
        p2inventoryManager = GameObject.FindGameObjectWithTag("Character2UI").GetComponent<PC2InventoryManager>();
       // cSwitch = cSwitchObj.GetComponent<CharacterSwitch>();
        itemSprite = emptySprite;        
    }

    public void AddItem(string itemName, Sprite itemSprite, GameObject itemObject)
    {
        this.itemSprite = itemSprite;
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
        if(cSwitch.p1Active && p1inventoryManager.currentItemIndex != thisIndex) { p1inventoryManager.DeselectAllSlots(); SelectedSlot(); p1inventoryManager.currentItemIndex = thisIndex; }
        if(!cSwitch.p1Active && p2inventoryManager.currentItemIndex != thisIndex) { p2inventoryManager.DeselectAllSlots(); SelectedSlot(); p2inventoryManager.currentItemIndex = thisIndex; }

        if(cSwitch.p1Active && p1inventoryManager.currentItemIndex == thisIndex) { p1inventoryManager.DeselectAllSlots(); SelectedSlot(); p1inventoryManager.currentItemIndex = thisIndex; }
        if(!cSwitch.p1Active && p2inventoryManager.currentItemIndex == thisIndex) { p2inventoryManager.DeselectAllSlots(); SelectedSlot(); p2inventoryManager.currentItemIndex = thisIndex; }

    }

    public void OnRightClick()
    {
        PlayerController p1Control = GameObject.FindGameObjectWithTag("Character1").GetComponent<PlayerController>();
        PlayerController p2Control = GameObject.FindGameObjectWithTag("Character2").GetComponent<PlayerController>();

        TransportPlatform platform1 = p1Control.DetectNearbyPlatform();
        TransportPlatform platform2 = p2Control.DetectNearbyPlatform();
        Door door1 = p1Control.DetectNearbyDoor();
        Door door2 = p2Control.DetectNearbyDoor();

        #region CHARACTER 1 RMC ACTIONS
        if (cSwitch.p1Active)
        {
            if (door1 != null)
            {
                //as Character 1, if near a door, run the interact method in the playercontroller script
                //after assigning the item to be used as the item right clicked
                p1Control.p1ItemObject = itemObject;
                p1Control.Interact();
                return;
            }
            else if (platform1 != null)
            {
                // Place item onto platform if nearby
                platform1.PlaceItem(itemObject);
                Debug.Log($"Placed '{itemObject.name}' onto platform: {platform1.gameObject.name}");
            }
            else
            {
                // Drop item in front of player
                Vector3 dropPosition = playerObj.transform.position + playerObj.transform.forward * respawnDistance;
                itemObject.transform.position = dropPosition;
                itemObject.SetActive(true);
                Debug.Log($"Dropped '{itemObject.name}' in front of player.");
            }
        }
        #endregion

        #region CHARACTER 2 RMC ACTIONS
        if (!cSwitch.p1Active)
        {
            if (door2 != null)
            {
                //as Character 2, if near a door, run the interact method in the playercontroller script
                //after assigning the item to be used as the item right clicked
                p2Control.p2ItemObject = itemObject;
                p2Control.Interact();
                return;
            }
            else if (platform2 != null)
            {
                // Place item onto platform if nearby
                platform2.PlaceItem(itemObject);
                Debug.Log($"Placed '{itemObject.name}' onto platform: {platform2.gameObject.name}");
            }
            else
            {
                // Drop item in front of player
                Vector3 dropPosition = playerObj.transform.position + playerObj.transform.forward * respawnDistance;
                itemObject.transform.position = dropPosition;
                itemObject.SetActive(true);
                Debug.Log($"Dropped '{itemObject.name}' in front of player.");
            }
        }
        #endregion
        EmptySlot();
    }

    public void Update()
    {
        ActiveCharacterDetermination();
    }

    void ActiveCharacterDetermination()
    {
        if (cSwitch.p1Active) { playerObj = p1Obj; } else { playerObj = p2Obj; }
    }

    #region SLOT SELECTION/DESELECTION
    public void SelectedSlot()
    {
        thisItemSelected = true;
        selectedShader.SetActive(true);
    }

    public void DeselectSlot()
    {
        thisItemSelected = false;
        selectedShader.SetActive(false);
    }
    #endregion
    public void EmptySlot()
    {
        //resets cached data in item slot object as well as item description objects
        //resets boolean to allow for another item to use that slot 
        itemImage.sprite = emptySprite;
        itemObject = null;
        isFull = false;
    }

}