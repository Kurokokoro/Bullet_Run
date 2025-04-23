using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;

    public PlayerMovement PlayerMovement => playerMovement;


    private void Awake()
    {
        GameManager.Instance.SetPlayer(this);
    }

    public void ResetPlayer(Vector3 pos)
    {
        transform.position = pos;
    }
}
