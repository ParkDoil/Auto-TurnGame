using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * 참고 사항 : Player1, Player2 스크립트는 세부적인 동작은 동일합니다. 그래서 주석은 Player1에만 달아 두겠습니다.
 * 코드 리팩토링은 본가를 내려가야하는 상황이라 지금 당장 어렵고 추후에 꼭 리팩토링 하겠습니다.
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
    /// 공격 함수
    /// </summary>
    public override void Attack()
    {
        base.Attack();
        _player2.Hit(AttackDamage);

        StartCoroutine(BehaviorGague());
    }

    /// <summary>
    /// 피격 판정 계산 함수
    /// </summary>
    /// <param name="damage">다른 플레이어가 주는 데미지</param>
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
    /// 피격 데미지 텍스느 효과 연출 코루틴
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
    /// 행동 게이지 코루틴
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
    /// 누군가 죽었을 때 코루틴 멈추는 함수
    /// </summary>
    private void SomebodyDie_1()
    {
        //TODO: 누군가 죽었을때 그 막타를 떄린 플레이어의 코루틴이 다 멈추지 않는 현상 있음. 추후 수정 예정
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        GameManager.Instance.Dead.RemoveListener(SomebodyDie_1);
    }
}
