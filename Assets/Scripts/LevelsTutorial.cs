using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsTutorial : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Player _))
        {

        }
    }
}
