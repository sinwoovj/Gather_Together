using UnityEngine;

public class AppleScript : MonoBehaviour
{
    private applemanager appleManager; // applemanager ����

    private void Start()
    {
        appleManager = FindObjectOfType<applemanager>(); // applemanager �ν��Ͻ� ã��
    }

    void Update()
    {
        if (transform.position.y <= -6) // y ��ǥ�� -6 �����̸� ��� ����
        {
            Destroy(gameObject);
            appleManager.MissScore();
            appleManager.DestroyApple(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Basket")) // �ٱ��Ͽ� ������ ���� ����
        {
            appleManager.IncreaseScore();
            appleManager.DestroyApple(gameObject);
            Destroy(gameObject);
        }
    }
}
