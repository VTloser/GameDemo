using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class ResourceManager
{
    public T Load<T>(string Path) where T : Object
    {
        return Resources.Load<T>(Path);
    }
}
