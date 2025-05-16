using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDead : MonoBehaviour
{
    private void OnEnable()
    {
        GetComponent<Renderer>().material.color = Color.cyan;
    }

    public void HitDeadArea()
    {
        FindObjectOfType<UIManager>().ShowGameOver();
    }
}
