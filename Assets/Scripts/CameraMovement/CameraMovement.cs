using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private InputReader inputReader;
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private int minX;
    [SerializeField]
    private int minZ;

    private void Update()
    {
        if (inputReader.IsRightMouseButtonClicked)
        {
            if (transform.position.x <= minX && inputReader.look.x < 0 && transform.position.z <= minZ && inputReader.look.y < 0)
            {
                return;
            }

            if (transform.position.x <= minX && inputReader.look.x < 0)
            {
                transform.position += new Vector3(0, 0, inputReader.look.y * speedMultiplier);
                return;
            }

            if (transform.position.z <= minZ && inputReader.look.y < 0)
            {
                transform.position += new Vector3(inputReader.look.x * speedMultiplier, 0, 0);
                return;
            }

            transform.position += new Vector3(inputReader.look.x * speedMultiplier, 0, inputReader.look.y * speedMultiplier);
        }
    }
}
