using System;
using UnityEngine;


public class aop : MonoBehaviour
{
    class Program
    {
        static void Main(string[] args)
        {
            Transform t = new Transform();
            t.v.x = 1;
            t.ShowV();
            Console.Read();
        }
    }

    struct Vector
    {
        public float x;
        public float y;
        public float z;
    }

    class Transform
    {
        public Vector v { get; set; }

        public void ShowV()
        {
            Console.WriteLine(v.x + "..." + v.y + "..." + v.z);
        }
    }
}