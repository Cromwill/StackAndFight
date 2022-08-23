using UnityEngine;

public class AtCameraLooker : MonoBehaviour
{
    private void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(new Vector3(0, 180, 0));
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, 0f);
    }
}
