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
    /// <summary>   物体名字   </summary>
    public string Name;
    /// <summary>   描述   </summary>
    public string Description;
    /// <summary>   最大堆叠数   </summary>
    public float MaxCount;
    /// <summary>   物品类型   </summary>
    public GoodsType type;
    /// <summary>   图片名称   </summary>
    public string ImageName;
    /// <summary>   物体名称   </summary>
    public string ResourcesName;
}