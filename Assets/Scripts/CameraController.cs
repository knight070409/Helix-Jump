using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Ball ball;
    private float offset;

    private void Awake()
    {
        offset = transform.position.y - ball.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curPos = transform.position;
        curPos.y = ball.transform.position.y + offset;
        transform.position = curPos;
    }
}
