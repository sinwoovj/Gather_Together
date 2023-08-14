using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class applemanager : MonoBehaviour
{
    [SerializeField] private GameObject applePrefab; // ��� ������
    [SerializeField] private GameObject basket; // �ٱ���
    [SerializeField] private float score; // ����
    [SerializeField] private float missscore; // ��ģ ����
    [SerializeField] private TextMeshProUGUI score_text; // ����
    [SerializeField] private TextMeshProUGUI miss_text; //��ģ ����
    [SerializeField] private TextMeshProUGUI accuracy; //��Ȯ��
    [SerializeField] private TextMeshProUGUI applecount; // ���� ��� ��
    [SerializeField] private Slider timerSlider; //���� �ð�

    [SerializeField] private float duration = 60f; // ��� ���� �ð�
    [SerializeField] private int totalApples = 100; // ������ ����� �� ����
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
        // ���� ������ ����
        List<float> intervals = new List<float>();
        for (int i = 0; i < totalApples; i++)
        {
            intervals.Add(Random.Range(-0.2f, 0.2f));
        }
        // ���� ������ �� ���
        float totalRandom = 0;
        foreach (float val in intervals)
        {
            totalRandom += val;
        }

        // ���� ���ݰ� �Բ� ��� ������ ���
        float averageInterval = (duration - totalRandom) / totalApples;

        // �����̴��� �ִ� �� ����
        timerSlider.maxValue = duration;
        timerSlider.value = duration;

        float elapsed = 0; // ��� �ð�

        for (int i = 0; i < totalApples; i++)
        {
            yield return new WaitForSeconds(averageInterval + intervals[i]); // ������ ���� + ��� ����

            GameObject apple = Instantiate(applePrefab);
            apple.transform.position = new Vector2(Random.Range(-7.5f, 7.5f), Random.Range(5.5f, 7f)); // ���� ��ġ

            Rigidbody2D appleRb = apple.AddComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ �߰�
            appleRb.gravityScale = Random.Range(2f, 4f); // ���� �ӵ�

            // x�� �������� ������ �� �߰�
            appleRb.AddForce(new Vector2(Random.Range(-1f, 1f), 0), ForceMode2D.Impulse);

            foreach (var existingApple in apples)
            {
                if (existingApple != null)
                {
                    Physics2D.IgnoreCollision(apple.GetComponent<Collider2D>(), existingApple.GetComponent<Collider2D>());
                }
            }

            apples.Add(apple);

            // ��� �ð� ������Ʈ
            elapsed += averageInterval + intervals[i];
            // �����̴� �� ������Ʈ
            timerSlider.value = duration - elapsed;
        }

    }


    public void DestroyApple(GameObject apple)
    {
        apples.Remove(apple); // ����� ����Ʈ���� ����
        Destroy(apple); // ����� �ı�
        applecount.text = "���� ��� ��: " + (totalApples - (score + missscore)).ToString();
    }

    public void IncreaseScore()
    {
        score++;
        score_text.text = "���� ��� ��: " + score.ToString();
        accuracy.text = "��Ȯ��: " + (((score / (score + missscore)) * 100).ToString("F2")) + "%";

    }

    public void MissScore()
    {
        missscore++;
        miss_text.text = "��ģ ��� ��: " + missscore.ToString();
    }
}
