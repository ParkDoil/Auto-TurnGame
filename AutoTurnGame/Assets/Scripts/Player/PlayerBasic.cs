using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasic : MonoBehaviour
{
    protected float _health;
    protected float _attackCooldown;
    protected int _speed;

    [SerializeField] protected Image _healthImage;
    [SerializeField] protected Image _behaviorImage;
    [SerializeField] protected TextMeshProUGUI _text;

    public float AttackDamage { get; private set; }

    public virtual void Attack()
    {
        AttackDamage = Random.Range(0.05f, 0.2f);
    }
}
