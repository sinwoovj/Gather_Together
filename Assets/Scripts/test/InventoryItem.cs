using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;  // ������ �̹���
    public Text countText;  // ������ ���� �ؽ�Ʈ

    [HideInInspector] public Item item;  // ������
    [HideInInspector] public int count = 1;  // ������ �� (�⺻��: 1)
    [HideInInspector] public Transform parentAfterDrag;  // �巡�� �� �θ�

    // ������ �ʱ�ȭ
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.Image;  // ������ �̹��� ����
        RefreshCount();  // ������ ���� ����
    }

    // ������ ���� ����
    public void RefreshCount()
    {
        countText.text = count.ToString();  // ������ ���ڿ��� ��ȯ
        bool textActive = count > 1;  // ������ 1���� ū�� Ȯ��
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

        // InventoryManager ��������
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        // ������ ������ ������ Ȯ�� �� ����
        inventoryManager.CheckAndRemoveItemAtEndSlot();
    }

}