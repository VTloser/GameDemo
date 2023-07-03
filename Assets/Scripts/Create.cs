using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Create : MonoBehaviour
{
    public int Count;
    public Image Item;

    void Start()
    {
        for (int i = 0; i < Count; i++)
        {
            Instantiate<Image>(Item, this.transform).color = new Color().Ran();
        }

        Debug.Log(EnBools());

    }

    bool[] bools = new bool[5] { true, false, false, true, true };

    int EnBools()
    {
        int num = 0;

        for (int i = 0; i < bools.Length; i++)
        {
            if (bools[i])
                num += (int)Math.Pow(2, i);
        }
        return num;

    }

    void DeBools()
    { 
        
    }

}
public static class ColorHelper
{
    public static Color Ran(this Color color)
    {
        return new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
    }
}