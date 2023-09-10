using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DI;
using UnityEngine.SceneManagement;

public class bugManager : DIMono
{
    [Inject]
    public PlayData playData;

    [SerializeField] private GameObject objectToSpawn; // Inspector에서 생성할 오브젝트를 지정합니다.
    [SerializeField] private float speed = 1f; // 오브젝트의 이동 속도를 설정합니다.
    [SerializeField] private float curveRadius = 5f; // 곡선의 반지름을 설정합니다.
    [SerializeField] private int numCurvePoints = 100; // 곡선 위의 포인트 수를 설정합니다.
    [SerializeField] private float score = 0; // 점수를 저장하는 변수입니다.
    [SerializeField] private float miss = 0; // 놓친 점수를 저장하는 변수입니다.

    [SerializeField] private TextMeshProUGUI score_text; // 점수를 출력하는 텍스트입니다.
    [SerializeField] private TextMeshProUGUI miss_text; // 놓친 점수를 출력하는 텍스트입니다.
    [SerializeField] private TextMeshProUGUI accuracy_text; // 정확도를 출력하는 텍스트입니다.

    // 각 범위에 대한 x와 y의 최소, 최대 값입니다.
    [SerializeField] private float[] xBounds = { -9.5f, 9.5f, -7f, 7f };
    [SerializeField] private float[] yBounds = { -5.5f, 5.5f, -3f, 3f };

    // 좌표 변수입니다. 오브젝트가 스폰될 3개의 위치를 저장합니다.
    [SerializeField] private Vector3[] spawnPositions = new Vector3[3];

    public bool isAtSecondPosition = false;

    protected override void Initialize()
    {
        GenerateRandomPositions(); // 랜덤한 위치를 생성합니다.
        StartCoroutine(SpawnSequence()); // 스폰 시퀀스 코루틴을 시작합니다.
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            ReturnToMainScene();
        }
    }

    IEnumerator SpawnSequence()
    {
        while (true)
        {
            // Instantiate 함수를 이용하여 오브젝트를 생성합니다.
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPositions[0], Quaternion.identity);

            // 오브젝트를 2번 좌표로 이동시킵니다.
            yield return MoveObjectThroughBezierCurve(spawnedObject, spawnPositions[0], spawnPositions[1]);

            // 오브젝트가 2번 좌표에 도착했음을 나타내는 변수를 true로 설정합니다.
            isAtSecondPosition = true;

            yield return new WaitForSeconds(0.5f);

            // 오브젝트가 3번 좌표로 이동을 시작함을 나타내는 변수를 false로 설정합니다.
            isAtSecondPosition = false;

            // 오브젝트를 3번 좌표로 이동시킵니다.
            yield return MoveObjectThroughBezierCurve(spawnedObject, spawnPositions[1], spawnPositions[2]);

            if (spawnedObject != null && !spawnedObject.Equals(null))
            {
                miss++;
                miss_text.text = miss.ToString();
                if (score + miss != 0)
                {
                    accuracy_text.text = ((score / (miss + score)) * 100).ToString("F2") + "%";
                }

            }
            // 오브젝트 삭제
            Destroy(spawnedObject);
            spawnedObject = null; // 추가된 부분


            // 새로운 좌표를 생성합니다.
            GenerateRandomPositions();

            // 0.3초와 0.6초 사이의 랜덤한 시간 동안 대기합니다.
            yield return new WaitForSeconds(Random.Range(0.3f, 0.6f));
        }
    }

    void GenerateRandomPositions() // 랜덤 위치를 생성하는 함수입니다.
    {
        spawnPositions[0] = GetRandomPositionForDefaultRange();
        spawnPositions[1] = GetRandomPosition(xBounds[2], xBounds[3], yBounds[2], yBounds[3]); // 특별 범위
        spawnPositions[2] = GetRandomPositionForDefaultRange();
    }

    Vector3 GetRandomPositionForDefaultRange() // 기본 범위로 랜덤 위치를 생성하는 함수입니다.
    {
        int range = Random.Range(0, 4);
        float x, y;

        // 범위에 따라 다른 랜덤 값을 생성합니다.
        switch (range)
        {
            case 0:
                x = xBounds[1];
                y = Random.Range(yBounds[0], yBounds[1]);
                break;
            case 1:
                x = xBounds[0];
                y = Random.Range(yBounds[0], yBounds[1]);
                break;
            case 2:
                x = Random.Range(xBounds[0], xBounds[1]);
                y = yBounds[1];
                break;
            default:
                x = Random.Range(xBounds[0], xBounds[1]);
                y = yBounds[0];
                break;
        }

        return new Vector3(x, y, 0f);
    }

    Vector3 GetRandomPosition(float xMin, float xMax, float yMin, float yMax) // 주어진 범위로 랜덤 위치를 생성하는 함수입니다.
    {
        float x = Random.Range(xMin, xMax);
        float y = Random.Range(yMin, yMax);

        return new Vector3(x, y, 0f);
    }

    IEnumerator MoveObjectThroughBezierCurve(GameObject obj, Vector3 startPos, Vector3 endPos) // 베지어 곡선을 통한 오브젝트 이동 함수입니다.
    {
        Vector3 midPoint = (startPos + endPos) / 2.0f + new Vector3(Random.Range(-curveRadius, curveRadius), Random.Range(-curveRadius, curveRadius), 0);
        Vector3[] curvePoints = new Vector3[numCurvePoints];

        // 베지어 곡선을 샘플링합니다.
        for (int i = 0; i < numCurvePoints; i++)
        {
            float t = i / (float)numCurvePoints;
            curvePoints[i] = (1 - t) * (1 - t) * startPos + 2 * (1 - t) * t * midPoint + t * t * endPos;
        }

        // 곡선을 따라 오브젝트를 이동시킵니다.
        for (int i = 0; i < numCurvePoints - 1; i++)
        {
            if (obj == null || obj.Equals(null))
                yield break;
            float distance = Vector3.Distance(curvePoints[i], curvePoints[i + 1]);
            float moveTime = distance / speed;

            float startTime = Time.time;
            while (Time.time < startTime + moveTime)
            {
                if (obj == null || obj.Equals(null))
                    yield break;
                obj.transform.position = Vector3.Lerp(curvePoints[i], curvePoints[i + 1], (Time.time - startTime) / moveTime);
                yield return null;
            }
        }
    }

    public void RemoveObject(GameObject obj) // 오브젝트를 제거하는 함수입니다.
    {
        Destroy(obj);
        score++; // 점수를 증가시킵니다.
        score_text.text = score.ToString();
        if (score + miss != 0)
        {
            accuracy_text.text = ((score / (miss + score)) * 100).ToString("F2") + "%";
        }
        GenerateRandomPositions(); // 새로운 위치를 생성합니다.
    }


    public void ReturnToMainScene()
    {
        playData.miniGameScore = score;
        playData.fromMiniGame = PlayData.FromMiniGame.BugManager;
        SceneManager.LoadScene("Main");
    }
}
