using System.Collections;
using ch.sycoforge.Decal;
using UnityEngine;

public class DecalSpawner : MonoBehaviour
{
    [SerializeField] private EasyDecal _decalPrefab;
    [SerializeField] private float _delay;

    public void Spawn()
    {
        Vector3 targetPosition = transform.position + transform.forward;
        EasyDecal.Project(_decalPrefab.gameObject, targetPosition, Quaternion.Euler(0, transform.rotation.y, 0));
    }
}
