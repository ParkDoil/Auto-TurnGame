using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : Player
{
    [SerializeField] Player1 _player1;

    private int _skillDamage;
    public override void Attack()
    {
        StopCoroutine(BehaviorGague());
        _player1.Hit(_offensePower);

        if (_someoneDead == false)
        {
            StartCoroutine(BehaviorGague());
        }
    }

    public override void Skill_A()
    {
        StopCoroutine(BehaviorGague());
        _skillDamage = Mathf.RoundToInt((float)_offensePower * 1.3f);
        _player1.Hit(_skillDamage);

        if (_someoneDead == false)
        {
            StartCoroutine(BehaviorGague());
        }
    }

    public override void Skill_B()
    {
        StopCoroutine(BehaviorGague());
        _skillDamage = Mathf.RoundToInt((float)_offensePower * 0.9f);
        _player1.Hit(_skillDamage * 2);

        if (_someoneDead == false)
        {
            StartCoroutine(BehaviorGague());
        }
    }
}
