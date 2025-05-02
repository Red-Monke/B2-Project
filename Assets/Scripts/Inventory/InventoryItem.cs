using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public Sprite sprite;
    [TextArea]public string itemDescription;
    private GameObject itemObject;
    private CharacterSwitch cSwitch;
    private PC1InventoryManager p1InventoryManager;
    private PC2InventoryManager p2InventoryManager;
    public bool collected;

    PlayerController p1Control;
    PlayerController p2Control;
    float pickupDist;
    bool canPickUp = false;

    // Start is called before the first frame update
    void Start()
    {
        p1InventoryManager = GameObject.FindGameObjectWithTag("Character1UI").GetComponent<PC1InventoryManager>();
        p2InventoryManager = GameObject.FindGameObjectWithTag("Character2UI").GetComponent<PC2InventoryManager>();
        p1Control = GameObject.FindGameObjectWithTag("Character1").GetComponent<PlayerController>();
        p2Control = GameObject.FindGameObjectWithTag("Character2").GetComponent<PlayerController>();
        cSwitch = FindObjectOfType<CharacterSwitch>();
        collected = false;
        itemObject = gameObject;
        pickupDist = p1Control.interactDistance;
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

    private void Update()
    {
        DetectPlayerProximity();
    }

    private void DetectPlayerProximity()
    {
        GameObject character1 = GameObject.FindGameObjectWithTag("Character1");
        GameObject character2 = GameObject.FindGameObjectWithTag("Character2");

        if(character1 != null && cSwitch.p1Active)
        {
            float distance = Vector3.Distance(transform.position, character1.transform.position);
            canPickUp = distance <= pickupDist;
        }

        if(character2 != null && !cSwitch.p1Active)
        {
            float distance = Vector3.Distance(transform.position, character2.transform.position);
            canPickUp = distance <= pickupDist;
        }
    }

    void OnLeftClick()
    {
        ItemCollection();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && canPickUp)
        {
            OnLeftClick();
        }
    }
}
