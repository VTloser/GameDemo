using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DemoGame
{
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

        public List<UITrace> UI_Traces;

        public int RenderTextureSize;

        private void Start()
        {
            RenderTexture renderTexture = new RenderTexture(RenderTextureSize, RenderTextureSize, 0);
            MiniCam.targetTexture = renderTexture;
            MiniImage.GetComponent<RawImage>().texture = renderTexture;
            MiniCam.transform.position = new Vector3(Center.position.x, MiniCam.transform.position.y, Center.position.z);

            for (int i = 0; i < GameManager.Instance.MiniMapTail.Count; i++)
            {
                var uITrace = Instantiate(GameManager.Instance.ResourceManager.Load<UITrace>("UITrace"), MiniImage);
                uITrace.Image.sprite = GetUITrace(GameManager.Instance.MiniMapTail[i]._MiniType);
                uITrace.Tag = GameManager.Instance.MiniMapTail[i]._Transform;
                UI_Traces.Add(uITrace);
            }
        }


        public Sprite GetUITrace(MiniType type)
        {
            switch (type)
            {
                case MiniType.None:
                    return null;
                case MiniType.Player:
                    return GameManager.Instance.ResourceManager.Load<Sprite>("Player");
                case MiniType.Enemy:
                    return GameManager.Instance.ResourceManager.Load<Sprite>("Enemy");
                case MiniType.Props:
                    return GameManager.Instance.ResourceManager.Load<Sprite>("Props");
                default:
                    Debug.LogError($"δ���������{type}");
                    break;
            }
            return null;
        }

        private void Update()
        {
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
        /// UI����Ŀ��
        /// </summary>
        public void Trace()
        {
            for (int i = 0; i < UI_Traces.Count; i++)
            {
                Vector3 Tag = RectTransformUtility.WorldToScreenPoint(MiniCam, UI_Traces[i].Tag.position);
                Tag -= new Vector3(RenderTextureSize / 2, Screen.height / 2, 0);
                Tag *= (MiniMap.localScale.x * MiniImage.localScale.x * MiniImage.sizeDelta.x / RenderTextureSize);
                Tag += MiniMap.position;
                UI_Traces[i].transform.position = Tag;
            }
        }


        /// <summary>
        /// ��ȡ������Ƕ�
        /// </summary>
        /// <param name="Pos_A"></param>
        /// <param name="Pos_B"></param>
        /// <returns></returns>
        public float Two_ObjectAngle(Vector3 Pos_A, Vector3 Pos_B)
        {
            Vector3 dir = new Vector2(Pos_B.x, Pos_B.z) - new Vector2(Pos_A.x, Pos_A.z);
            float angle = Vector2.Angle(Vector2.up, dir); //���������֮��ļн� 
            angle *= Mathf.Sign(Vector2.Dot(dir, Vector2.left));  //���������������Ϸ���������ˣ����Ϊ1��-1��������ת���� 
            return angle;
        }

        private float _angle;
        /// <summary>
        /// ��ⷢ��
        /// </summary>
        private void Detection()
        {
            _angle = ScanningLine.transform.eulerAngles.z;
            _angle = _angle >= 180 ? _angle - 360 : _angle;
            foreach (var item in UI_Traces)
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
        /// �����ͼ ������������
        /// </summary>
        private void ClickMap()
        {
            DemoImage.transform.position = Input.mousePosition;
            Ray ray = RectTransformUtility.ScreenPointToRay(MiniCam, (DemoImage.transform.position - MiniMap.position) / MiniMap.localScale.x / MiniImage.localScale.x / (MiniImage.sizeDelta.x / RenderTextureSize));
            ray.origin += new Vector3(MiniCam.orthographicSize, 0, MiniCam.orthographicSize); //UI�������Size
            if (Physics.Raycast(ray.origin, ray.direction, out hitInfo, rayDistance, raymask))
            {
                Cube.transform.position = hitInfo.point;
            }
        }
    }
}