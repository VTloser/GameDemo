using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager 
{
    public List<Entity> Entities = new List<Entity> ();

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }
}
