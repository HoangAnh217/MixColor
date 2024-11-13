using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMove : MonoBehaviour
{
    /* public Transform startPoint; // Điểm A
     public Transform endPoint; // Điểm B
     public GameObject objectPrefab; // Prefab của vật thể
     public int objectCount; // Số lượng vật thể
     public float duration = 1f; // Thời gian di chuyển
     private List<Laze2D> laze = new List<Laze2D>();*/
    public GameObject enemyPrefab; // Prefab của enemy
    public Transform pointA; // Điểm A
    public Transform pointB; // Điểm B
    private float spawnInterval; // Thời gian cách nhau giữa các lần spawn
    public int numberOfEnemies = 5; // Số lượng enemy cần spawn
    public float moveDuration = 2f; // Thời gian di chuyển từ A đến B
    private List<Laze2D> laze = new List<Laze2D>();
    private List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        /*for (int i = 0; i < objectCount; i++)
        {
            // Tính toán vị trí  = sstatartPoint.position + new Vector3(i * 1f, 0, 0); // Thay đổi 1f để điều chỉnh khoảng cách
            float distance = (endPoint.position.x - startPoint.position.x)/(objectCount);
            GameObject obj = Instantiate(objectPrefab, startPoint.position + new Vector3(distance*i,0,0), Quaternion.Euler(0,0,-90));
            laze.Add(obj.GetComponent<Laze2D>());
            obj.SetActive(true);
            MoveObject(obj,i);
        }*/
        enemies = new List<GameObject>();
        laze = new List<Laze2D>();
        // enemies = new List<GameObject>();
        spawnInterval = moveDuration / numberOfEnemies;
        StartCoroutine(SpawnEnemies());
    }

    /*void MoveObject(GameObject obj,int i)
    {
        // Tạo chuỗi hoạt động cho vật thể
        Sequence sequence = DOTween.Sequence();
        sequence.Append(obj.transform.DOMove(endPoint.position, duration*(objectCount-i)/objectCount).SetEase(Ease.Linear))
                .AppendCallback(()=> 
                {

                    Debug.Log("asdasd");

                    obj.transform.position = startPoint.position;
                    laze[i].ChangeColor();
                })
                .SetLoops(-1, LoopType.Restart); // Lặp lại vô hạn
    }*/
    private IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Tạo một enemy mới tại điểm A
            GameObject enemy = Instantiate(enemyPrefab, pointA.position, Quaternion.identity);
            enemy.SetActive(true);

            enemies.Add(enemy);
            laze.Add(enemy.GetComponent<Laze2D>()); 
            // MoveEnemy(enemies[i], i);
            StartCoroutine(MoveEnemyCoroutine(enemy, i));
            // Đợi 0.2s trước khi spawn enemy tiếp theo
            yield return new WaitForSeconds(spawnInterval);
        }
    }
    private IEnumerator MoveEnemyCoroutine(GameObject enemy, int i)
    {
        while (true)
        {
            // Di chuyển từ pointA đến pointB
            yield return MoveToPoint(enemy, pointB.position, moveDuration);

            // Đổi màu ngẫu nhiên sau khi đến pointB
            int ra = Random.Range(0, 4);
            laze[i].SetLineColor(ra);

            // Di chuyển trở lại pointA
            //yield return MoveToPoint(enemy, pointA.position, moveDuration);
            enemy.transform.position = pointA.position;
        }
    }
    private void MoveEnemy(GameObject enemy,int i)
    {
        Vector3[] path = new Vector3[]
    {
        pointA.position,
        pointB.position
    };

        enemy.transform.DOPath(path, moveDuration, PathType.CatmullRom)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart) // Vòng lặp Yoyo cho di chuyển qua lại
            .OnStepComplete(() =>
            {   
                //enemy.transform.position = pointA.position;
                int ra = Random.Range(0, 4);
                laze[i].SetLineColor(ra); // Đổi màu sau mỗi bước di chuyển hoàn thành
            });
    }
    private IEnumerator MoveToPoint(GameObject enemy, Vector3 target, float duration)
    {
        Vector3 startPosition = enemy.transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            enemy.transform.position = Vector3.Lerp(startPosition, target, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        enemy.transform.position = target; // Đảm bảo đến đúng vị trí cuối
    }
}
