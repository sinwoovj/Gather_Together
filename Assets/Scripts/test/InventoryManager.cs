using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DI;
using Unity.VisualScripting;

public class InventoryManager : DIMono
{
    [Header("UI")]
    public GameObject imagePrefab;  // 이미지 프리팹
    public InventorySlot[] inventorySlots;  // 인벤토리 슬롯 배열
    public GameObject inventoryItemPrefab;  // 인벤토리 아이템 프리팹
    public GameObject currentContextMenu;  // 아이템 메뉴 on 확인
    public GraphicRaycaster m_Raycaster;  // 레이캐스터
    public EventSystem m_EventSystem;  // 이벤트 시스템

    int selectedSlot = -1;  // 선택된 슬롯 (기본값: -1)
    int value = 0;
    int minValue = 0;
    int maxValue = 9;
    [Inject]
    UserData userData;




    void SettingUserData()
    {
        userData.Inventory.Clear();
        userData.Inventory.Add(new InvenSlot()
        {
            index = 0,
            itemCode = 1,
            count = 1
        });
        userData.Inventory.Add(new InvenSlot()
        {
            index = 1,
            itemCode = 2,
            count = 1
        });
        userData.Inventory.Add(new InvenSlot()
        {
            index = 2,
            itemCode = 3,
            count = 1
        });
        Debug.Log("SettingUserData");
    }

    protected override void Initialize()
    {
        SettingUserData();
        LoadItems();
        //UserData를 바탕으로 인벤토리를 열 때 UI 업데이트
        ChangeSelectedSlot(value);  // 시작 시 첫 번째 슬롯을 선택합니다.


    }

    private void LoadItems()
    {
        Debug.Log("LoadItems " + userData.Inventory.Count);
        foreach (var inven in userData.Inventory)
        {
            GameObject LoadItem = Instantiate(inventoryItemPrefab, inventorySlots[inven.index].transform);
            InventoryItem inventoryitem = LoadItem.GetComponent<InventoryItem>();
            inventoryitem.InitialiseItem(inven);
        }
    }

    private void Update()
    {
        // 마우스 휠을 통한 슬롯 선택 구현
        float mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if(mouseWheelInput != 0)
        {

            if (mouseWheelInput > 0 && value < maxValue)
            {
                ChangeSelectedSlot(++value);
            }
            else if (mouseWheelInput < 0 && value > minValue)
            {
                ChangeSelectedSlot(--value);
            }

        }
        
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
        {
            PointerEventData pointerData = new PointerEventData(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            // 결과가 없거나 또는 결과에 이미지 프리팹이 없으면 이미지를 삭제합니다.
            if (results.Count == 0 || !results.Exists((r) => r.gameObject == currentContextMenu))
            {
                Destroy(currentContextMenu);
                currentContextMenu = null;
            }
        }
    }

    // 클릭 위치에 이미지 표시
    public void ShowImageAtClickPosition(PointerEventData eventData)
    {
        // 이미지 생성 전에 기존 이미지가 있으면 삭제합니다.
        if (currentContextMenu != null)
        {
            Destroy(currentContextMenu);
        }

        // 이미지 프리팹의 원래 크기를 사용하도록 설정합니다.
        RectTransform imagePrefabRect = imagePrefab.GetComponent<RectTransform>();
        Vector2 originalSize = imagePrefabRect.sizeDelta;

        // 캔버스의 Transform을 이미지의 부모로 설정하여 인벤토리 슬롯 위에 이미지가 렌더링되도록 합니다.
        currentContextMenu = Instantiate(imagePrefab, GameObject.Find("Canvas").transform);
        RectTransform rectTransform = currentContextMenu.GetComponent<RectTransform>();

        rectTransform.sizeDelta = originalSize;

        // 클릭한 마우스 위치를 가져옵니다.
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").GetComponent<RectTransform>(), eventData.position, null, out localPoint);
        rectTransform.anchoredPosition = new Vector2(localPoint.x + originalSize.x / 2, localPoint.y + originalSize.y / 2);
    }

    // 선택된 슬롯 변경
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();  // 이전 슬롯의 선택 해제
        }
        inventorySlots[newValue].Select();  // 새로운 슬롯 선택
        selectedSlot = newValue;
    }

    // 아이템 추가
    public bool AddItem(Item item)
    {

        // 동일한 아이템이 있고, 아이템 수가 최대값보다 작다면 아이템 수를 증가
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null &&
                itemInSlot.item == item &&
                itemInSlot.Count < item.maxStackedItems &&
                itemInSlot.item.stackable == true)
            {

                itemInSlot.Count++;
                itemInSlot.RefreshCount();
                return true;
            }
        }

        // 비어있는 슬롯 찾기
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            slot.index = i;
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);  // 새 아이템을 슬롯에 추가합니다.
                return true;
            }
        }

        return false;  // 슬롯에 추가할 수 없으면 false를 반환합니다.
    }

 
    // 새 아이템 생성
    void SpawnNewItem(Item item, InventorySlot slot)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();

        InvenSlot invenSlot = new InvenSlot()
        {
            itemCode=item.id,
            count=1,
            index=slot.index
        };
        userData.Inventory.Add(invenSlot);

        inventoryItem.InitialiseItem(invenSlot);  // 아이템 초기화
    }

    // 선택된 아이템 얻기
    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.Count--;  // 아이템을 사용하면 카운트를 줄입니다.
                if (itemInSlot.Count <= 0)
                {
                    userData.Inventory.Remove(itemInSlot.invenSlot);

                    Destroy(itemInSlot.gameObject);  // 아이템이 더 이상 없으면 아이템을 파괴합니다.
                }
                else
                {
                    itemInSlot.RefreshCount();  // 아이템 수를 업데이트합니다.
                }
            }

            return item;  // 선택된 아이템을 반환합니다.
        }

        return null;  // 선택된 아이템이 없으면 null을 반환합니다.
    }

    // 마지막 슬롯의 아이템 확인 및 제거
    public void CheckAndRemoveItemAtEndSlot()
    {
        int lastSlotIndex = inventorySlots.Length - 1;
        InventorySlot endSlot = inventorySlots[lastSlotIndex];
        InventoryItem itemInEndSlot = endSlot.GetComponentInChildren<InventoryItem>();
        Debug.Log($"Before Count {userData.Inventory.Count}");
        if (itemInEndSlot != null)
        {         
            Destroy(itemInEndSlot.gameObject);  // 마지막 슬롯에 아이템이 있으면 제거합니다.
        }
        foreach (var inven in userData.Inventory)
        {
            Debug.Log($"{inven.index} : {inven.itemCode} : {inven.count}");
        }
    }
}
