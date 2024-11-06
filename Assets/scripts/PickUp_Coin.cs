using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp_Coin : MonoBehaviour
{
    public ParticleSystem Part_Starfall;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"{collision}碰到金幣");
        PlayerProps player = collision.GetComponent<PlayerProps>();
        if( player != null) //不可以用player?.HP_Add(1)，因?語法會與unity內部的null判斷衝突
        {
            player.HP_Add(1);
            Destroy(gameObject);//不能用Destroy(this)，因為this只代表PickUp_Coin這張class而不是遊戲物件;
            Instantiate(Part_Starfall,transform.position,Quaternion.identity);
        }
        //Destroy(collision.gameObject);//刪除吃金幣的角色
    }

    //private void ontriggerstay2d(collider2d collision)
    //{
    //    debug.log($"{collision}跟金幣在一起");
    //}

    //private void ontriggerexit2d(collider2d collision)
    //{
    //    debug.log($"{collision}離開金幣");
    //}
}
