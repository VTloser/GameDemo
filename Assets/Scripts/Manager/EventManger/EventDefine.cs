/*
 * FileName:      EventDefine.cs
 * Author:        摩诘创新
 * Date:          2023/08/06 13:14:52
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameEvent : int
{
    Error = -1,
    None = 0,

    Start = 2,
    Pause = 3,
    Resume = 4,
    Stop = 5,

    GameBegin = 6,


    LevelSuccess,
    LevelFailde,

}


public delegate void EventCallback();
public delegate void EventCallback<T>(T t);
public delegate void EventCallback<T, U>(T t, U u);
public delegate void EventCallback<T, U, V>(T t, U u, V v);
public delegate void EventCallback<T, U, V, X>(T t, U u, V v, X x);