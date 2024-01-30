using UnityEngine;

public class AppleScript : MonoBehaviour
{
    private applemanager appleManager; // applemanager 참조

    private void Start()
    {
        appleManager = FindObjectOfType<applemanager>(); // applemanager 인스턴스 찾기
    }

    void Update()
    {
        if (transform.position.y <= -6) // y 좌표가 -6 이하이면 사과 삭제
        {
            Destroy(gameObject);
            appleManager.MissScore();
            appleManager.DestroyApple(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Basket")) // 바구니에 닿으면 점수 증가
        {
            appleManager.IncreaseScore();
            appleManager.DestroyApple(gameObject);
            Destroy(gameObject);
        }
    }
}
