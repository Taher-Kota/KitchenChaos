using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
