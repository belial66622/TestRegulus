using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestRegulus.Control;
using UnityEngine.InputSystem;
using JetBrains.Annotations;

public class InputControl : MonoBehaviour
{
    ControlInput control;
    
    Camera cam;

    RaycastHit2D hit;

    private void Awake()
    {
        control = new ControlInput();
        control.Game.Enable();
    }

    private void Start()
    {
        cam = Camera.main;
    }

    public void OnChoose(InputValue value)
    {
        hit = (Physics2D.Raycast(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, float.MaxValue));
        if (hit.collider != null)
        {
            //Debug.Log("choose");
            if (hit.collider.TryGetComponent<ITile>(out var tile))
            {
                tile.OnChoose();
            }
        }
    }

    public void OnRelease(InputValue value)
    {
        EventContainer.OnReleaseTile?.Invoke();
    }

    public void OnChangeTile(InputValue value)
    {
        hit = (Physics2D.Raycast(cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()), Vector2.zero, float.MaxValue));
        if (hit.collider != null)
        {
            //Debug.Log("change");
            if (hit.collider.TryGetComponent<ITile>(out var tile))
            {
                tile.OnChangeTile();
            }
        }
    }

    public void OnHighLight(InputValue value)
    { 
        EventContainer.OnHighlightClicked?.Invoke();
    }
}

