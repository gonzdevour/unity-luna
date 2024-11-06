using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar_Int : MonoBehaviour
{
    public int Max = 100;
    public int Cur = 0;
    public float Offset = 13f;
    public float lerpSpeed = 5f;  // �K�[�t�צ]�l

    private Vector3 initialPosition;
    private float width;
    private RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        initialPosition = rectTransform.anchoredPosition;
        width = rectTransform.rect.width-Offset;//��l�e��(�첾�Z��)�L�ըϰ_�I�k��
        //Debug.Log($"width={width}");
    }

    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (Max <= 0) return;

        float targetRatio = Mathf.Clamp01((float)Cur / Max);  // �p��ؼФ��
        Vector3 targetPosition = Vector3.Lerp(
            initialPosition - new Vector3(width, 0, 0),
            initialPosition,
            targetRatio
        );

        // �ϥγt�צ]�l�v�B�L���ؼЦ�m
        rectTransform.anchoredPosition = Vector3.Lerp(
            rectTransform.anchoredPosition,
            targetPosition,
            lerpSpeed * Time.deltaTime
        );
    }
}

