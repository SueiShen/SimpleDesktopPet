using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

public class WindowSetupWin : MonoBehaviour
{
#if !UNITY_EDITOR
    private struct MARGINS
    {
        public int cxLeftWidth, cxRightWidth, cyTopHeight, cyBottomHeight;
    }

    private const int GWL_EXSTYLE = -20;
    private const uint WS_EX_LAYERED = 0x00080000;
    private const uint WS_EX_TRANSPARENT = 0x00000020;
    private const uint LWA_COLORKEY = 0x00000001;
    private const uint LWA_ALPHA = 0x00000002;
    private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    [DllImport("user32.dll")]
    private static extern bool SetLayeredWindowAttributes(IntPtr hWnd, uint crKey, byte bAlpha, uint dwFlags);

    private IntPtr hWnd;

    private GameObject MainCamera;
#endif
    private void Start()
    {
#if !UNITY_EDITOR
        hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1, cxRightWidth = -1, cyTopHeight = -1, cyBottomHeight = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        // 設置視窗可透明 + 允許點擊穿透
        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);

        // 設定 RGB(0,0,0)黑幕 為透明色，確保 Unity 背景設為這個顏色
        //SetLayeredWindowAttributes(hWnd, 0, 0, LWA_COLORKEY);

        // 確保視窗保持在最上層
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
#else
        Camera.main.backgroundColor = Color.gray;
#endif
        Application.runInBackground = true;
    }
#if !UNITY_EDITOR
    private void Update()
    {
        SetClickThrought(!IsMouseOverCollider());
    }
    private void SetClickThrought(bool ClickThrought)
    {
        if(ClickThrought)
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        }else{
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
        }
    }
    private bool IsMouseOverCollider()
    {
    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    Collider2D hitCollider = Physics2D.OverlapPoint(mousePosition);
    
    return hitCollider != null; // 若 `hitCollider` 存在，代表滑鼠指向某個 `Collider2D`
    }
    void OnApplicationQuit()
    {
        Application.runInBackground = false;
        SetWindowLong(hWnd, GWL_EXSTYLE, 0);
        Application.Quit();
        System.Diagnostics.Process.GetCurrentProcess().Kill(); // 強制關閉
    }
#endif
}
