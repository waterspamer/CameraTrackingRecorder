using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Accord.Math;
using UI;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;
using Vector3 = UnityEngine.Vector3;

public class DataVisualiser : MonoBehaviour
{
    
    [SerializeField] private GameObject UIRecordPrefab;

    [SerializeField] private GameObject contentView;

    [SerializeField] private GameObject newElementText;
    

    public void RecalculateScrollViewSize() {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo =  info.GetFiles().OrderByDescending(file => file.CreationTime).ToArray();

        newElementText.SetActive(fileInfo.Length == 0);
        
        var height = UIRecordPrefab.GetComponent<RectTransform>().rect.height;
        var rect = contentView.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, (height)* fileInfo.Length);
    }


    public VerticalLayoutGroup contentLayoutGroup; 
    
    private void CreateAlternativeContentScrollView()
    {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo =  info.GetFiles().OrderByDescending(file => file.CreationTime).ToArray();
        newElementText.SetActive(fileInfo.Length == 0);
        Debug.Log(info.FullName);
        var height = UIRecordPrefab.GetComponent<RectTransform>().rect.height;
        var rect = contentView.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, (height)* fileInfo.Length);
        var parentGameObject = contentView;
        
        for (int i = 0; i < fileInfo.Length; ++i) {
            var obj = Instantiate(UIRecordPrefab, new Vector3(contentView.GetComponent<RectTransform>().rect.width/2, -(i+1) * height , 0), 
                Quaternion.identity, parentGameObject.GetComponent<RectTransform>());
            
            var tmp = obj.GetComponent<UIElementManager>();
            tmp.fileName = fileInfo[i].Name;
            tmp.creationDate = fileInfo[i].CreationTime.ToString("dd/MMM").Replace('/', ' ');
            tmp.fileSize = $"{(int) (fileInfo[i].Length / 1000f)} KB";
            tmp.Initialize();
        }

    }
    

    private void CreateContentScrollView() {
        var info = new DirectoryInfo(Application.persistentDataPath);
        var fileInfo =  info.GetFiles().OrderByDescending(file => file.CreationTime).ToArray();
        newElementText.SetActive(fileInfo.Length == 0);
        Debug.Log(info.FullName);
        var height = UIRecordPrefab.GetComponent<RectTransform>().rect.height;
        var rect = contentView.GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(0, (height)* fileInfo.Length);
        var parentGameObject = contentView;
        
        for (int i = 0; i < fileInfo.Length; ++i) {
            var obj = Instantiate(UIRecordPrefab, new Vector3(contentView.GetComponent<RectTransform>().rect.width/2, -(i+1) * height , 0), 
                Quaternion.identity, parentGameObject.GetComponent<RectTransform>());
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0,   i == 0 ? - height :  -height * 2);
            var tr = obj.GetComponent<RectTransform>();
            tr.pivot = new Vector2(0.5f, 0);
            tr.anchorMax = new Vector2(1, 1);
            tr.anchorMin = new Vector2(0, 1);
            var tmp = obj.GetComponent<UIElementManager>();
            tmp.fileName = fileInfo[i].Name;
            tmp.creationDate = fileInfo[i].CreationTime.ToString("dd/MMM").Replace('/', ' ');
            tmp.fileSize = $"{(int) (fileInfo[i].Length / 1000f)} KB";
            tmp.Initialize();
            parentGameObject = obj;
        }
    }
    
    void Awake() {
        Permission.RequestUserPermission(Permission.FineLocation);
        CreateContentScrollView();
        //CreateAlternativeContentScrollView();
    }
}
