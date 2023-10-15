using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image image;  // ���� �̹���
    public Color selectedColor, notSelectedColor;  // ���� �� ���� ���� ����


    private InventoryManager inventoryManager;  // �κ��丮 �Ŵ��� �ν��Ͻ�
    public int index;

    private void Awake()
    {
        Deselect();  // ó������ ���õ��� ���� ���·� �ʱ�ȭ
        inventoryManager = FindObjectOfType<InventoryManager>();  // �κ��丮 �Ŵ��� �ν��Ͻ� ã��
    }

    public void Select()
    {
        image.color = selectedColor;  // ���� ���� �� ���� ����
    }

    public void Deselect()
    {
        image.color = notSelectedColor;  // ���� ���� ���� �� ���� ����
    }

    // Drag and drop �޼���
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        // ������ ��� ������ �巡�׵� �������� ���Կ� ����
        if (transform.childCount != 0)
        {
            InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();

            InvenSlot slot1 = itemInSlot.invenSlot;
            InvenSlot tempInvenSlot = inventoryItem.invenSlot;
            
            var fromIdx= inventoryItem.invenSlot.index;
            var fromSlot=inventoryManager.inventorySlots[fromIdx];
            itemInSlot.transform.SetParent(fromSlot.transform);
            itemInSlot.invenSlot.index = fromIdx;
          

        }
        inventoryItem.parentAfterDrag = transform;
    }

    // ���콺 ��Ŭ�� �޼���
    public void OnPointerClick(PointerEventData eventData)
    {
        // ��Ŭ�� ��
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // ���Կ� �������� ������
            InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                // ��Ŭ���� ��ġ�� �̹��� ǥ��
                inventoryManager.ShowImageAtClickPosition(eventData);
                Debug.Log("Image displayed");
            }
        }
    }
}
