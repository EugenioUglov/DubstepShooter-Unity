using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int _health = 1;
    [SerializeField] protected GameObject _deathEffect;

    public abstract void TakeDamage(int damageValue);
}