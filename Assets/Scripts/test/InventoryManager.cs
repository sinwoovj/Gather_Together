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
    public GameObject imagePrefab;  // �̹��� ������
    public InventorySlot[] inventorySlots;  // �κ��丮 ���� �迭
    public GameObject inventoryItemPrefab;  // �κ��丮 ������ ������
    public GameObject currentContextMenu;  // ������ �޴� on Ȯ��
    public GraphicRaycaster m_Raycaster;  // ����ĳ����
    public EventSystem m_EventSystem;  // �̺�Ʈ �ý���

    int selectedSlot = -1;  // ���õ� ���� (�⺻��: -1)
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
        //UserData�� �������� �κ��丮�� �� �� UI ������Ʈ
        ChangeSelectedSlot(value);  // ���� �� ù ��° ������ �����մϴ�.


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
        // ���콺 ���� ���� ���� ���� ����
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

            // ����� ���ų� �Ǵ� ����� �̹��� �������� ������ �̹����� �����մϴ�.
            if (results.Count == 0 || !results.Exists((r) => r.gameObject == currentContextMenu))
            {
                Destroy(currentContextMenu);
                currentContextMenu = null;
            }
        }
    }

    // Ŭ�� ��ġ�� �̹��� ǥ��
    public void ShowImageAtClickPosition(PointerEventData eventData)
    {
        // �̹��� ���� ���� ���� �̹����� ������ �����մϴ�.
        if (currentContextMenu != null)
        {
            Destroy(currentContextMenu);
        }

        // �̹��� �������� ���� ũ�⸦ ����ϵ��� �����մϴ�.
        RectTransform imagePrefabRect = imagePrefab.GetComponent<RectTransform>();
        Vector2 originalSize = imagePrefabRect.sizeDelta;

        // ĵ������ Transform�� �̹����� �θ�� �����Ͽ� �κ��丮 ���� ���� �̹����� �������ǵ��� �մϴ�.
        currentContextMenu = Instantiate(imagePrefab, GameObject.Find("Canvas").transform);
        RectTransform rectTransform = currentContextMenu.GetComponent<RectTransform>();

        rectTransform.sizeDelta = originalSize;

        // Ŭ���� ���콺 ��ġ�� �����ɴϴ�.
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(GameObject.Find("Canvas").GetComponent<RectTransform>(), eventData.position, null, out localPoint);
        rectTransform.anchoredPosition = new Vector2(localPoint.x + originalSize.x / 2, localPoint.y + originalSize.y / 2);
    }

    // ���õ� ���� ����
    void ChangeSelectedSlot(int newValue)
    {
        if (selectedSlot >= 0)
        {
            inventorySlots[selectedSlot].Deselect();  // ���� ������ ���� ����
        }
        inventorySlots[newValue].Select();  // ���ο� ���� ����
        selectedSlot = newValue;
    }

    // ������ �߰�
    public bool AddItem(Item item)
    {

        // ������ �������� �ְ�, ������ ���� �ִ밪���� �۴ٸ� ������ ���� ����
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

        // ����ִ� ���� ã��
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlot slot = inventorySlots[i];
            slot.index = i;
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null)
            {
                SpawnNewItem(item, slot);  // �� �������� ���Կ� �߰��մϴ�.
                return true;
            }
        }

        return false;  // ���Կ� �߰��� �� ������ false�� ��ȯ�մϴ�.
    }

 
    // �� ������ ����
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

        inventoryItem.InitialiseItem(invenSlot);  // ������ �ʱ�ȭ
    }

    // ���õ� ������ ���
    public Item GetSelectedItem(bool use)
    {
        InventorySlot slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.Count--;  // �������� ����ϸ� ī��Ʈ�� ���Դϴ�.
                if (itemInSlot.Count <= 0)
                {
                    userData.Inventory.Remove(itemInSlot.invenSlot);

                    Destroy(itemInSlot.gameObject);  // �������� �� �̻� ������ �������� �ı��մϴ�.
                }
                else
                {
                    itemInSlot.RefreshCount();  // ������ ���� ������Ʈ�մϴ�.
                }
            }

            return item;  // ���õ� �������� ��ȯ�մϴ�.
        }

        return null;  // ���õ� �������� ������ null�� ��ȯ�մϴ�.
    }

    // ������ ������ ������ Ȯ�� �� ����
    public void CheckAndRemoveItemAtEndSlot()
    {
        int lastSlotIndex = inventorySlots.Length - 1;
        InventorySlot endSlot = inventorySlots[lastSlotIndex];
        InventoryItem itemInEndSlot = endSlot.GetComponentInChildren<InventoryItem>();
        Debug.Log($"Before Count {userData.Inventory.Count}");
        if (itemInEndSlot != null)
        {         
            Destroy(itemInEndSlot.gameObject);  // ������ ���Կ� �������� ������ �����մϴ�.
        }
        foreach (var inven in userData.Inventory)
        {
            Debug.Log($"{inven.index} : {inven.itemCode} : {inven.count}");
        }
    }
}
