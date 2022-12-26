using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : PlayerBasic
{
    [SerializeField] private Player1 _player1;

    private float _elapsedTime_attack;
    private float _elapsedTime_text;

    private WaitForSeconds _wait;

    private Vector3 _prevPosition;
    private Color _prevColor;

    private int _showDamage;

    private void OnEnable()
    {
        GameManager.Instance.Dead.AddListener(SomebodyDie_2);
    }

    void Start()
    {
        _health = 1f;
        _speed = Random.Range(1, 6);
        _attackCooldown = 10.0f / _speed;
        _wait = new WaitForSeconds(Time.fixedDeltaTime);
        _prevColor = _text.color;
        _prevPosition = _text.transform.position;

        StartCoroutine(BehaviorGague());
    }

    public override void Attack()
    {
        base.Attack();
        _player1.Hit(AttackDamage);

        StartCoroutine(BehaviorGague());
    }

    public void Hit(float damage)
    {
        _showDamage = (int)(damage * 100);
        _text.text = $"{_showDamage}";
        StartCoroutine(TextPosition());

        _health -= damage;
        _healthImage.fillAmount -= damage;

        if (_health <= 0f)
        {
            GameManager.Instance.SomeoneDie();
        }
    }
    private IEnumerator TextPosition()
    {
        _elapsedTime_text = 0f;
        _text.color = _prevColor;
        _text.transform.position = _prevPosition;
        while (_elapsedTime_text <= 1.0f)
        {
            _elapsedTime_text += Time.fixedDeltaTime;
            _text.transform.position = new Vector3(_text.transform.position.x, _text.transform.position.y + Time.fixedDeltaTime, _text.transform.position.z);
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - Time.fixedDeltaTime);
            yield return _wait;
        }
    }

    private IEnumerator BehaviorGague()
    {
        _elapsedTime_attack = 0f;
        while (_elapsedTime_attack <= _attackCooldown)
        {
            _elapsedTime_attack += Time.fixedDeltaTime;
            _behaviorImage.fillAmount += Time.fixedDeltaTime / _attackCooldown;
            yield return _wait;
        }
        _behaviorImage.fillAmount = 0.0f;
        Attack();
    }

    private void SomebodyDie_2()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        GameManager.Instance.Dead.RemoveListener(SomebodyDie_2);
    }
}
