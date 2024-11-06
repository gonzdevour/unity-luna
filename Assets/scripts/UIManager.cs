using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image photo_bar_ap_fill;

    private static UIManager instance;//�������
    public static UIManager Instance //�~�������ҮɨϥΪ��ܼ�
    {
        get
        {
            if (instance == null) //�즸����ɳЫس��
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
    /// �]�wAP��
    /// </summary>
    /// <param name="value">UI_Set_AP��value�O���</param>
    public void UI_Set_AP(int value)
    {
        Bar_Int ap = photo_bar_ap_fill.GetComponent<Bar_Int>();
        ap.Cur = value;
    }
}
