using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    [SerializeField]
    [Range(0.01f, 10f)]
    private float FadeTime = 3f;
    private Image image;
    public bool FadeInOn = false;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        Color color = image.color;
        //image�� color ������Ƽ�� a ������ ���� set�� �Ұ����ؼ� ������ ����

        if (FadeInOn == true)
        {
            //���� ��(a)�� 0���� ũ�� ���� �� ����
            if (color.a < 255)
            {
                color.a += Time.deltaTime * FadeTime;
            }
        }
        else
        {
            if (color.a > 0)
            {
                color.a -= Time.deltaTime * FadeTime;
            }
        }
        //�ٲ� ���� ������ image.color�� ����
        image.color = color;
    }
}
