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
            // �P�_�Z���A�ð�������� DOMove �ʵe
            Vector2 targetPosition = distA > distB ? PointA.position : PointB.position;
            collision.transform.DOMove(targetPosition, 1f).OnComplete(() =>
            {
                inputMapCtrl.Jump(false);//�����ᰱ����D
            });

            Transform JumpingChar = collision.transform.Find("Char");
            float LocalOY = JumpingChar.transform.localPosition.y;
            Sequence tweenSeq = DOTween.Sequence();
            // ���_�G�ϥ� Ease.OutQuad�A����H�b�_���ɳt���ܺC
            tweenSeq.Append(JumpingChar.DOLocalMoveY(LocalOY + 0.5f, 0.3f).SetEase(Ease.OutQuad));
            // ���U�G�ϥ� Ease.InQuad�A����H�b���U�ɥ[�t
            tweenSeq.Append(JumpingChar.DOLocalMoveY(LocalOY, 0.3f).SetEase(Ease.InCubic));
            tweenSeq.Play();
        }
    }
}
