using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class JumpArea : MonoBehaviour
{
    public Transform PointA;
    public Transform PointB;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InputMapCtrl inputMapCtrl = collision.GetComponent<InputMapCtrl>();
            inputMapCtrl.Jump(true);

            float distA = Vector2.Distance(collision.transform.position, PointA.transform.position);
            float distB = Vector2.Distance(collision.transform.position, PointB.transform.position);
            // 判斷距離，並執行相應的 DOMove 動畫
            Vector2 targetPosition = distA > distB ? PointA.position : PointB.position;
            collision.transform.DOMove(targetPosition, 1f).OnComplete(() =>
            {
                inputMapCtrl.Jump(false);//完成後停止跳躍
            });

            Transform JumpingChar = collision.transform.Find("Char");
            float LocalOY = JumpingChar.transform.localPosition.y;
            Sequence tweenSeq = DOTween.Sequence();
            // 跳起：使用 Ease.OutQuad，讓對象在起跳時速度變慢
            tweenSeq.Append(JumpingChar.DOLocalMoveY(LocalOY + 0.5f, 0.3f).SetEase(Ease.OutQuad));
            // 落下：使用 Ease.InQuad，讓對象在落下時加速
            tweenSeq.Append(JumpingChar.DOLocalMoveY(LocalOY, 0.3f).SetEase(Ease.InCubic));
            tweenSeq.Play();
        }
    }
}
