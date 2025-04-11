using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadMovement : MonoBehaviour
{
    Rigidbody rb;
    public float moveSpeed;
    CapsuleCollider capsuleCollider;
    [SerializeField] GameObject[] wayPoints;
    int currentWaypointIndex = 0;
    public int damage;
    PlayerLife pLife;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        pLife = FindObjectOfType<PlayerLife>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[currentWaypointIndex].transform.position, Time.deltaTime * moveSpeed);

        if (Vector3.Distance(wayPoints[currentWaypointIndex].transform.position, gameObject.transform.position) < 0.1f)
        {
            currentWaypointIndex++;

            if(currentWaypointIndex >= wayPoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DealDamage();
        }
    }

    void DealDamage()
    {
        pLife.TakeDamage(damage, pLife.currentHealth);
    }

}
