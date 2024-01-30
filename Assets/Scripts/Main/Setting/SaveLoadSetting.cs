using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SaveLoadSetting : MonoBehaviour
{
    public SaveLoadManager saveLoadManager;
    public GameObject slotSelectPanel;
    public GameObject DataPanel;
    public GameObject DataElement;
    public Text SlotTitleText;
    public Text DataTitleText;
    public Text DataElementText;
    public Text DataButtonText;


    void Start()
    {
        slotSelectPanel.SetActive(false);
        DataPanel.SetActive(false);
    }

    // Save일 시 1, Load일 시 2, Initialization일 시 3
    public static int saveload = 0;

    public void DataProcessButtons(int saveLoad) // 1-save, 2-load, 3-initialization
    {
        slotSelectPanel.SetActive(true);
        DataTitleText.transform.position = new Vector3(0, 255);
        saveload = saveLoad;
        switch (saveLoad)
        {
            case 1:
                SlotTitleText.text = "몇 번 슬롯에 데이터를 저장하시겠습니까?";
                break;
            case 2:
                SlotTitleText.text = "몇 번 슬롯의 데이터를 불러오시겠습니까?";
                break;
            case 3:
                SlotTitleText.text = "몇 번 슬롯의 데이터를 초기화하시겠습니까?";
                break;
        }
    }

    public void SlotSelectPanelCancel()
    {
        slotSelectPanel.SetActive(false);
        saveload = 0;
    }
    public void LoadDataPanelCancel()
    {
        DataPanel.SetActive(false);
        SlotSelectPanelCancel();
    }

    public void SelectSlot(int slotNum)
    {
        Debug.Log("saveload:"+saveload+"//slotNum:"+slotNum);
        UserData preData = saveLoadManager.GetUserData(0);
        UserData slotData = saveLoadManager.GetUserData(slotNum);
        if (saveload == 1) // Save
        {
            // 현재까지의 데이터를 n Slot에 저장합니다 텍스트 그 아래 데이터
            // 만약 덮어쓰는 경우 해당 데이터를 덮어씌우시겠습니까? 문구와 함께 덮어씌어질 데이터 보여줌 이후 저장될 데이터를 한번더 보여줌.
            // 이후 저장

            if (saveLoadManager.SaveDataInSpecificSlot(slotNum))
            {
                // 덮어 씌여질 userData를 띄우고 불러오시겠습니까 물어봄
                DataButtonText.text = "덮어쓰기";
                DataElement.SetActive(true);
                DataTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 255);
                DataTitleText.text = $"{slotNum}번 슬롯에 현재 데이터를 덮어씌우시겠습니까? (아래에 표시된 데이터는 기존 데이터로 덮어씌울 시 영구적으로 삭제됩니다!!!)";
                SetDataElementTextFunc(slotData);
            }
            else
            {
                DataButtonText.text = "확인";
                DataElement.SetActive(true);
                DataTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 255);
                DataTitleText.text = "해당 슬롯에 현재 데이터를 저장하시겠습니까? (아래에 표시된 데이터는 현재 데이터 입니다.)";
                SetDataElementTextFunc(preData);
            }
        }
        else if (saveload == 2) // Load
        {
            if (saveLoadManager.LoadDataInSpecificSlot(slotNum))
            {
                // 해당 userData를 띄우고 불러오시겠습니까 물어봄
                DataButtonText.text = "불러오기";
                DataElement.SetActive(true);
                DataTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 255);
                DataTitleText.text = $"{slotNum}번 슬롯의 데이터를 불러오시겠습니까?";
                SetDataElementTextFunc(slotData);

            }
            else
            {
                saveload = 0;
                DataButtonText.text = "확인";
                DataElement.SetActive(false);
                DataTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                DataTitleText.text = "해당 슬롯은 비어있거나 데이터 파일이 손상되었습니다.";
            }
        }
        else if (saveload == 3) // Intialization
        {
            if (saveLoadManager.InitializationDataInSpecificSlot(slotNum))
            {
                // 아니라면 내용 보여주고 초기화할 건지 물어봄.
                DataButtonText.text = "초기화";
                DataElement.SetActive(true);
                DataTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 255);
                DataTitleText.text = $"{slotNum}번 슬롯의 데이터를 초기화하시겠습니까?";
                SetDataElementTextFunc(slotData);
            }
            else
            {
                saveload = 0;
                DataButtonText.text = "확인";
                DataElement.SetActive(false);
                DataTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                DataTitleText.text = "해당 슬롯은 이미 비어있으므로 초기화할 수 없습니다.";
            }
        }
        else
        {
            Debug.LogError("saveload 오류");
        }
        // 패널 띄우기
        DataPanel.SetActive(true);
    }
    public void DataProcessFucn()
    {
        DataElement.SetActive(false);
        DataTitleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        switch (saveload)
        {
            case 0:
                DataPanel.SetActive(false); 
                break;
            case 1:
                saveLoadManager.SaveData();
                DataTitleText.text = "성공적으로 저장되었습니다.";
                break;
            case 2:
                saveLoadManager.LoadData();
                DataTitleText.text = "성공적으로 로드되었습니다.";
                break;
            case 3:
                saveLoadManager.InitializationData();
                DataTitleText.text = "성공적으로 초기화되었습니다.";
                break;
        }
        saveload = 0;
    }
    public void SetDataElementTextFunc(UserData ud)
    {
        Debug.Log(saveLoadManager.GetGameData().ToString() + ( saveLoadManager.GetGameData() == null));
        DataElementText.text = $"{ud.campsiteName} 캠핑장\n" +
            $"{ud.processivity} %\n" +
            $"{saveLoadManager.GetGameData().MainQuest.FirstOrDefault(l => l.MainQuestId == ud.mainQuestId).MainQuestName}\n" +
            $"{saveLoadManager.GetGameData().SubQuest.FirstOrDefault(l => l.SubQuestId == ud.subQuestId).SubQuestName}\n" +
            $"{ud.money} Rub\n{ud.startedTime.ToString("yyyy'년' MM'월' dd'일' h'시' m'분'")}";
    }
}
