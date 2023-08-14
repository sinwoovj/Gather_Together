using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class applemanager : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab; // 사과 프리팹
    [SerializeField] private GameObject basket; // 바구니
    [SerializeField] private float score; // 점수
    [SerializeField] private float missscore; // 놓친 점수
    [SerializeField] private TextMeshProUGUI score_text; // 점수
    [SerializeField] private TextMeshProUGUI miss_text; //놓친 점수
    [SerializeField] private TextMeshProUGUI accuracy; //정확도
    [SerializeField] private TextMeshProUGUI applecount; // 남은 사과 수
    [SerializeField] private Slider timerSlider; //남은 시간

    [SerializeField] private float duration = 60f; // 사과 생성 시간
    [SerializeField] private int totalApples = 100; // 생성할 사과의 총 개수
    List<GameObject> apples = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnApples());
    }

    void Update()
    {
        MoveBasket();
    }

    void MoveBasket()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        basket.transform.position = new Vector3(mousePos.x, basket.transform.position.y, basket.transform.position.z);
    }

    IEnumerator SpawnApples()
    {
        // 랜덤 간격을 생성
        List<float> intervals = new List<float>();
        for (int i = 0; i < totalApples; i++)
        {
            intervals.Add(Random.Range(-0.2f, 0.2f));
        }
        // 랜덤 간격의 합 계산
        float totalRandom = 0;
        foreach (float val in intervals)
        {
            totalRandom += val;
        }

        // 랜덤 간격과 함께 평균 간격을 계산
        float averageInterval = (duration - totalRandom) / totalApples;

        // 슬라이더의 최대 값 설정
        timerSlider.maxValue = duration;
        timerSlider.value = duration;

        float elapsed = 0; // 경과 시간

        for (int i = 0; i < totalApples; i++)
        {
            yield return new WaitForSeconds(averageInterval + intervals[i]); // 무작위 간격 + 평균 간격

            GameObject apple = Instantiate(applePrefab);
            apple.transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(5.5f, 7f)); // 랜덤 위치

            Rigidbody2D appleRb = apple.AddComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 추가
            appleRb.gravityScale = Random.Range(2f, 4f); // 랜덤 속도

            // x축 방향으로 무작위 힘 추가
            appleRb.AddForce(new Vector2(Random.Range(-1f, 1f), 0), ForceMode2D.Impulse);

            foreach (var existingApple in apples)
            {
                if (existingApple != null)
                {
                    Physics2D.IgnoreCollision(apple.GetComponent<Collider2D>(), existingApple.GetComponent<Collider2D>());
                }
            }

            apples.Add(apple);

            // 경과 시간 업데이트
            elapsed += averageInterval + intervals[i];
            // 슬라이더 값 업데이트
            timerSlider.value = duration - elapsed;
        }

    }


    public void DestroyApple(GameObject apple)
    {
        apples.Remove(apple); // 사과를 리스트에서 제거
        Destroy(apple); // 사과를 파괴
        applecount.text = "남은 사과 수: " + (totalApples - (score + missscore)).ToString();
    }

    public void IncreaseScore()
    {
        score++;
        score_text.text = "받은 사과 수: " + score.ToString();
        accuracy.text = "정확도: " + (((score / (score + missscore)) * 100).ToString("F2")) + "%";

    }

    public void MissScore()
    {
        missscore++;
        miss_text.text = "놓친 사과 수: " + missscore.ToString();
    }
}
