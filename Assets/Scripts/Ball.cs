using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private float impulseForce = 5;
    private bool NextCollisionDetect;
    private Vector3 startPosition;

    private void Awake()
    {
        startPosition = transform.position;   
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (NextCollisionDetect) return;

        PlayerDead deadArea = collision.transform.GetComponent<PlayerDead>();
        if (deadArea) { deadArea.HitDeadArea(); }

        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);
        NextCollisionDetect = true;

        Invoke("AllowCollision", 0.2f);
    }

    private void AllowCollision()
    {
        NextCollisionDetect = false;
    }

    public void ResetBall()
    {
        transform.position = startPosition;
    }

}
