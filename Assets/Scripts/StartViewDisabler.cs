using UnityEngine;

public class StartViewDisabler : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.SetActive(false);
            enabled = false;
        }
    }
}
