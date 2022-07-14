using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotate : MonoBehaviour
{
    public float rotateSpeed = 10;

    private void OnMouseDrag()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        transform.Rotate((Vector3.up * -x + Vector3.right * y) * rotateSpeed * 100 * Time.deltaTime, Space.World);
    }
}
