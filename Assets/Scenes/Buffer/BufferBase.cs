/*
 * FileName:      BufferBase.cs
 * Author:        Administrator
 * Date:          2024/02/18 11:08:17
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

//衰减方式，比如线性，指数，固定等
public enum DecayType
{
    Linear,
    Exponential,
    Fixed,
}

public class Buff
{
    /// <summary> Buff的名字 </summary>
    public string Name;

    /// <summary> 施加者 </summary>
    public GameObject Caster;

    /// <summary> 目标 </summary>
    public IBuffItem Target;

    /// <summary> 最大持续时间 </summary>
    public float MaxDuration;

    /// <summary> 持续时间 </summary>
    public float Duration;

    /// <summary> 最大堆叠层数 </summary>
    public int MaxStack;

    /// <summary> 当前堆叠层数 </summary>
    public int CurrentStack;

    /// <summary> 衰减方式 </summary>
    public DecayType decayType;

    /// <summary> Buff是否可刷新 </summary>
    public bool Refreshable;

    /// <summary> Buff文字描述 </summary>
    public string Info;

    /// <summary> Buff图标名称 </summary>
    public string Icon;

    /// <summary> Buff优先级 </summary>
    public int Priority; //负面类Buff优先处理 优先级低 0 - 1000

    /// <summary> 开始运行时回调 </summary>
    public UnityAction StartCallback;

    /// <summary> 效果回调 </summary>
    public UnityAction<Buff> EffectCallback;

    /// <summary> 结束回调 </summary>
    public UnityAction EndCallback;

    /// <summary> 中止回调 </summary>
    public UnityAction InterruptCallback;

    public Buff(string name, float maxDuration, int maxStack,
        DecayType decayType, bool refreshable, string info, string icon, int priority,
        UnityAction startCallback, UnityAction<Buff> effectCallback, UnityAction endCallback,
        UnityAction interruptCallback)
    {
        Name = name;
        MaxDuration = maxDuration;
        MaxStack = maxStack;
        this.decayType = decayType;
        Refreshable = refreshable;
        Info = info;
        Icon = icon;
        Priority = priority;

        StartCallback = startCallback;
        EffectCallback = effectCallback;
        EndCallback = endCallback;
        InterruptCallback = interruptCallback;
    }

    public void Begin()
    {
        Duration = MaxDuration;
        StartCallback?.Invoke();
    }

    public void End()
    {
        EndCallback?.Invoke();
    }

    public void Interrupt()
    {
        InterruptCallback?.Invoke();
    }

    public void Refresh()
    {
        if (Refreshable)
        {
            Duration = MaxDuration;
            CurrentStack = Mathf.Min(CurrentStack + 1, MaxStack);
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        else if (!(obj is Buff)) return false;
        else if ((obj as Buff).Name != this.Name) return false;
        return true;
    }
}

public interface IBuffItem
{
    public List<Buff> BuffList{ get; set; }
    public bool IsDead{ get; set; }
    public void InterruptBuff(Buff buff);
    public void RemoveBuff(Buff buff);
    public void AddBuff(Buff buff);
    public IEnumerator BuffLogic();
}

public class BuffFactor
{
    public Buff GetBuff(string BuffName)
    {
        Buff SpeedUp = new Buff("SpeedUp", 5, 5, global::DecayType.Linear, true, "提高移速", "DotImage", 1,
            () => { Debug.Log("速度提升"); }, (_) => { Debug.Log($"速度提升层数{_.CurrentStack}"); },
            () => { Debug.Log("速度恢复"); },
            () => { Debug.Log("速度提前恢复"); });


        switch (BuffName)
        {
            case "Dot":
                return new Buff("Dot", 5, 0, global::DecayType.Linear, true, "天照持续伤害", "DotImage", 2,
                    () => { Debug.Log("开始天照"); }, (_) => { Debug.Log($"收到伤害，剩余时间{_.Duration}"); },
                    () => { Debug.Log("开始结束"); },
                    () => { Debug.Log("天照被中断"); });
            case "SpeedUp":
                return SpeedUp;
            case "SpeedCut":
                return new Buff("SpeedCut", 5, 0, global::DecayType.Linear, false, "降低移速", "DotImage", 3,
                    () => { Debug.Log("速度降低"); }, (_) => { Debug.Log($"速度降低了啊"); },
                    () => { Debug.Log("速度恢复"); },
                    () => { Debug.Log("速度提前恢复"); });
            default:
                Debug.LogError("Error~!");
                return null;
        }
    }
}

public class BufferBase : MonoBehaviour, IBuffItem
{
    public List<Buff> BuffList { get; set; }
    public bool IsDead { get; set; }

    public void InterruptBuff(Buff buff)
    {
        buff.Interrupt();
        BuffList.Remove(buff);
    }

    public void RemoveBuff(Buff buff)
    {
        buff.End();
        BuffList.Remove(buff);
    }

    public void AddBuff(Buff buff)
    {
        //判断Buff是否已在Buff列表中，如果在列表中刷新，如果不在添加。
        if (BuffList.Contains(buff))
        {
            var auto = BuffList.Find(X => X.Name == buff.Name);
            if (auto != null)
            {
                auto.Refresh();
            }
        }
        else
        {
            buff.Begin();
            BuffList.Add(buff);
            BuffList.Sort(delegate(Buff x, Buff y)
            {
                if (x.Priority < y.Priority) return -1; // 如果 x 的 id 小于 y 的 id，返回 -1
                else if (x.Priority == y.Priority) return 0; // 如果 x 的 id 等于 y 的 id，返回 0
                else return 1; // 如果 x 的 id 大于 y 的 id，返回 1
            });
        }
    }

    public IEnumerator BuffLogic()
    {
        while (true)
        {
            for (int i = 0; i < BuffList.Count; i++)
            {
                if (BuffList[i].Duration <= 0.01f || IsDead /*|| 其他条件 */)
                {
                    RemoveBuff(BuffList[i]);
                    break;
                }

                BuffList[i].EffectCallback?.Invoke(BuffList[i]);

                switch (BuffList[i].decayType)
                {
                    case DecayType.Linear:
                        BuffList[i].Duration -= Time.deltaTime;
                        break;
                    case DecayType.Exponential:
                        BuffList[i].Duration *= 0.9f;
                        break;
                    default:
                        break;
                }
            }

            yield return null;
        }
    }

    private BuffFactor _buffFactor;

    private void Awake()
    {
        _buffFactor = new BuffFactor();
        StartCoroutine(BuffLogic());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddBuff(_buffFactor.GetBuff("Dot"));
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            AddBuff(_buffFactor.GetBuff("SpeedUp"));
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            AddBuff(_buffFactor.GetBuff("SpeedCut"));
        }
    }
}
