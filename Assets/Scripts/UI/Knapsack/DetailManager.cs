using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DetailManager : MonoBehaviour
{
    public static DetailManager Instance;
    Image _DetailUI;

    public float WaitingTime = 0.5f;

    public Vector2 Offset;

    private void Awake()
    {
        Instance = this;
        _DetailUI = this.GetComponent<Image>();
    }


    //��ʾUI
    public void Show()
    {
        _DetailUI.gameObject.SetActive(true);
    }

    public void Hide()
    {
        _DetailUI.gameObject.SetActive(false);
    }

    public void Judge_Offset(Vector2 Pos)
    {
        if (Screen.width / 2 >= Pos.x) Offset.x = Mathf.Abs(Offset.x);
        else Offset.x = -Mathf.Abs(Offset.x);

        if (Screen.height / 2 >= Pos.y) Offset.y = Mathf.Abs(Offset.y);
        else Offset.y = -Mathf.Abs(Offset.y);
    }


    public void ChangePos(Vector2 Pos)
    {
        _DetailUI.transform.position = Pos + Offset;
    }

}
