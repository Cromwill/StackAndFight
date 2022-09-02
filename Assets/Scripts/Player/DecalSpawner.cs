using System.Collections;
using ch.sycoforge.Decal;
using DG.Tweening;
using UnityEngine;

public class DecalSpawner : MonoBehaviour
{
    [SerializeField] private EasyDecal _decalPrefab;
    [SerializeField] private float _delay;
    [SerializeField] private Decal _decal;

    //public void Spawn()
    //{
    //    Vector3 targetPosition = transform.position + transform.forward * 0.8f;
    //    EasyDecal.Project(_decalPrefab.gameObject, targetPosition, new Quaternion(0, transform.rotation.y, 0, 0));
    //}

    public void Spawn(SwipeDirection direction)
    {
        Vector3 targetPosition = transform.position + transform.forward - transform.up * 0.7f;
        var decal = Instantiate(_decal, targetPosition, Quaternion.identity);

        if (direction == SwipeDirection.Forward)
            decal.transform.Rotate(new Vector3(0, 180, 0));
        if (direction == SwipeDirection.Left)
            decal.transform.Rotate(new Vector3(0, 90, 0));
        if (direction == SwipeDirection.Right)
            decal.transform.Rotate(new Vector3(0, -90, 0));

        StartCoroutine(Fade(decal));
    }

    private IEnumerator Fade(Decal decal)
    {
        yield return new WaitForSeconds(2f);

        decal.Fade();
    }
}
