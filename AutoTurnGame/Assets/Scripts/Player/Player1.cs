using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : PlayerBasic
{
    [SerializeField] private Player2 _player2;

    private float _elapsedTime;

    private WaitForSeconds _wait;

    void Start()
    {
        _health = 1f;
        _speed = Random.Range(1, 6);
        _attackCooldown = 10.0f / _speed;
        _wait = new WaitForSeconds(Time.fixedDeltaTime);

        StartCoroutine(BehaviorGague());
    }

    public override void Attack()
    {
        base.Attack();
        _player2.Hit(AttackDamage);

        StartCoroutine(BehaviorGague());
    }

    public void Hit(float damage)
    {
        _health -= damage;
    }

    private IEnumerator BehaviorGague()
    {
        _elapsedTime = 0f;
        while (_elapsedTime <= _attackCooldown)
        {
            _elapsedTime += Time.fixedDeltaTime;
            _behaviorImage.fillAmount += Time.fixedDeltaTime / _attackCooldown;
            yield return _wait;
        }
        _behaviorImage.fillAmount = 0.0f;
        Attack();
    }
}
