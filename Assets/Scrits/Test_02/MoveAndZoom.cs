using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAndZoom : MonoBehaviour
{
    private Camera mainCam;

    Vector3 targetOriPos;

    Vector3 targetScreenPos;

    Vector3 offset;

    Rigidbody rig;

    private void Awake()
    {
        mainCam = Camera.main;
        rig = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        targetOriPos = transform.position;
        targetScreenPos = mainCam.WorldToScreenPoint(transform.position);
        offset = transform.position - mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPos.z));
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, targetScreenPos.z)) + offset;
        newPos.z = targetOriPos.z;
        transform.position = newPos;
        rig.velocity = Vector3.zero;

        float mouseScrollValue = Input.GetAxis("Mouse ScrollWheel");
        if (mouseScrollValue != 0)
        {
            transform.localScale += Vector3.one * mouseScrollValue;
        }
    }
}
