using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _level;

    public int Level => _level;

    public void Die()
    {
        Destroy(gameObject);
    }
}
