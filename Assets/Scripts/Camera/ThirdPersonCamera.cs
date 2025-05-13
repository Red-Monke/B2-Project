using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform orbitPoint;
    [SerializeField] float orbitSpeed;
    [SerializeField] float orbitDistance;
    private float rotY;
    private float rotX;
    private Vector3 currentRot;
    private Vector3 smoothVelocity = Vector3.zero;
    [SerializeField] private float smoothTime;
    private bool isRotating = false;


    // Start is called before the first frame update
    void Start()
    {
        //initialize orbit position
        transform.position = orbitPoint.position - (transform.forward * orbitDistance);
    }


    // Update is called once per frame
    void Update()
    {
        //maintin and update orbit position outside of RMC
        transform.position = orbitPoint.position - (transform.forward * orbitDistance);

        CameraOrbit();
    }
    void CameraOrbit()
    {
        //rotates camera orbit when RMC is clicked and held
        if (Input.GetMouseButton(1))
        {

            isRotating = true; // Set flag to true when button is pressed

            float mouseX = Input.GetAxis("Mouse X") * orbitSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * orbitSpeed;

            rotY += mouseX;
            rotX += mouseY;

            // Apply clamping for rotation around X axis, vertically in game
            rotX = Mathf.Clamp(rotX, 0, 80);
        }
        else
        {
            isRotating = false; // Set flag to false when button is released
        }

        // Continue updating rotation 
        Vector3 nextRot = new Vector3(rotX, rotY);
        currentRot = Vector3.SmoothDamp(currentRot, nextRot, ref smoothVelocity, smoothTime);

        // Apply the smoothed rotation
        transform.localEulerAngles = currentRot;

        // Continue applying orbit distance
        transform.position = orbitPoint.position - (transform.forward * orbitDistance);

        // Reduce the velocity over time after releasing the button
        if (!isRotating)
        {
            smoothVelocity = Vector3.Lerp(smoothVelocity, Vector3.zero, Time.deltaTime / smoothTime);
        }
        
        
    }

}
