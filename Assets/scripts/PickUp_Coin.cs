using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Coin : MonoBehaviour
{
    public ParticleSystem Part_Starfall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision}�I�����");
        PlayerProps player = collision.GetComponent<PlayerProps>();
        if( player != null) //���i�H��player?.HP_Add(1)�A�]?�y�k�|�Punity������null�P�_�Ĭ�
        {
            player.HP_Add(1);
            Destroy(gameObject);//�����Destroy(this)�A�]��this�u�N��PickUp_Coin�o�iclass�Ӥ��O�C������;
            Instantiate(Part_Starfall,transform.position,Quaternion.identity);
        }
        //Destroy(collision.gameObject);//�R���Y����������
    }

    //private void ontriggerstay2d(collider2d collision)
    //{
    //    debug.log($"{collision}������b�@�_");
    //}

    //private void ontriggerexit2d(collider2d collision)
    //{
    //    debug.log($"{collision}���}����");
    //}
}
