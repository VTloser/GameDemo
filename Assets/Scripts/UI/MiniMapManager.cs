using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Unity.VisualScripting;

public class MiniMapManager : MonoBehaviour
{
    public bool EnableScann;
    public GameObject ScanningLine;

    public Transform Center;

    public bool Follow_Center;

    public Camera MiniCam;

    public Transform MiniMap;

    public RectTransform MiniImage;

    public GameObject DemoImage;

    public GameObject Cube;

    public float RotateSpeed;

    public List<UITrace> Entity_Traces;

    public int RenderTextureSize;

    private void Awake()
    {
        RenderTexture renderTexture = new RenderTexture(RenderTextureSize, RenderTextureSize, 0);
        MiniCam.targetTexture = renderTexture;
        MiniImage.GetComponent<RawImage>().texture = renderTexture;
        MiniCam.transform.position = new Vector3(Center.position.x, MiniCam.transform.position.y, Center.position.z);

        for (int i = 0; i < GameManager.Instance.Entities.Count; i++)
        {
            var uITrace = Instantiate(Resources.Load<UITrace>("UITrace"), MiniImage);
            uITrace.Image.sprite = GameManager.Instance.Entities[i].Image;
            uITrace.Tag = GameManager.Instance.Entities[i].transform;
            Entity_Traces.Add(uITrace);
        }

    }

    private void Update()
    {
        Debug.Log(GameManager.Instance.Entities.Count);


        if (Follow_Center)
            MiniCam.transform.position = Center.transform.position;
        if (EnableScann)
        {
            ScanningLine.gameObject.SetActive(true);
            ScanningLine.transform.Rotate(-Vector3.forward * Time.deltaTime * RotateSpeed);
        }
        else
        {
            ScanningLine.gameObject.SetActive(false);
        }

        Trace();
        Detection();
        ClickMap();
    }

    /// <summary>
    /// UI跟踪目标
    /// </summary>
    public void Trace()
    {
        for (int i = 0; i < Entity_Traces.Count; i++)
        {
            Vector3 Tag = RectTransformUtility.WorldToScreenPoint(MiniCam, Entity_Traces[i].Tag.position);
            Tag -= new Vector3(RenderTextureSize / 2, Screen.height / 2, 0);
            Tag *= (MiniMap.localScale.x * MiniImage.localScale.x * MiniImage.sizeDelta.x / RenderTextureSize);
            Tag += MiniMap.position;
            Entity_Traces[i].transform.position = Tag;
        }
    }

    /// <summary>
    /// 获取两物体角度
    /// </summary>
    /// <param name="Pos_A"></param>
    /// <param name="Pos_B"></param>
    /// <returns></returns>
    public float Two_ObjectAngle(Vector3 Pos_A, Vector3 Pos_B)
    {
        Vector3 dir = new Vector2(Pos_B.x, Pos_B.z) - new Vector2(Pos_A.x, Pos_A.z);
        float angle = Vector2.Angle(Vector2.up, dir); //求出两向量之间的夹角 
        angle *= Mathf.Sign(Vector2.Dot(dir, Vector2.left));  //求法线向量与物体上方向向量点乘，结果为1或-1，修正旋转方向 
        return angle;
    }

    private float _angle;
    /// <summary>
    /// 检测发现
    /// </summary>
    private void Detection()
    {
        _angle = ScanningLine.transform.eulerAngles.z;
        _angle = _angle >= 180 ? _angle - 360 : _angle;
        foreach (var item in Entity_Traces)
        {
            item.Angle = Two_ObjectAngle(Center.transform.position, item.Tag.position);

            if (item.Angle > _angle - 5 && item.Angle < _angle + 5)
            {
                item.Image.DOColor(Color.green, 0.1f);
            }
            else
            {
                item.Image.DOColor(Color.white, 1);
            }
        }
    }

    [Header("ClickMap")]
    public float rayDistance = 200;
    public LayerMask raymask = -1;
    private RaycastHit hitInfo;
    /// <summary>
    /// 点击地图 返回世界坐标
    /// </summary>
    private void ClickMap()
    {
        DemoImage.transform.position = Input.mousePosition;
        Ray ray = RectTransformUtility.ScreenPointToRay(MiniCam, (DemoImage.transform.position - MiniMap.position) / MiniMap.localScale.x / MiniImage.localScale.x / (MiniImage.sizeDelta.x / RenderTextureSize));
        ray.origin += new Vector3(MiniCam.orthographicSize, 0, MiniCam.orthographicSize); //UI摄像机的Size
        if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, rayDistance, raymask))
        {
            Cube.transform.position = hitInfo.point;
        }
    }

}
