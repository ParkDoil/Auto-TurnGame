using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : SingletonBehavior<GameManager>
{
    public UnityEvent Dead = new UnityEvent();

    public void SomeoneDie()
    {
        Dead.Invoke();
    }
}