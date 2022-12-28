using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : Player
{
    [SerializeField] Player2 _player2;

    private int _skillDamage;
    public override void Attack()
    {
        StopCoroutine(BehaviorGague());
        _player2.Hit(_offensePower);

        if (_someoneDead == false)
        {
            StartCoroutine(BehaviorGague());
        }
    }

    public override void Skill_A()
    {
        StopCoroutine(BehaviorGague());
        _health += 20;
        _healthImage.fillAmount += 0.2f;

        if (_someoneDead == false)
        {
            StartCoroutine(BehaviorGague());
        }
    }

    public override void Skill_B()
    {
        StopCoroutine(BehaviorGague());
        _skillDamage = (int)(50 + ((float)_ammor * 0.1f));
        _player2.Hit(_skillDamage);

        if (_someoneDead == false)
        {
            StartCoroutine(BehaviorGague());
        }
    }
}
