using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private Rigidbody[] _rigidbodies;
    public Chest Chest { get; private set; }

    private void Awake()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();

        ChangeRagdollState(true);

        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

            if(rigidbody.TryGetComponent(out CharacterJoint characterJoint))
            {
                characterJoint.massScale = 0.5f;
                characterJoint.enableProjection = true;
            }
        }
    }

    public void ActivateRagdoll()
    {
        _animator.enabled = false;
        ChangeRagdollState(false);
    }

    private void ChangeRagdollState(bool isDisabled)
    {
        foreach (var rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = isDisabled;

            if (rigidbody.TryGetComponent(out Collider collider))
                collider.isTrigger = isDisabled;

            if(rigidbody.TryGetComponent(out Chest chest))
            {
                Chest = chest;
                Chest.Init(rigidbody);
            }
        }
    }
}
