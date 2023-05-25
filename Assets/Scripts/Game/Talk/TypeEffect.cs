using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Text 변수를 사용하기 위함

public class TypeEffect : MonoBehaviour
{
    
    //글자 재생 속도를 위한 변수 생성 (CPS) /시간차
    public int CharPerSeconds;
    public GameObject EndCursor;
    public bool isAnim; //애니메이션 실행 판단을 위한 플래그 변수 생성


    AudioSource audioSource;

    string targetMsg;    //표시할 대화 문자열을 따로 변수로 저장
    Text msgText;    //Text변수 생성, 초기화 후 시작함수에서 공백 처리
    int index;
    float interval;

    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    //대화 문자열을 받는 함수 생성
    public void SetMsg(string msg)
    {
        if (isAnim){ //플래그 변수를 이용하여 분기점 로직 작성
            msgText.text = targetMsg;
            CancelInvoke(); //실행중인 Invoke 함수를 종료
            EffectEnd();
        }
        else{
            targetMsg = msg;
            EffectStart();
        }
    }

    //애니메이션 재생을 위한 시작-재생-종료 3개 함수 생성
    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        //Start Animation
        interval = 1.0f / CharPerSeconds; //확실한 소수값을 얻기위해 분자 1.0f 생성
        isAnim = true;

        Invoke("Effecting", interval); //시간차 반복 호출을 위한 Invoke 함수를 사용 // 1초 / CPS = 1글자가 나오는 딜레이
    }

    void Effecting()
    {
        //End Animation
        if(msgText.text == targetMsg){  //대화 문자열과 Text 내용이 일치하면 종료

            EffectEnd();
            return;
        }

        msgText.text += targetMsg[index]; //문자열도 배열처럼 char값에 접근 가능

        //Sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;

        //Recursive 
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        isAnim = false;
        EndCursor.SetActive(true); //종료 함수에서는 대화 마침 아이콘을 활성화(시작에선 비활성화)
    }
}
