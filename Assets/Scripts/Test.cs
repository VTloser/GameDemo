/*
 * FileName:      Test.cs
 * Author:        摩诘创新
 * Date:          2023/08/01 10:50:18
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DemoGame
{
    public class Test : MonoBehaviour
    {
        // Start is called before the first frame update
        public GameObject T;
        void Start()
        {
            T.transform.DORotate(Vector3.forward * 90, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
            
            T.transform.DOMove(Vector3.right * 50, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
