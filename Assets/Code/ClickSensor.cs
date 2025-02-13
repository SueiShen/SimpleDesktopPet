using System.IO;
using UnityEngine;

public class ClickSensor : MonoBehaviour
{
    private GameObject Core;
    private GlobalDailogController GlobalDailogController;
    private Mediator Mediator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Core = GameObject.Find("Core");
        GlobalDailogController = Core.GetComponent<GlobalDailogController>();
        Mediator = GetComponentInParent<Mediator>();
    }

    // Update is called once per frame
    void Update()
    {
if (GlobalDailogController != null)
{
    Core = GameObject.Find("Core");
    GlobalDailogController = Core.GetComponent<GlobalDailogController>();
}
else
{
    Debug.LogError("GlobalDailogController is null");
}
    }
    private void OnMouseDown()
    {
        // 當滑鼠點擊到這個物件時執行的代碼
        Debug.Log("Mouse Clicked on: " + Mediator.Character);
        //"C:\Users\pTATq\Downloads\BnR\SimpleDesktopPet_Data\StreamingAssets\Characters\TIMK\Dialog\LongLog\LongLog1.txt"
        GlobalDailogController.LongDailog(Path.Combine(Application.streamingAssetsPath,"Characters/TIMK/Dialog/LongLog/LongLog1.txt"));
    }
}
