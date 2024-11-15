using System.Collections;
using UnityEngine;

public class EnemyOnMap : MonoBehaviour
{
    public string Name;                // ���w�n�ͦ���prefab�W�١A�Ҧprockman
    public string Path;                // ���w�n�ͦ���prefab���|�A�Ҧpprefabs/enemiesOnMap/
    public Transform pointA;           // ���ʰ_�I
    public Transform pointB;           // ���ʲ��I
    public float speed = 2f;           // ���ʳt��

    private GameObject enemyInstance;  // �ͦ��� prefab ���
    private Animator enemyAnimator;    // Animator �ե�
    private bool movingToB = true;     // �Ω󱱨�ʤ�V

    void Start()
    {
        // �[�� prefab
        GameObject prefab = Resources.Load<GameObject>(Path + Name);
        if (prefab != null)
        {
            // �b�ĤH��m�ͦ� prefab
            enemyInstance = Instantiate(prefab, transform.position, Quaternion.identity);

            // ��� Animator �ե�
            enemyAnimator = enemyInstance.GetComponent<Animator>();

            // �ҰʨӦ^���ʪ���{
            StartCoroutine(MoveBetweenPoints());
        }
        else
        {
            Debug.LogError("Prefab not found: " + Path);
        }
    }

    // ��{�Ω󱱨�b A �M B �I�������Ӧ^����
    private IEnumerator MoveBetweenPoints()
    {
        while (true)
        {
            Transform target = movingToB ? pointB : pointA;  // �ھڤ�V�T�w�ؼ��I
                                                             
            if (target.position.x > enemyInstance.transform.position.x) // �P�_�蹳
            {
                enemyInstance.transform.localScale = new Vector3(-1, 1, 1);  // �蹳
            }
            else
            {
                enemyInstance.transform.localScale = new Vector3(1, 1, 1);  // �٭�
            }

            // ���� Walk �ʵe
            if (enemyAnimator != null)
            {
                enemyAnimator.Play("walk");
            }

            // ���ʨ�ؼ��I
            while (Vector2.Distance(enemyInstance.transform.position, target.position) > 0.1f)
            {
                enemyInstance.transform.position = Vector2.MoveTowards(
                    enemyInstance.transform.position,
                    target.position,
                    speed * Time.deltaTime
                );
                yield return null;
            }

            // ��F�ؼ��I�Ἵ�� Idle �ʵe
            if (enemyAnimator != null)
            {
                enemyAnimator.Play("idle");
            }

            // �����V
            movingToB = !movingToB;

            // ���y�ɶ�
            yield return new WaitForSeconds(1f);
        }
    }
}
