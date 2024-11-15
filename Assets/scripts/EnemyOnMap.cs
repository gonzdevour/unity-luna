using System.Collections;
using UnityEngine;

public class EnemyOnMap : MonoBehaviour
{
    public string Name;                // 指定要生成的prefab名稱，例如rockman
    public string Path;                // 指定要生成的prefab路徑，例如prefabs/enemiesOnMap/
    public Transform pointA;           // 移動起點
    public Transform pointB;           // 移動終點
    public float speed = 2f;           // 移動速度

    private GameObject enemyInstance;  // 生成的 prefab 實例
    private Animator enemyAnimator;    // Animator 組件
    private bool movingToB = true;     // 用於控制移動方向

    void Start()
    {
        // 加載 prefab
        GameObject prefab = Resources.Load<GameObject>(Path + Name);
        if (prefab != null)
        {
            // 在敵人位置生成 prefab
            enemyInstance = Instantiate(prefab, transform.position, Quaternion.identity);

            // 獲取 Animator 組件
            enemyAnimator = enemyInstance.GetComponent<Animator>();

            // 啟動來回移動的協程
            StartCoroutine(MoveBetweenPoints());
        }
        else
        {
            Debug.LogError("Prefab not found: " + Path);
        }
    }

    // 協程用於控制在 A 和 B 點之間的來回移動
    private IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            Transform target = movingToB ? pointB : pointA;  // 根據方向確定目標點
                                                             
            if (target.position.x > enemyInstance.transform.position.x) // 判斷鏡像
            {
                enemyInstance.transform.localScale = new Vector3(-1, 1, 1);  // 鏡像
            }
            else
            {
                enemyInstance.transform.localScale = new Vector3(1, 1, 1);  // 還原
            }

            // 播放 Walk 動畫
            if (enemyAnimator != null)
            {
                enemyAnimator.Play("walk");
            }

            // 移動到目標點
            while (Vector2.Distance(enemyInstance.transform.position, target.position) > 0.1f)
            {
                enemyInstance.transform.position = Vector2.MoveTowards(
                    enemyInstance.transform.position,
                    target.position,
                    speed * Time.deltaTime
                );
                yield return null;
            }

            // 到達目標點後播放 Idle 動畫
            if (enemyAnimator != null)
            {
                enemyAnimator.Play("idle");
            }

            // 反轉方向
            movingToB = !movingToB;

            // 停頓時間
            yield return new WaitForSeconds(1f);
        }
    }
}
