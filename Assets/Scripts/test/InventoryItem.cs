using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DI;

public class InventoryItem : DIMono, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;  // ������ �̹���
    public Text countText;  // ������ ���� �ؽ�Ʈ

    [HideInInspector] public Item item;  // ������
    [HideInInspector] public int Count {
        get {
            if (invenSlot == null)
            {
                return 0;
            }
            return invenSlot.count;
        }
        set { invenSlot.count = value; }
    }
    [HideInInspector] public Transform parentAfterDrag;  // �巡�� �� �θ�


    [Inject]
    GameData gameData;

    public InvenSlot invenSlot;
    /*
    // ������ �ʱ�ȭ
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.Image;  // ������ �̹��� ����
        RefreshCount();  // ������ ���� ����
    }
    */
    public void InitialiseItem(InvenSlot invenSlot)
    {
        CheckInjection();

        this.invenSlot = invenSlot;
        item = gameData.Item.Find(l=>l.id== invenSlot.itemCode);
   
        image.sprite = item.Image;  // ������ �̹��� ����
        RefreshCount();  // ������ ���� ����
    }

    // ������ ���� ����
    public void RefreshCount()
    {
        countText.text = Count.ToString();  // ������ ���ڿ��� ��ȯ
        bool textActive = Count > 1;  // ������ 1���� ū�� Ȯ��
        countText.gameObject.SetActive(textActive);  // ������ 1���� ũ�� �ؽ�Ʈ Ȱ��ȭ
    }

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;  // �̹����� ���� ����ĳ���� ��Ȱ��ȭ
        parentAfterDrag = transform.parent;  // �巡�� ���� �θ� ���� �θ�� ����
        transform.SetParent(transform.root);  // �巡�� ���߿��� �������� �ֻ��� �θ� ����
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;  // �������� ��ġ�� ���콺 ��ġ�� ����
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;  // �̹����� ���� ����ĳ���� Ȱ��ȭ
        transform.SetParent(parentAfterDrag);  // �巡�� ���� �� �θ� ������ �θ�� ����
        invenSlot.index = parentAfterDrag.GetComponent<InventorySlot>().index;
        Debug.Log(invenSlot.index);
        // InventoryManager ��������
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        // ������ ������ ������ Ȯ�� �� ����
        inventoryManager.CheckAndRemoveItemAtEndSlot();

    }

}