using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WarehouseItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Goods goods;
    
    private float _waitingTime;
    private bool _exit;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _exit = false;
        OnEnter(eventData);
    }

    private async void OnEnter(PointerEventData eventData)
    {
        while (!_exit && _waitingTime < DetailManager.Instance.WaitingTime)
        {
            _waitingTime += Time.deltaTime;
            await Task.Yield();
        }
        if (!_exit)
        {
            DetailManager.Instance.Show();
            Move(eventData);
        }
    }

    private async void Move(PointerEventData eventData)
    {
        DetailManager.Instance.Judge_Offset(eventData.position);
        while (!_exit)
        {
            DetailManager.Instance.ChangePos(eventData.position);
            await Task.Yield();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _waitingTime = 0;
        _exit = true;
        DetailManager.Instance.Hide();
    }
}

public enum GoodsType
{ 
    None,
    Weapon,
    Food,
}

public struct Goods
{
    /// <summary>   ��������   </summary>
    public string Name;
    /// <summary>   ����   </summary>
    public string Description;
    /// <summary>   ���ѵ���   </summary>
    public float MaxCount;
    /// <summary>   ��Ʒ����   </summary>
    public GoodsType type;
    /// <summary>   ͼƬ����   </summary>
    public string ImageName;
    /// <summary>   ��������   </summary>
    public string ResourcesName;
}