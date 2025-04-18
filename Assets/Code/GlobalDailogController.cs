using System;
using System.Collections;
using System.IO;
using Unity.VisualScripting;
//using UnityEditor.Playables;
using UnityEngine;
using System.Linq;

public class GlobalDailogController : MonoBehaviour
{
    public int DailogTurn = 0;
    public float DailogSpeed = 4;
    private string[] lines;
    private string Clear = "GLOB|N01||";
    public delegate void SentDailog(string message);
    public event SentDailog OnMessageReceived;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void Exp()
    {
        StartCoroutine(DailogTurnCount(5));
    }
    IEnumerator DailogTurnCount(int Max)
    {
        while (DailogTurn < Max)  // 永遠重複
        {
            OnMessageReceived?.Invoke(lines[DailogTurn]);
            //Debug.Log(lines[DailogTurn]);
            Debug.Log("DialogTurn: " + DailogTurn);
            string[] SplitMessage = lines[DailogTurn].Split(new string[] { "|" }, System.StringSplitOptions.None);
            // 等待5秒後繼續執行
            yield return new WaitForSeconds(SplitMessage[3].Length/DailogSpeed);

            DailogTurn += 1;  // 增加對話回合數
        }
        DailogTurn = 0;
        lines = new string[0];
        OnMessageReceived?.Invoke(Clear);
        //Mediator.GetDailog(Clear);
    }
    public void LongDailog(string filePath)
    {
        if (DailogTurn == 0)
        {
            if (File.Exists(filePath))
            {
                // 讀取檔案內容
                string fileContent = File.ReadAllText(filePath);
                Debug.Log(fileContent);
                lines = fileContent.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
                StartCoroutine(DailogTurnCount(lines.Length));
                /*
                while(DailogTurn<=lines.Length)
                {
                    Mediator.GetDailog(lines[DailogTurn]);
                    Debug.Log(lines[DailogTurn]);
                }
                */
            }
            else
            {
                Debug.LogError("File not found at: " + filePath);
            }
        }
    }
    public void ShortDailog(string filePath)
    {
        if (DailogTurn == 0)
        {
            if (File.Exists(filePath))
            {
                // 讀取檔案內容
                string fileContent = File.ReadAllText(filePath);
                Debug.Log(fileContent);
                lines = fileContent.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
                if (lines.Length > 0)
                {
                    int randomIndex = UnityEngine.Random.Range(0, lines.Length);
                    string selectedLine = lines[randomIndex];

                    // 把原本陣列直接改掉，只留選中的那一句
                    lines = new string[] { selectedLine };
                }
                StartCoroutine(DailogTurnCount(lines.Length));
                /*
                while(DailogTurn<=lines.Length)
                {
                    Mediator.GetDailog(lines[DailogTurn]);
                    Debug.Log(lines[DailogTurn]);
                }
                */
            }
            else
            {
                Debug.LogError("File not found at: " + filePath);
            }
        }
    }
}
