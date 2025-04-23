using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler Instance => _instance;
    private static InputHandler _instance;

    [Header("Input Names")]
    [SerializeField] private string horizontal;
    [SerializeField] private string vertical;
    [SerializeField] private string confirm;

    [Header("Properties")]
    [SerializeField] private float minAxisValue;

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float GetHorizontalRaw()
    {
        float value = Input.GetAxisRaw(horizontal);
        if (-minAxisValue < value && value < minAxisValue)
        {
            value = 0f;
        }
        return value;
    }

    public float GetVerticalRaw()
    {
        float value = Input.GetAxisRaw(vertical);
        if (-minAxisValue < value && value < minAxisValue)
        {
            value = 0f;
        }
        return value;
    }

    public bool GetConfirmDown()
    {
        return Input.GetButtonDown(confirm);
    }
}
