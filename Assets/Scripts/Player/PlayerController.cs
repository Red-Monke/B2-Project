using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region OBJECT REFERENCES
    [Header("Object References")]
    public Rigidbody pb;
    public BoxCollider coll;
    public GameObject pauseUI;
    public LayerMask groundLayer;
    public LayerMask itemLayer;
    public LayerMask doorLayer;
    public LayerMask transporterLayer;
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

    void Start()
    {
        
    }

    void Update()
    {
        PlayerMovement();
        if (Input.GetButtonDown("Interact")) { Interact(); }
        if (Input.GetMouseButtonDown(0)) { Fire(); }
        if (Input.GetButtonDown("Pause")) { Pause(); }
    }

    private void Pause()
    {
        Debug.Log("Pause pressed");
    }


    #region PLAYER MOVEMENT
    private void PlayerMovement()
    {
        if (Input.GetButton("Vertical"))
        {
            MoveForward();
        }

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }

        if (Input.GetButton("Horizontal"))
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

    private void Fire()
    {
        Debug.Log("LMC clicked, fire attempted");
    }

    public void Interact()
    {
        Debug.Log("E pressed, interaction attempted");
    }
}
