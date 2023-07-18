using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ScriptHeadComment : UnityEditor.AssetModificationProcessor
{
    public static void OnWillCreateAsset(string metaName)
    {
        string filePath = metaName.Replace(".meta", ""); // "Assets/Scripts/ScriptHeadCommentTest.cs"
        string fileExt = Path.GetExtension(filePath); // ".cs"
        if (fileExt != ".cs")
        {
            return; // ���ǽű�
        }
        string fileFullPath = Application.dataPath.Replace("Assets", "") + filePath; // "D:/Program Files/UnityProjects/UnityTest/Assets/Scripts/ScriptHeadCommentTest.cs"
        string fileContent = File.ReadAllText(fileFullPath); // "using System.Collections;\r\nusing System.Collections.Generic;\r\nusing UnityEngine;\r\n\r\npublic class ScriptHeadCommentTest : MonoBehaviour {\r\n\r\n\t// Use this for initialization\r\n\tvoid Start () {\r\n\t\t\r\n\t}\r\n\t\r\n\t// Update is called once per frame\r\n\tvoid Update () {\r\n\t\t\r\n\t}\r\n}\r\n"
        string commentContent =
            "/*" +
            "\n * FileName:      #FILENAME#" +
            "\n * Author:        #AUTHOR#" +
            "\n * Date:          #DATE#" +
            "\n * Describe:      Describe" +
            "\n * UnityVersion:  #UNITYVERSION#" +
            "\n * Version:       0.1" +
            "\n */" +
            "\n"; // �����Լ�����������Ҫ�Զ����ɵ���Ϣ�������ü��
        commentContent = commentContent.Replace("#FILENAME#", Path.GetFileName(fileFullPath));
        commentContent = commentContent.Replace("#AUTHOR#", Environment.UserName);
        commentContent = commentContent.Replace("#DATE#", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
        commentContent = commentContent.Replace("#UNITYVERSION#", Application.unityVersion);
        //fileContent.Insert(0, commentContent); �������׳������ı�ԭֵ
        fileContent = fileContent.Insert(0, commentContent); // "/*\n *FileName:      ScriptHeadCommentTest.cs\n *Author:        tangmingzhe\n *Date:          2020/11/12 20:00:16\n *UnityVersion:  2018.2.0f2\n */\nusing System.Collections;\r\nusing System.Collections.Generic;\r\nusing UnityEngine;\r\n\r\npublic class ScriptHeadCommentTest : MonoBehaviour {\r\n\r\n\t// Use this for initialization\r\n\tvoid Start () {\r\n\t\t\r\n\t}\r\n\t\r\n\t// Update is called once per frame\r\n\tvoid Update () {\r\n\t\t\r\n\t}\r\n}\r\n"
        File.WriteAllText(fileFullPath, fileContent);
    }
}