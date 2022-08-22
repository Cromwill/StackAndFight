using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack
{
    private LevelSystem _levelSystem;

    public JumpAttack(LevelSystem levelSystem)
    {
        _levelSystem = levelSystem;
    }

    public void Perform(Transform transform)
    {
        var colliders = Physics.OverlapSphere(transform.position, 1f);

        foreach (var collider in colliders)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Push(-enemy.transform.forward*100);
                _levelSystem.IncreaseLevel(enemy.Level);
            }
        }
    }
}
