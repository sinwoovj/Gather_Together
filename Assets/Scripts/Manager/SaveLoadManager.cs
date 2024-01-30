using DI;
using UnityEngine;

public class SaveLoadManager : DIMono
{
    [Inject]
    UserData userData;

    [Inject]
    GameData gameData;


    public static string SaveKey;
    public static string JsonData;
    public static UserData SaveUserData(UserData userData, SaveSlot slot)
    {
        string saveKey = "UserData_" + slot.ToString();
        string jsonData = JsonUtility.ToJson(userData);
        SaveKey = saveKey;
        JsonData = jsonData;
        if (PlayerPrefs.HasKey(saveKey))
        {
            return JsonUtility.FromJson<UserData>(PlayerPrefs.GetString(saveKey));
        }
        else
        {
            return null; // 해당 슬롯에 저장된 데이터가 없을 때
        }
    }

    public static UserData LoadUserData(SaveSlot slot)
    {
        string saveKey = "UserData_" + slot.ToString();
        if (PlayerPrefs.HasKey(saveKey))
        {
            string jsonData = PlayerPrefs.GetString(saveKey);
            return JsonUtility.FromJson<UserData>(jsonData);
        }
        else
        {
            return null; // 해당 슬롯에 저장된 데이터가 없을 때
        }
    }

    public static UserData InitializationUserData(SaveSlot slot)
    {
        string saveKey = "UserData_" + slot.ToString();
        if (PlayerPrefs.HasKey(saveKey))
        {
            SaveKey = saveKey;
            string jsonData = PlayerPrefs.GetString(saveKey);
            return JsonUtility.FromJson<UserData>(jsonData);
        }
        else
        {
            return null; // 해당 슬롯에 저장된 데이터가 없을 때
        }
    }
    public enum SaveSlot
    {
        Slot1 = 1,  
        Slot2 = 2,
        Slot3 = 3
    }
    public bool SaveDataInSpecificSlot(int slotNum)
    {
        Debug.Log("Save");
        // 슬롯 번호에 따른 savekey 생성
        if(SaveUserData(userData, (SaveSlot)slotNum)!=null)
        {
            Debug.Log("이미 데이터가 있는 슬롯, 그래도 저장한다면 덮어씌어야함.");
            return true;
            //이미 저장되어 있는 userdata 들고오고 화면에 표시

        }
        else
        {
            Debug.Log("별 다른 문제없이 데이터 저장 가능.");
            return false;
            //저장할 데이터 보여줌
        }
    }

    public static UserData LoadedUserData;
    public bool LoadDataInSpecificSlot(int slotNum)
    {
        Debug.Log("Load");
        // 슬롯 n에서 사용자 데이터를 불러오기
        UserData loadedUserData = LoadUserData((SaveSlot)slotNum);
        if (loadedUserData != null)
        {
            Debug.Log("로드가 가능합니다.");
            LoadedUserData = loadedUserData;
            return true;
        }
        else
        {
            Debug.Log("해당 슬롯은 비어있거나 데이터 파일이 손상되었습니다.");
            return false;
        }
    }
    public bool InitializationDataInSpecificSlot(int slotNum)
    {
        Debug.Log("Initialization");

        UserData initializationUserData = InitializationUserData((SaveSlot)slotNum);
        if (initializationUserData != null)
        {
            Debug.Log("초기화가 가능합니다.");
            return true;
        }
        else
        {
            Debug.Log("해당 슬롯은 이미 비어있으므로 초기화할 수 없습니다.");
            return false;
        }
    }
    public void SaveData()
    {
        PlayerPrefs.SetString(SaveKey, JsonData);
        PlayerPrefs.Save(); // 변경 사항을 저장
    }
    public void LoadData()
    {
        userData = LoadedUserData;
    }
    public void InitializationData()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();
    }
    public UserData GetUserData(int id) // 0 - present, 1~3 - in slot
    {
        if(id == 0)
            return userData;
        else if(id >= 1 && id <= 3)
            return LoadUserData((SaveSlot)id);
        else
            return null;
    }
    public GameData GetGameData()
    {
        return gameData;
    }
}
