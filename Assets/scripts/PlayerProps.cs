using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProps : MonoBehaviour
{
    public float Movespeed;//5, �bGUI���������
    public int HP_Max;//10, �bGUI���������
    public int HP_Cur;//���[private���ŧi�h�w�]��private�A�ҥHprivate���i�H���[

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
