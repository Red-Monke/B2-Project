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
        [SerializeField] private GameObject currentItem;
        public bool isOccupied = false;
        public bool recentTransfer = true;


        private void Update()
        {
            if(currentItem == null && isOccupied) { ResetPlatform(); }
        }
        public void PlaceItem(GameObject item)
        {
            if (item != null && !isOccupied)
            {
                currentItem = item;
                item.SetActive(true);
                item.transform.position = itemArea.position; // Move item onto this platform
                isOccupied = true;
                Debug.Log($"Item '{item.name}' placed on {gameObject.name}");
                if (!recentTransfer && isOccupied) { StartCoroutine(MoveItemAfterDelay()); }
                
            }
        }

        private IEnumerator MoveItemAfterDelay()
        {
            yield return new WaitForSeconds(waitTime); // Wait before moving to next platform

            if (nextPlatform != null && currentItem != null && nextPlatform != this)
            {
                nextPlatform.recentTransfer = true;
                nextPlatform.PlaceItem(currentItem); // Move item to the next platform
                Debug.Log($"Item moved from {gameObject.name} to {nextPlatform.gameObject.name}");
            }
        }
        
        public void ResetPlatform()
        {
            currentItem = null;
            isOccupied = false;
            Debug.Log($"{gameObject.name} is ready for a new item.");
        }

        public GameObject RemoveItem()
        {
            if (currentItem != null)
            {
                GameObject itemToReturn = currentItem;
                currentItem = null; 
                isOccupied = false;  
                recentTransfer = false;  
                Debug.Log($"Item '{itemToReturn.name}' removed from platform {gameObject.name}");
                return itemToReturn;
            }
            return null; 
        }

    }
}
