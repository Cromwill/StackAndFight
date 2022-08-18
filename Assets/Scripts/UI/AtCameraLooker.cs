using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtCameraLooker : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
    }
}
