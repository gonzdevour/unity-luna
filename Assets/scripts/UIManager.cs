using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image photo_bar_ap_fill;

    private static UIManager instance;//內部單例
    public static UIManager Instance //外部獲取單例時使用的變數
    {
        get
        {
            if (instance == null) //初次獲取時創建單例
            {
                instance = FindObjectOfType<UIManager>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("GameManager");
                    instance = singletonObject.AddComponent<UIManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    /// <summary>
    /// 設定AP條
    /// </summary>
    /// <param name="value">UI_Set_AP的value是整數</param>
    public void UI_Set_AP(int value)
    {
        Bar_Int ap = photo_bar_ap_fill.GetComponent<Bar_Int>();
        ap.Cur = value;
    }
}
