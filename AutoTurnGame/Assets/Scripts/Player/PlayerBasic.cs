using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasic : MonoBehaviour
{
    [SerializeField] protected float _health;
    [SerializeField] protected float _attackCooldown;
    [SerializeField] protected int _speed;

    [SerializeField] protected Image _healthImage;
    [SerializeField] protected Image _behaviorImage;

    public float AttackDamage { get; private set; }

    public virtual void Attack()
    {
        AttackDamage = Random.Range(0.01f, 0.1f);
    }
}
