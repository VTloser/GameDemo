/*
 * FileName:      EventCenter.cs
 * Author:        摩诘创新
 * Date:          2023/08/06 13:14:52
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCenter 
{
    /// <summary>     事件列表    </summary>
    public static Dictionary<GameEvent, Delegate> mEventTable = new Dictionary<GameEvent, Delegate>();
    /// <summary>     永存事件列表 不清理     </summary>
    private static List<GameEvent> mPerManentMessages = new List<GameEvent>();

    /// <summary>
    ///标记为用不清理事件
    /// </summary>
    /// <param name="gameEvent"></param>
    public static void MarkAsPerManent(GameEvent gameEvent) 
    {
        mPerManentMessages.Add(gameEvent);
    }

    /// <summary>
    /// 清理
    /// </summary>
    public static void ClearUp()
    {
        List<GameEvent> messagesToremove = new List<GameEvent>();
        foreach (KeyValuePair<GameEvent, Delegate> item in mEventTable)
        {
            bool wasFound = false;
            foreach (GameEvent message in mPerManentMessages)
            {
                if (item.Key == message)
                {
                    wasFound = true;
                    break;
                }
            }
            if (!wasFound) messagesToremove.Add(item.Key);
        }

        foreach (var item in messagesToremove)
        {
            mEventTable.Remove(item);
        }
    }

    private static void OnListenerAdding(GameEvent gameEvent, Delegate listenerBeingAdded)
    {
        if (!mEventTable.ContainsKey(gameEvent))
        {
            mEventTable.Add(gameEvent, null);
        }
        Delegate d = mEventTable[gameEvent];
        if (d != null && d.GetType() != listenerBeingAdded.GetType())
        {
            throw new ListenerException($"������Ͳ�һ��{gameEvent}:[{d.GetType().Name}]|[{listenerBeingAdded.GetType().Name}]");
        }
    }

    private static void OnListenerRemoving(GameEvent gameEvent, Delegate listenerBeingRemoved)
    {
        if (mEventTable.ContainsKey(gameEvent))
        {
            Delegate d = mEventTable[gameEvent];
            if (d == null)
            {
                throw new ListenerException($"�¼�û�а󶨷���{gameEvent}");
            }
            else if (d.GetType() != listenerBeingRemoved.GetType())
            {
                throw new ListenerException($"�Ƴ����������Ͳ�һ��{gameEvent}:[{d.GetType().Name}]|[{listenerBeingRemoved.GetType().Name}]");
            }
        }
        else
        {
            throw new ListenerException($"�¼��б���û��{gameEvent}");
        }
    }

    private static void OnListenerRemoved(GameEvent gameEvent)
    {
        if (mEventTable[gameEvent] == null)
        {
            mEventTable.Remove(gameEvent);
        }
    }


    private static void OnBroadcasting(GameEvent gameEvent)
    {
        if (!mEventTable.ContainsKey(gameEvent))
        {
            Debug.LogError($"�¼�{gameEvent}δ��");
        }
    }

    public static BroadCastException CreateBroadcastSingatureException(GameEvent gameEvent)
    {
        return new BroadCastException($"�㲥�¼��쳣{gameEvent}");
    }

    public static void AddListener(GameEvent gameEvent, EventCallback eventCallback)
    {
        OnListenerAdding(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback)mEventTable[gameEvent] + eventCallback;
    }
    public static void AddListener<T>(GameEvent gameEvent, EventCallback<T> eventCallback)
    {
        OnListenerAdding(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T>)mEventTable[gameEvent] + eventCallback;
    }
    public static void AddListener<T,U>(GameEvent gameEvent, EventCallback<T, U> eventCallback)
    {
        OnListenerAdding(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T, U>)mEventTable[gameEvent] + eventCallback;
    }
    public static void AddListener<T, U, V>(GameEvent gameEvent, EventCallback<T, U, V> eventCallback)
    {
        OnListenerAdding(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T, U, V>)mEventTable[gameEvent] + eventCallback;
    }
    public static void AddListener<T, U, V, X>(GameEvent gameEvent, EventCallback<T, U, V, X> eventCallback)
    {
        OnListenerAdding(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T, U, V, X>)mEventTable[gameEvent] + eventCallback;
    }

    public static void RemoveListener(GameEvent gameEvent, EventCallback eventCallback)
    {
        OnListenerRemoving(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback)mEventTable[gameEvent] - eventCallback;
        OnListenerRemoved(gameEvent);
    }
    public static void RemoveListener<T>(GameEvent gameEvent, EventCallback<T> eventCallback)
    {
        OnListenerRemoving(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T>)mEventTable[gameEvent] - eventCallback;
        OnListenerRemoved(gameEvent);
    }
    public static void RemoveListener<T, U>(GameEvent gameEvent, EventCallback<T, U> eventCallback)
    {
        OnListenerRemoving(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T, U>)mEventTable[gameEvent] - eventCallback;
        OnListenerRemoved(gameEvent);
    }
    public static void RemoveListener<T, U, V>(GameEvent gameEvent, EventCallback<T, U, V> eventCallback)
    {
        OnListenerRemoving(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T, U, V>)mEventTable[gameEvent] - eventCallback;
        OnListenerRemoved(gameEvent);
    }
    public static void RemoveListener<T, U, V, X>(GameEvent gameEvent, EventCallback<T, U, V, X> eventCallback)
    {
        OnListenerRemoving(gameEvent, eventCallback);
        mEventTable[gameEvent] = (EventCallback<T, U, V, X>)mEventTable[gameEvent] - eventCallback;
        OnListenerRemoved(gameEvent);
    }




    public static void ClearListener(GameEvent gameEvent, EventCallback eventCallback)
    {
        if (mEventTable.ContainsKey(gameEvent))
        {
            mEventTable.Remove(gameEvent);
        }
    }
    public static void ClearListener<T>(GameEvent gameEvent, EventCallback<T> eventCallback)
    {
        if (mEventTable.ContainsKey(gameEvent))
        {
            mEventTable.Remove(gameEvent);
        }
    }
    public static void ClearListener<T, U>(GameEvent gameEvent, EventCallback<T, U> eventCallback)
    {
        if (mEventTable.ContainsKey(gameEvent))
        {
            mEventTable.Remove(gameEvent);
        }
    }
    public static void ClearListener<T, U, V>(GameEvent gameEvent, EventCallback<T, U, V> eventCallback)
    {
        if (mEventTable.ContainsKey(gameEvent))
        {
            mEventTable.Remove(gameEvent);
        }
    }
    public static void ClearListener<T, U, V, X>(GameEvent gameEvent, EventCallback<T, U, V, X> eventCallback)
    {
        if (mEventTable.ContainsKey(gameEvent))
        {
            mEventTable.Remove(gameEvent);
        }
    }


    /// <summary>
    /// �¼��㲥
    /// </summary>
    public static void Broadcast(GameEvent gameEvent)
    {
        OnBroadcasting(gameEvent);
        if (mEventTable.TryGetValue(gameEvent, out Delegate d))
        {
            EventCallback callback = d as EventCallback;
            if (callback != null)
            {
                callback();
            }
            else
            {
                throw CreateBroadcastSingatureException(gameEvent);
            }
        }
    }
    public static void Broadcast<T>(GameEvent gameEvent,T t)
    {
        OnBroadcasting(gameEvent);
        if (mEventTable.TryGetValue(gameEvent, out Delegate d))
        {
            EventCallback<T> callback = d as EventCallback<T>;
            if (callback != null)
            {
                callback(t);
            }
            else
            {
                throw CreateBroadcastSingatureException(gameEvent);
            }
        }
    }
    public static void Broadcast<T,U>(GameEvent gameEvent,T t,U u)
    {
        OnBroadcasting(gameEvent);
        if (mEventTable.TryGetValue(gameEvent, out Delegate d))
        {
            EventCallback<T, U> callback = d as EventCallback<T, U>;
            if (callback != null)
            {
                callback(t,u);
            }
            else
            {
                throw CreateBroadcastSingatureException(gameEvent);
            }
        }
    }
    public static void Broadcast<T, U, V>(GameEvent gameEvent, T t, U u, V v)
    {
        OnBroadcasting(gameEvent);
        if (mEventTable.TryGetValue(gameEvent, out Delegate d))
        {
            EventCallback<T, U, V> callback = d as EventCallback<T, U, V>;
            if (callback != null)
            {
                callback(t, u, v);
            }
            else
            {
                throw CreateBroadcastSingatureException(gameEvent);
            }
        }
    }
    public static void Broadcast<T, U, V,X>(GameEvent gameEvent, T t, U u, V v,X x)
    {
        OnBroadcasting(gameEvent);
        if (mEventTable.TryGetValue(gameEvent, out Delegate d))
        {
            EventCallback<T, U, V, X> callback = d as EventCallback<T, U, V, X>;
            if (callback != null)
            {
                callback(t, u, v, x);
            }
            else
            {
                throw CreateBroadcastSingatureException(gameEvent);
            }
        }
    }



}


public class ListenerException : Exception
{
    public ListenerException(string msg) : base(msg) { }

}


public class BroadCastException : Exception
{ 
    public BroadCastException(string msg) : base(msg) { }
}