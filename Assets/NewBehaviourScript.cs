/*
 * FileName:      NewBehaviourScript.cs
 * Author:        摩诘创新
 * Date:          2023/08/03 16:12:30
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using DemoGame;
using Palmmedia.ReportGenerator.Core;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;




public class NewBehaviourScript : MonoBehaviour
{
    public DemoClass Aa;
    public DemoClass Bb;
    public DemoClass Ca;
    public DemoClass Dd;


    public float ModeSize = 0.5f;
    [ExecuteInEditMode]
    // Update is called once per frame
    void Update()
    {
        StartCoroutine("11");
        MutilCollider(Aa, Bb, Ca, Dd);
    }

    public void MutilCollider(DemoClass A, DemoClass B, DemoClass C, DemoClass D)
    {



        A.transform.transform.Translate(SingalCollider(A.demoC, B.demoC, C.demoC, D.demoC));
        B.transform.transform.Translate(SingalCollider(B.demoC, A.demoC, C.demoC, D.demoC));
        C.transform.transform.Translate(SingalCollider(C.demoC, B.demoC, A.demoC, D.demoC));
        D.transform.transform.Translate(SingalCollider(D.demoC, B.demoC, A.demoC, C.demoC));
    }

    public Vector2 SingalCollider(DemoC A, params DemoC[] Array)
    {
        Vector2 Dir = Vector2.zero;
        for (int i = 0; i < Array.Length; i++)
        {
            Dir += Assit2D(A.Pos, Array[i].Pos, A.Radio + Array[i].Radio);
        }
        return Dir;
    }

    public Vector2 Assit2D(Vector2 Tag, Vector2 Compare, float RadioSum)
    {
        if (Vector2.Distance(Tag, Compare) < RadioSum)
        {
            var point = (Tag + Compare) / 2;
            Vector2 Dir = (Tag - point).normalized;
            Dir = Dir.magnitude == 0 ? Random.insideUnitCircle : Dir;
            float Speed = RadioSum / 2 - (Tag - point).magnitude;
            return Dir * Speed;
        }
        return Vector2.zero;
    }


    public Vector3 AssVV(GameObject A, Vector3 point)
    {
        var Dir = (A.transform.position - point).normalized;
        float Speed = ModeSize - (A.transform.position - point).magnitude;
        Speed = Mathf.Clamp01(Speed);
        return Dir * Speed;
    }

    [ExecuteInEditMode]
    public void JEE(GameObject A, GameObject B, GameObject C)
    {

        Vector3 point = (A.transform.position + B.transform.position) / 2; //中心


        Ass(A, point);
        Ass(B, point);

        Vector3 dir = C.transform.position - point;
        Vector3 AB = B.transform.position - A.transform.position;

        Vector3 nor = Vector3.Cross(AB, Vector3.forward);
        nor = Vector3.Dot(dir, nor) > 0 ? nor : -nor;

        Debug.DrawLine(C.transform.position, point);
        Debug.DrawLine(B.transform.position, A.transform.position);
        Debug.DrawLine(point + nor.normalized * ModeSize * Mathf.Sqrt(3) / 2, point);
        C.transform.position = point + nor.normalized * ModeSize * Mathf.Sqrt(3) ;
    }

    public void Ass(GameObject A, Vector3 point)
    {
        var Dir = (A.transform.position - point).normalized;
        float Speed = ModeSize - (A.transform.position - point).magnitude;
        Speed = Mathf.Clamp01(Speed);
        A.transform.transform.Translate(Dir * Speed);
    }


    public void JE(GameObject A, GameObject B)
    {
        if (Vector3.Distance(A.transform.position, B.transform.position) < 1)
        {
            Vector3 point = (A.transform.position + B.transform.position) / 2;

            A.transform.transform.Translate((A.transform.position - point).normalized * Mathf.Clamp(point.magnitude, 0, 1) * Time.deltaTime);
            B.transform.transform.Translate((B.transform.position - point).normalized * Mathf.Clamp(point.magnitude, 0, 1) * Time.deltaTime);
        }
    }
}
