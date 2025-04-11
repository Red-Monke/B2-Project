using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationSpeed;
    float dirX;
    float dirZ;
    Vector3 movementDirection;
    Rigidbody rb;
   
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //movement direction with button press
        dirX = Input.GetAxis("Horizontal");
        dirZ = Input.GetAxis("Vertical");
        movementDirection = new Vector3(dirX, 0, dirZ);
        gameObject.transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.Self);

        
        //rotation with RMC in same direction as mouse drag on x axis
        if (Input.GetMouseButton(0))
        {
            float yRot = Input.GetAxis("Mouse X") * rotationSpeed;
            gameObject.transform.eulerAngles = new Vector3(0, gameObject.transform.eulerAngles.y + yRot, 0);
        }
        
    }
}
