namespace TransportingObject
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class TransportPlatform : MonoBehaviour
    {
        public Transform itemArea;
        public TransportPlatform nextPlatform;
        public float waitTime;
        public GameObject currentItem;
        public bool isOccupied = false;
        public bool waitingForPlayer = false;

        public void PlaceItem(GameObject item)
        {
            if (item != null && !isOccupied && !waitingForPlayer)
            {
                currentItem = item;
                item.SetActive(true);
                item.transform.position = itemArea.position; // Move item onto this platform
                
                isOccupied = true;

                InventoryItem itemScript = item.GetComponent<InventoryItem>();
                if(itemScript != null) { itemScript.assignedPlatform = this; Debug.LogWarning("assigned platform = " + this.name); }

                Debug.Log($"Item '{item.name}' placed on {gameObject.name}");
                
                if (!waitingForPlayer && isOccupied) { StartCoroutine(MoveItemAfterDelay()); } //if occupied and not waiting for player to pick up item, transfer item to next platform
            }

            if (waitingForPlayer && !isOccupied)
            {
                currentItem = item;
                item.SetActive(true);
                item.transform.position = itemArea.position; // Move item onto this platform
                isOccupied = true;
            }

        }

        private IEnumerator MoveItemAfterDelay()
        {
            yield return new WaitForSeconds(waitTime); // Wait before moving to next platform
           
            if (nextPlatform.isOccupied)
            {
                while (nextPlatform.isOccupied)
                {
                    Debug.Log($"Waiting for {nextPlatform.gameObject.name} to become available...");
                    yield return new WaitForSeconds(waitTime);
                }
            }
            
            if (nextPlatform != null && currentItem != null && nextPlatform != this && !nextPlatform.isOccupied)
            {
                nextPlatform.waitingForPlayer = true;
                nextPlatform.PlaceItem(currentItem); // Move item to the next platform
                ResetPlatform(); //reset platform to default state after item transfer
                Debug.Log($"Item moved from {gameObject.name} to {nextPlatform.gameObject.name}");
            }
            else
            {
                Debug.LogWarning($"Transfer failed: Next platform is null or item is missing.");
            }
        }
        
        public void ResetPlatform()
        {
            currentItem = null;
            isOccupied = false;
            waitingForPlayer = false;
            Debug.Log($"{gameObject.name} is ready for a new item.");
        }

        public void ReturnItem()
        {
            if(currentItem != null)
            {
                InventoryItem itemScript = currentItem.GetComponent<InventoryItem>();

                if(itemScript != null)
                {
                    itemScript.ItemCollection();
                    Debug.Log($"Returning item '{currentItem.name}' to player inventory.");
                }
                currentItem = null;
                isOccupied = false;
                waitingForPlayer = false;

                ResetPlatform();
            }
            else
            {
                Debug.LogWarning($"{gameObject.name} has no item to return");
            }
        }

    }
}
