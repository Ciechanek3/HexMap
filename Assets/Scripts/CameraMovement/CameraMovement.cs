using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private InputReader inputReader;
    [SerializeField]
    private HexGridGenerator hexGridGenerator;
    [SerializeField]
    private float speedMultiplier;
    [SerializeField]
    private int minX;
    [SerializeField]
    private int minZ;

    private float maxX;
    private float maxZ;

    private float initialX;
    private float initialZ;

    private void Start()
    {
        initialX = transform.position.x;
        initialZ = transform.position.z;

        maxX = hexGridGenerator.GridLength;
        maxZ = hexGridGenerator.GridHeight;
    }

    private void Update()
    {
        if (inputReader.IsRightMouseButtonClicked)
        {
            if (!InsideCombinedValues()) return;
            if (!AboveMinimumValues()) return;
            if (!BelowMaximumValues()) return;

            transform.position += new Vector3(inputReader.look.x * speedMultiplier, 0, inputReader.look.y * speedMultiplier);
        }

        if(hexGridGenerator.XGridLayerSize + initialX < transform.position.x)
        {
            initialX += hexGridGenerator.XGridLayerSize;
            hexGridGenerator.GetLayer(1, 0);
        }

    }
    private bool AboveMinimumValues()
    {
        if (transform.position.x <= minX && inputReader.look.x <= 0 && transform.position.z <= minZ && inputReader.look.y <= 0)
        {
            return false;
        }

        if (transform.position.x <= minX && inputReader.look.x <= 0)
        {
            transform.position += new Vector3(0, 0, inputReader.look.y * speedMultiplier);
            return false;
        }

        if (transform.position.z <= minZ && inputReader.look.y <= 0)
        {
            transform.position += new Vector3(inputReader.look.x * speedMultiplier, 0, 0);
            return false;
        }
        return true;
    }

    private bool BelowMaximumValues()
    {
        if (transform.position.x >= maxX && inputReader.look.x >= 0 && transform.position.z >= maxZ && inputReader.look.y >= 0)
        {
            return false;
        }

        if (transform.position.x >= maxX && inputReader.look.x >= 0)
        {
            transform.position += new Vector3(0, 0, inputReader.look.y * speedMultiplier);
            return false;
        }

        if (transform.position.z >= maxZ && inputReader.look.y >= 0)
        {
            transform.position += new Vector3(inputReader.look.x * speedMultiplier, 0, 0);
            return false;
        }
        return true;
    }

    private bool InsideCombinedValues()
    {
        if (transform.position.x <= minX && inputReader.look.x <= 0 && transform.position.z >= maxZ && inputReader.look.y > 0)
        {
            return false;
        }

        if (transform.position.x >= maxX && inputReader.look.x <= 0 && transform.position.z <= minZ && inputReader.look.y < 0)
        {
            return false;
        }

        if (transform.position.z <= minZ && inputReader.look.y <= 0 && transform.position.x >= maxZ && inputReader.look.x > 0)
        {
            return false;
        }

        if (transform.position.z >= maxX && inputReader.look.y <= 0 && transform.position.x <= minZ && inputReader.look.x < 0)
        {
            return false;
        }

        return true;
    }
}

  
