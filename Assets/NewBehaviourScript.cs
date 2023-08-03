/*
 * FileName:      NewBehaviourScript.cs
 * Author:        摩诘创新
 * Date:          2023/08/03 16:12:30
 * Describe:      Describe
 * UnityVersion:  2021.3.23f1c1
 * Version:       0.1
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Aa;
    public GameObject Bb;
    public GameObject Ca;
    public float ModeSize = 0.58f;
    [ExecuteInEditMode]
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            JEE(Aa, Ca, Bb);
        }
    }

    [ExecuteInEditMode]
    public void JEE(GameObject A, GameObject B, GameObject C)
    {

        Vector3 point = (A.transform.position + B.transform.position);/ //中心
        Debug.DrawLine(point, A.transform.position);
        Debug.DrawLine(point, B.transform.position);
        Debug.DrawLine(point, C.transform.position);

        var DirA = (A.transform.position - point).normalized;
        float SpeedA = ModeSize - (A.transform.position - point).magnitude;
        SpeedA = Mathf.Clamp01(SpeedA);
        A.transform.transform.Translate(DirA * SpeedA);

        var DirB = (B.transform.position - point).normalized;
        float SpeedB = ModeSize - (B.transform.position - point).magnitude;
        SpeedB = Mathf.Clamp01(SpeedB);
        B.transform.transform.Translate(DirB * SpeedB);

        var DirC = (C.transform.position - point).normalized;
        float SpeedC = ModeSize - (C.transform.position - point).magnitude;
        SpeedC = Mathf.Clamp01(SpeedC);
        C.transform.transform.Translate(DirC * SpeedC);


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
