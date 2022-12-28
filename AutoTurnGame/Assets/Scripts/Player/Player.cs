using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    protected int _health;
    protected float _attackCooldown;
    protected int _speed;

    private float _elapsedTime_attack;
    private float _elapsedTime_text;

    private WaitForSeconds _wait;

    private Vector3 _prevPosition;
    private Color _prevColor;

    private int _showDamage;

    // ��ų A ��ٿ� 3��, ��ų B ��ٿ� 5��
    private int _aCooldown;
    private int _bCooldown;
    private int _skillACooldown;
    private int _skillBCooldown;

    protected bool _someoneDead;

    [SerializeField] protected int _ammor;
    [SerializeField] protected int _offensePower;

    [SerializeField] protected Image _healthImage;
    [SerializeField] protected Image _behaviorImage;
    [SerializeField] protected TextMeshProUGUI _text;

    private void OnEnable()
    {
        GameManager.Instance.Dead.AddListener(SomebodyDie);
    }

    void Start()
    {
        _health = 100;
        _speed = Random.Range(1, 6);
        _attackCooldown = 10.0f / _speed;
        _wait = new WaitForSeconds(Time.fixedDeltaTime);
        _prevColor = _text.color;
        _prevPosition = _text.transform.position;

        _someoneDead = false;

        _aCooldown = _skillACooldown = 3;
        _bCooldown = _skillBCooldown = 5;

        StartCoroutine(BehaviorGague());
    }

    /// <summary>
    /// ���� �Լ�
    /// </summary>
    public virtual void Attack()
    { }

    /// <summary>
    /// 1��ų
    /// </summary>
    public virtual void Skill_A()
    { }

    /// <summary>
    /// 2��ų
    /// </summary>
    public virtual void Skill_B()
    { }

    /// <summary>
    /// �ǰ� ���� ��� �Լ�
    /// </summary>
    /// <param name="damage">�ٸ� �÷��̾ �ִ� ������</param>
    public void Hit(int damage)
    {
        _showDamage = damage - _ammor;
        _text.text = $"{_showDamage}";
        StopCoroutine(TextPosition());
        StartCoroutine(TextPosition());

        _health -= _showDamage;
        _healthImage.fillAmount -= (float)(_showDamage / 100f);

        if (_health <= 0f)
        {
            GameManager.Instance.SomeoneDie();
        }
    }

    /// <summary>
    /// �ǰ� ������ �ؽ�Ʈ ȿ�� ���� �ڷ�ƾ
    /// </summary>
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

    /// <summary>
    /// �ൿ ������ �ڷ�ƾ
    /// </summary>
    protected IEnumerator BehaviorGague()
    {
        _elapsedTime_attack = 0f;
        while (_elapsedTime_attack <= _attackCooldown)
        {
            _elapsedTime_attack += Time.fixedDeltaTime;
            _behaviorImage.fillAmount += Time.fixedDeltaTime / _attackCooldown;
            yield return _wait;
        }

        _behaviorImage.fillAmount = 0.0f;
        if (_skillACooldown == 0)
        {
            if (_skillBCooldown == 0)
            {
                _skillBCooldown = _bCooldown;
                Skill_B();
            }

            --_skillBCooldown;
            _skillACooldown = _aCooldown;
            Skill_A();
        }

        else if(_skillBCooldown == 0)
        {
            --_skillACooldown;
            _skillBCooldown = _bCooldown;
            Skill_B();
        }

        else
        {
            --_skillACooldown;
            --_skillBCooldown;
            Attack();
        }
    }

    /// <summary>
    /// ������ �׾��� �� �ڷ�ƾ ���ߴ� �Լ�
    /// </summary>
    private void SomebodyDie()
    {
        _someoneDead = true;
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        GameManager.Instance.Dead.RemoveListener(SomebodyDie);
    }
}
