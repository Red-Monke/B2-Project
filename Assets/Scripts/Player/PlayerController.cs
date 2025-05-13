using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TransportingObject;
using DoorOperations;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region OBJECT REFERENCES
    [Header("Object References")]
    public Rigidbody pb;
    public BoxCollider coll;
    public GameObject pauseUI;
    public PC1InventoryManager p1Inventory;
    public PC2InventoryManager p2Inventory;
    public CharacterSwitch cSwitch;
    public GameObject p1ItemObject;
    public GameObject p2ItemObject;
    public LayerMask groundLayer;
    #endregion

    #region MOVEMENT VARIABLES
    [Header("Script variables")]
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float jumpForce;
    float yRot;
    float dirZ;
    Vector3 movementDirection;
    public float raycastDistance;
    #endregion

    #region INTERACTION VARIABLES
    [Header("Interaction Variables")]
    public float interactDistance;
    public LayerMask interactableLayer;
    public bool itemTransfered = false;
    int p1ArrayIndex;
    int p2ArrayIndex;
    #endregion


    void Update()
    {
        PlayerMovement();
        if (Input.GetButtonDown("Interact")) { Interact(); }
        if (Input.GetButtonDown("Pause")) { Pause(); }
        p1ArrayIndex = p1Inventory.currentItemIndex;
        p2ArrayIndex = p2Inventory.currentItemIndex;
        p1ItemObject = p1Inventory.itemSlot[p1ArrayIndex].itemObject;
        p2ItemObject = p2Inventory.itemSlot[p2ArrayIndex].itemObject;
    }

    private void Pause()
    {
        Debug.Log("Pause pressed");
    }


    #region PLAYER MOVEMENT
    private void PlayerMovement()
    {
        if (Input.GetButton("Vertical") && !(Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow)))
        {
            MoveForward();
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (Input.GetButton("Horizontal") && !(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)))
        {
            RotatePlayer();
        }
    }
    public void Jump()
    {
        if (IsGrounded()) { return; }
        pb.AddForce(new Vector3(0f, jumpForce, 0f), ForceMode.Impulse);
        Debug.Log("space pressed, jump attempted");
    }
    private void RotatePlayer()
    {
        yRot = Input.GetAxis("Horizontal") * rotationSpeed;
        gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y + yRot, 0);
    }

    private void MoveForward()
    {
        dirZ = Input.GetAxis("Vertical");
        movementDirection = new Vector3(0f, 0f, dirZ);
        gameObject.transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.Self);
    }
    public bool IsGrounded()
    {
        return Physics.BoxCast(coll.bounds.center, coll.bounds.size, Vector3.down, transform.rotation, raycastDistance, groundLayer);
    }
    #endregion

    public void Interact()
    {
        List<GameObject> detectedObjects = DetectObjects();

        foreach (GameObject detectedObject in detectedObjects)
        {
            Debug.Log($"Detected object: {detectedObject.name} (Tag: {detectedObject.tag})");
            #region ITEM INTERACTION    
            // Collect items
            if (detectedObject.CompareTag("Item"))
            {
                if (cSwitch.p1Active)
                {
                    if(p1Inventory.itemSlot[p1Inventory.itemSlot.Length - 1].isFull == false)
                    {
                        InventoryItem item = detectedObject.GetComponent<InventoryItem>();
                        if (item != null)
                        {
                            item.ItemCollection();
                            Debug.Log($"Collected item: {detectedObject.name}");
                            break;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("can not collect item, inventory is full");
                    }
                }
                else if (!cSwitch.p1Active)
                {
                    if (p2Inventory.itemSlot[p2Inventory.itemSlot.Length - 1].isFull == false)
                    {
                        InventoryItem item = detectedObject.GetComponent<InventoryItem>();
                        if (item != null)
                        {
                            item.ItemCollection();
                            Debug.Log($"Collected item: {detectedObject.name}");
                            break;
                        }
                    }
                    else
                    {
                        Debug.LogWarning("can not collect item, inventory is full");
                    }
                }

            }
            #endregion
            #region PLATFORM INTERACTION
            // Place items on platforms & transfer after 2s delay
            else if (detectedObject.CompareTag("Platform"))
            {
                TransportPlatform platform = detectedObject.GetComponent<TransportPlatform>();
                if (cSwitch.p1Active)
                {
                    if (platform != null && platform.isOccupied)
                    {
                        Debug.Log($"Interacting with occupied platform: {platform.gameObject.name}");

                        platform.ReturnItem();
                        break;
                    }
                    else if (platform != null && p1ItemObject != null && !platform.isOccupied)
                    {
                        //clear item from character inventory and place item on platform
                        platform.PlaceItem(p1ItemObject);
                        p1Inventory.itemSlot[p1ArrayIndex].EmptySlot();
                        p1ItemObject = null;

                        Debug.Log($"Placed item onto platform: {detectedObject.name}");
                    }
                    else
                    {
                        Debug.LogWarning("can not place item on to platform");
                    }
                }
                else if (!cSwitch.p1Active)
                {
                    if (platform != null && platform.isOccupied)
                    {
                        Debug.Log($"Interacting with occupied platform: {platform.gameObject.name}");

                        platform.ReturnItem();
                        break;
                    }
                    else if (platform != null && p2ItemObject != null && !platform.isOccupied)
                    {
                        //clear item from character inventory and place item on platform
                        platform.PlaceItem(p2ItemObject);
                        p2Inventory.itemSlot[p2ArrayIndex].EmptySlot();
                        p2ItemObject = null;

                        Debug.Log($"Placed item onto platform: {detectedObject.name}");
                    }
                    else
                    {
                        Debug.LogWarning("can not place item on to platform");
                    }
                }
            }
            #endregion
            #region DOOR INTERACTION
            // Open doors
            else if (detectedObject.CompareTag("Door"))
            {
                Door door = detectedObject.GetComponent<Door>();
                if (cSwitch.p1Active)
                {
                    KeyItem keyItem = p1ItemObject.GetComponent<KeyItem>();

                    if (keyItem != null && keyItem.keyColourOpen == door.thisDoorColour) //Compare key and door colors
                    {
                        //Get all doors in the scene
                        Door[] allDoors = FindObjectsOfType<Door>();

                        foreach (Door otherDoor in allDoors)
                        {
                            if (keyItem.keyColourOpen == otherDoor.thisDoorColour)
                            {
                                otherDoor.OpenDoor();
                            }

                            if (keyItem.keyColourClose == otherDoor.thisDoorColour)
                            {
                                otherDoor.CloseDoor();
                            }
                        }

                        Debug.Log($"Used {keyItem.keyColourOpen} key: Opened all {keyItem.keyColourOpen} doors and closed all {keyItem.keyColourClose} doors.");
                    }
                    else
                    {
                        Debug.LogWarning($"Key '{keyItem.keyColourOpen}' does not match door '{door.thisDoorColour}'.");
                    }
                }
                else if (!cSwitch.p1Active)
                {
                    KeyItem keyItem = p2ItemObject.GetComponent<KeyItem>();

                    if (keyItem != null && keyItem.keyColourOpen == door.thisDoorColour) //Compare key and door colors
                    {
                        //Get all doors in the scene
                        Door[] allDoors = FindObjectsOfType<Door>();

                        foreach (Door otherDoor in allDoors)
                        {
                            if (keyItem.keyColourOpen == otherDoor.thisDoorColour)
                            {
                                otherDoor.OpenDoor();
                            }

                            if (keyItem.keyColourClose == otherDoor.thisDoorColour)
                            {
                                otherDoor.CloseDoor();
                            }
                        }

                        Debug.Log($"Used {keyItem.keyColourOpen} key: Opened all {keyItem.keyColourOpen} doors and closed all {keyItem.keyColourClose} doors.");
                    }
                    else
                    {
                        Debug.LogWarning($"Key '{keyItem.keyColourOpen}' does not match door '{door.thisDoorColour}'.");
                    }
                }
            }
            #endregion
        }

        Debug.Log("E pressed, interaction attempted");
    }

    #region OBJECT DETECTION
    private List<GameObject> DetectObjects()
    {
        Vector3 boxCenter = gameObject.GetComponent<Collider>().bounds.center; // Center of detection
        Vector3 boxHalfExtents = new Vector3(1f, 1f, 1f);
        Vector3 boxDirection = transform.forward;

        RaycastHit[] detectedHits = Physics.BoxCastAll(boxCenter, boxHalfExtents, boxDirection, Quaternion.identity, interactDistance, interactableLayer);

        List<GameObject> detectedObjects = new List<GameObject>();

        foreach (RaycastHit hit in detectedHits)
        {

            detectedObjects.Add(hit.collider.gameObject); // Store detected objects
        }

        return detectedObjects;
    }

    public TransportPlatform DetectNearbyPlatform()
    {
        Vector3 boxCenter = gameObject.GetComponent<Collider>().bounds.center; // Center of detection
        Vector3 boxHalfExtents = new Vector3(1f, 1f, 1f); // Box size for scanning
        Vector3 boxDirection = transform.forward; // Forward direction for BoxCast

        RaycastHit[] hits = Physics.BoxCastAll(boxCenter, boxHalfExtents, boxDirection, Quaternion.identity, interactDistance, interactableLayer);

        TransportPlatform closestPlatform = null;
        float closestDistance = float.MaxValue;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Platform"))
            {
                float distance = Vector3.Distance(transform.position, hit.collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestPlatform = hit.collider.GetComponent<TransportPlatform>();
                }
            }
        }

        return closestPlatform; // Returns nearest platform, or null if none are found
    }

    private void OnDrawGizmos()
    {
        // Define BoxCast parameters
        Vector3 boxCenter = gameObject.GetComponent<Collider>().bounds.center;
        Vector3 boxHalfExtents = new Vector3(1f, 1f, 1f); // Adjust for BoxCast size
        Vector3 boxDirection = transform.forward;
        float boxDistance = interactDistance;

        // Calculate the rotated box position (extended forward)
        Vector3 boxEndPosition = boxCenter + (boxDirection * boxDistance);

        // Apply player rotation to the Gizmo transform
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(boxEndPosition, transform.rotation, Vector3.one);
        Gizmos.matrix = rotationMatrix;

        // Draw a single wire cube that rotates with the player
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(Vector3.zero, boxHalfExtents * 2f); // Cube at forward range
    }
    #endregion
}
