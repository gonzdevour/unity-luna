using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_Int : MonoBehaviour
{
    public int Max = 100;
    public int Cur = 0;
    public float Offset = 13f;
    public float lerpSpeed = 5f;  // 添加速度因子

    private Vector3 initialPosition;
    private float width;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        width = rectTransform.rect.width-Offset;//初始寬度(位移距離)微調使起點右移
        //Debug.Log($"width={width}");
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (Max <= 0) return;

        float targetRatio = Mathf.Clamp01((float)Cur / Max);  // 計算目標比例
        Vector3 targetPosition = Vector3.Lerp(
            initialPosition - new Vector3(width, 0, 0),
            initialPosition,
            targetRatio
        );

        // 使用速度因子逐步過渡到目標位置
        rectTransform.anchoredPosition = Vector3.Lerp(
            rectTransform.anchoredPosition,
            targetPosition,
            lerpSpeed * Time.deltaTime
        );
    }
}

