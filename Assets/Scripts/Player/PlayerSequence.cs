using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSequence : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PlayerSequenceNode[] _playerSequenceNodes;


    public void Init()
    {
        StartCoroutine(PlayingSequence());
    }

    private IEnumerator PlayingSequence()
    {

        for (int i = 0; i < _playerSequenceNodes.Length; i++)
        {

            _player.Mover.Move(_playerSequenceNodes[i].SwipeDirection);

            while(_player.Mover.IsMoving)
            {
                yield return null;
            }

            yield return new WaitForSeconds(_playerSequenceNodes[i].DelayAfter);
        }
    }
}

[System.Serializable]
public class PlayerSequenceNode
{
    public SwipeDirection SwipeDirection;
    public float DelayAfter;
}
