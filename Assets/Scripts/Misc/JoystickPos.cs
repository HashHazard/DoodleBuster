using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JoystickPos : MonoBehaviour
{

    private RectTransform rect;
    // Start is called before the first frame update
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        
    }

    public void OnGetTouchPosition(InputValue value)
    {
        Vector2 inputVect = value.Get<Vector2>();
        rect.position = inputVect;
        Debug.Log(value.Get<Vector2>());
    }

    // Update is called once per frame
    void Update()
    {
    }
}
