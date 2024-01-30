using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventorySlot : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public Image image;  // 슬롯 이미지
    public Color selectedColor, notSelectedColor;  // 선택 및 비선택 시의 색상


    private InventoryManager inventoryManager;  // 인벤토리 매니저 인스턴스
    public int index;

    private void Awake()
    {
        Deselect();  // 처음에는 선택되지 않은 상태로 초기화
        inventoryManager = FindObjectOfType<InventoryManager>();  // 인벤토리 매니저 인스턴스 찾기
    }

    public void Select()
    {
        image.color = selectedColor;  // 슬롯 선택 시 색상 변경
    }

    public void Deselect()
    {
        image.color = notSelectedColor;  // 슬롯 선택 해제 시 색상 변경
    }

    // Drag and drop 메서드
    public void OnDrop(PointerEventData eventData)
    {
        InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
        // 슬롯이 비어 있으면 드래그된 아이템을 슬롯에 넣음
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

    // 마우스 우클릭 메서드
    public void OnPointerClick(PointerEventData eventData)
    {
        // 우클릭 시
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // 슬롯에 아이템이 있으면
            InventoryItem itemInSlot = GetComponentInChildren<InventoryItem>();
            if (itemInSlot != null)
            {
                // 우클릭한 위치에 이미지 표시
                inventoryManager.ShowImageAtClickPosition(eventData);
                Debug.Log("Image displayed");
            }
        }
    }
}
