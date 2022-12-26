using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * ���� ���� : Player1, Player2 ��ũ��Ʈ�� �������� ������ �����մϴ�. �׷��� �ּ��� Player1���� �޾� �ΰڽ��ϴ�.
 * �ڵ� �����丵�� ������ ���������ϴ� ��Ȳ�̶� ���� ���� ��ư� ���Ŀ� �� �����丵 �ϰڽ��ϴ�.
 */

public class Player1 : PlayerBasic
{
    [SerializeField] private Player2 _player2;

    private float _elapsedTime_attack;
    private float _elapsedTime_text;

    private WaitForSeconds _wait;

    private Vector3 _prevPosition;
    private Color _prevColor;

    private int _showDamage;

    private void OnEnable()
    {
        GameManager.Instance.Dead.AddListener(SomebodyDie_1);
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

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    public override void Attack()
    {
        base.Attack();
        _player2.Hit(AttackDamage);

        StartCoroutine(BehaviorGague());
    }

    /// <summary>
    /// �ǰ� ���� ��� �Լ�
    /// </summary>
    /// <param name="damage">�ٸ� �÷��̾ �ִ� ������</param>
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

    /// <summary>
    /// �ǰ� ������ �ؽ��� ȿ�� ���� �ڷ�ƾ
    /// </summary>
    private IEnumerator TextPosition()
    {
        _elapsedTime_text = 0f;
        _text.color = _prevColor;
        _text.transform.position = _prevPosition;
        while(_elapsedTime_text <= 1.0f)
        {
            _elapsedTime_text += Time.fixedDeltaTime;
            _text.transform.position = new Vector3(_text.transform.position.x, _text.transform.position.y + Time.fixedDeltaTime, _text.transform.position.z);
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, _text.color.a - Time.fixedDeltaTime);
            yield return _wait;
        }
    }

    /// <summary>
    /// �ൿ ������ �ڷ�ƾ
    /// </summary>
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

    /// <summary>
    /// ������ �׾��� �� �ڷ�ƾ ���ߴ� �Լ�
    /// </summary>
    private void SomebodyDie_1()
    {
        //TODO: ������ �׾����� �� ��Ÿ�� ���� �÷��̾��� �ڷ�ƾ�� �� ������ �ʴ� ���� ����. ���� ���� ����
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        GameManager.Instance.Dead.RemoveListener(SomebodyDie_1);
    }
}
