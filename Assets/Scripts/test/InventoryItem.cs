using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DI;

public class InventoryItem : DIMono, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    [Header("UI")]
    public Image image;  // 아이템 이미지
    public Text countText;  // 아이템 개수 텍스트

    [HideInInspector] public Item item;  // 아이템
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
    [HideInInspector] public Transform parentAfterDrag;  // 드래그 후 부모


    [Inject]
    GameData gameData;

    public InvenSlot invenSlot;
    /*
    // 아이템 초기화
    public void InitialiseItem(Item newItem)
    {
        item = newItem;
        image.sprite = newItem.Image;  // 아이템 이미지 설정
        RefreshCount();  // 아이템 개수 갱신
    }
    */
    public void InitialiseItem(InvenSlot invenSlot)
    {
        CheckInjection();

        this.invenSlot = invenSlot;
        item = gameData.Item.Find(l=>l.id== invenSlot.itemCode);
   
        image.sprite = item.Image;  // 아이템 이미지 설정
        RefreshCount();  // 아이템 개수 갱신
    }

    // 아이템 개수 갱신
    public void RefreshCount()
    {
        countText.text = Count.ToString();  // 개수를 문자열로 변환
        bool textActive = Count > 1;  // 개수가 1보다 큰지 확인
        countText.gameObject.SetActive(textActive);  // 개수가 1보다 크면 텍스트 활성화
    }

    // 드래그 시작
    public void OnBeginDrag(PointerEventData eventData)
    {
        image.raycastTarget = false;  // 이미지에 대한 레이캐스팅 비활성화
        parentAfterDrag = transform.parent;  // 드래그 후의 부모를 현재 부모로 설정
        transform.SetParent(transform.root);  // 드래그 도중에는 아이템이 최상위 부모를 가짐
    }

    // 드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;  // 아이템의 위치를 마우스 위치로 설정
    }

    // 드래그 종료
    public void OnEndDrag(PointerEventData eventData)
    {
        image.raycastTarget = true;  // 이미지에 대한 레이캐스팅 활성화
        transform.SetParent(parentAfterDrag);  // 드래그 종료 후 부모를 원래의 부모로 복귀
        invenSlot.index = parentAfterDrag.GetComponent<InventorySlot>().index;
        Debug.Log(invenSlot.index);
        // InventoryManager 가져오기
        InventoryManager inventoryManager = FindObjectOfType<InventoryManager>();
        // 마지막 슬롯의 아이템 확인 및 제거
        inventoryManager.CheckAndRemoveItemAtEndSlot();

    }

}