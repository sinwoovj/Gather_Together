using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image image;  // ���� �̹���
    public Color selectedColor, notSelectedColor;  // ���� �� ���� ���� ����

    private InventoryManager inventoryManager;  // �κ��丮 �Ŵ��� �ν��Ͻ�

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
        // ������ ��� ������ �巡�׵� �������� ���Կ� ����
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
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
