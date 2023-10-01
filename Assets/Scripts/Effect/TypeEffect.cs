using DI;
using UnityEngine;
using UnityEngine.UI; //Text 변수를 사용하기 위함

public class TypeEffect : DIMono
{
    
    //글자 재생 속도를 위한 변수 생성 (CPS) /시간차
    public int CharPerSeconds;
    public GameObject EndCursor;

    [Inject]
    PlayData playData;

    public string targetMsg;    //표시할 대화 문자열을 따로 변수로 저장
    int index;
    float interval;

    //대화 문자열을 받는 함수 생성
    public void SetMsg(string msg)
    {
        if (playData.isTypeEffectAnim){ //플래그 변수를 이용하여 분기점 로직 작성
            GetComponent<Text>().text = targetMsg;
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
        GetComponent<Text>().text = "";
        index = 0;
        EndCursor.SetActive(false);

        //Start Animation
        interval = 1.0f / CharPerSeconds; //확실한 소수값을 얻기위해 분자 1.0f 생성
        playData.isTypeEffectAnim = true;

        Invoke("Effecting", interval); //시간차 반복 호출을 위한 Invoke 함수를 사용 // 1초 / CPS = 1글자가 나오는 딜레이
    }

    void Effecting()
    {
        //End Animation
        if(GetComponent<Text>().text == targetMsg){  //대화 문자열과 Text 내용이 일치하면 종료

            EffectEnd();
            return;
        }

        GetComponent<Text>().text += targetMsg[index]; //문자열도 배열처럼 char값에 접근 가능

        //Sound
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
            GetComponent<AudioSource>().Play();

        index++;

        //Recursive 
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        playData.isTypeEffectAnim = false;
        EndCursor.SetActive(true); //종료 함수에서는 대화 마침 아이콘을 활성화(시작에선 비활성화)
    }
}
