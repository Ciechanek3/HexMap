using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
	[HideInInspector]
	public Vector2 look;
	[HideInInspector]
	public float isRightMouseButtonClicked;
	[HideInInspector]
	public float isLeftMouseButtonClicked;

	private MovementInputAction _movementInputAction;

	public bool IsRightMouseButtonClicked => isRightMouseButtonClicked != 0;

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
		look = _movementInputAction.movement.Look.ReadValue<Vector2>();
	}
}
