using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
	public Vector2 look;
	public float isRightMouseButtonClicked;
	public float isLeftMouseButtonClicked;

	private MovementInputAction _movementInputAction;

    private void Awake()
    {
		_movementInputAction = new MovementInputAction();
		_movementInputAction.Enable();
		look = _movementInputAction.movement.Look.ReadValue<Vector2>();

    }

    private void Update()
    {
		isRightMouseButtonClicked = _movementInputAction.movement.RightMouseButtonClicked.ReadValue<float>();
		isLeftMouseButtonClicked = _movementInputAction.movement.RightMouseButtonClicked.ReadValue<float>();

		if (isRightMouseButtonClicked != 0)
        {
			look = _movementInputAction.movement.Look.ReadValue<Vector2>();
			Debug.Log("hi");
        }
    }
}
