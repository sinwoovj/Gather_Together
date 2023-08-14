using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyAction{ UP, DOWN, LEFT, RIGHT, INTERACTION, KEYCOUNT}
public enum StandByKeyAction { UP, DOWN, LEFT, RIGHT, INTERACTION, KEYCOUNT }

//열거형의 실제 사용하는 값의 개수와 같음.
public static class KeySettingValue { 
    public static Dictionary<StandByKeyAction, KeyCode> keys = new Dictionary<StandByKeyAction, KeyCode>(); 
}

public static class StandByKeySettingValue
{
    public static Dictionary<KeyAction, KeyCode> StandBykeys = new Dictionary<KeyAction, KeyCode>();
}
//StandByKeyAction과 KeyCode를 key값과 value값으로 한 딕셔너리 생성
public class KeyManager : MonoBehaviour
{
    public static int a = 0;
    public KeyCode[] defaultKeys = new KeyCode[] {KeyCode.W,KeyCode.S,KeyCode.A,KeyCode.D,KeyCode.F}; 
    //WASDF 키값을 defaultKeys에 배열로 저장, 사용자가 다른 키로 바꾸지 않는 이상 위의 키로 유지됨.
    private void Awake()
    {
        FExcute(a);
        a++;
    }
  
    private void FExcute(int a)
    {
        if(a == 0)
        {
            for (int i = 0; i < (int)StandByKeyAction.KEYCOUNT; i++)
            {
                KeySettingValue.keys.Add((StandByKeyAction)i, defaultKeys[i]);
                //for문을 통해서 defaultKeys에 저장된 배열을 순서대로 StandByKeyAction에 값 추가
            }
            for (int i = 0; i < (int)KeyAction.KEYCOUNT; i++)
            {
                StandByKeySettingValue.StandBykeys.Add((KeyAction)i, defaultKeys[i]);
                //for문을 통해서 defaultKeys에 저장된 배열을 순서대로 StandByKeyAction에 값 추가
            }
        }
    }


    private void OnGUI() 
    //OnGUI()는 GUI, 키  입력 등의 이벤트가 발생할 때 호출됩니다.
    {
        Event keyEvent = Event.current; 
        //Event 클래스로 현재 실해되는 Event를 불러옵니다.
        if(keyEvent.isKey)//키가 눌렸을 때만 실행.
        { 
            //이벤트의 keyCode로 현재 눌린 키보드의 값을 알 수 있습니다.
            KeySettingValue.keys[(StandByKeyAction)key] = keyEvent.keyCode; //변수 key는 int형이므로 StandByKeyAction으로 캐스팅.
            key = -1; //keys를 바꾼뒤에도 key를 다시 -1로 만듦.
        }
    }
    public int key = -1; // key 변수를 -1로 초기화.
    public void ChangeKey(int num) //OnClick에 연결할 메서드 생성
    //int 매개변수를 추가하고 key를 초기화.
    {
        key = num;
    }
}
