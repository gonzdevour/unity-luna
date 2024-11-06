using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProps : MonoBehaviour
{
    public float Movespeed;//5, 在GUI介面中賦值
    public int HP_Max;//10, 在GUI介面中賦值
    public int HP_Cur;//不加private的宣告則預設為private，所以private其實可以不加

    void Start()
    {
        HP_Cur = 5;
        Debug.Log($"HP:{HP_Cur}/{HP_Max}");
    }

    public void HP_Add(int value)
    {
        HP_Cur = Mathf.Clamp(HP_Cur+value,0,HP_Max);
        Debug.Log($"{HP_Cur}/{HP_Max}");
    }
}
