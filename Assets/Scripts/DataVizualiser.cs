using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Android;

public class DataVizualiser : MonoBehaviour
{


    [SerializeField] private GameObject UIRecordPrefab;

    [SerializeField] private GameObject contentView;





    private void CreateContentScrollView()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo = info.GetFiles();
        var objects = new List<GameObject>();
        Debug.Log(info.FullName);
        var height = UIRecordPrefab.GetComponent<RectTransform>().rect.height;
        var rect = contentView.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, (height)* fileInfo.Length);
        var parentGameObject = contentView;
        for (int i = 0; i < fileInfo.Length; ++i)
        {
            var obj = Instantiate(UIRecordPrefab, new Vector3(contentView.GetComponent<RectTransform>().rect.width/2, -(i+1) * height , 0), 
                Quaternion.identity, parentGameObject.GetComponent<RectTransform>());
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,   i == 0 ? - height :  -height * 2);
            var tr = obj.GetComponent<RectTransform>();
            tr.pivot = new Vector2(0.5f, 0);
            tr.anchorMax = new Vector2(1, 1);
            tr.anchorMin = new Vector2(0, 1);
            var tmp = obj.GetComponent<UIElementManager>();
            tmp.fileName = fileInfo[i].Name;
            tmp.creationDate = fileInfo[i].CreationTime.ToShortDateString();
            tmp.Initialize();
            parentGameObject = obj;
        }
    }
    
    void Awake()
    {
        Permission.RequestUserPermission(Permission.FineLocation);
        CreateContentScrollView();
    }
}
