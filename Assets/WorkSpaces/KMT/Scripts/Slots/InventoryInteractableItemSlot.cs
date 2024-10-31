using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(ClickCallback))]
public class InventoryInteractableItemSlot : InventoryItemSlot
{

    HotKeySystem hotKeySystem;

    ClickCallback clickCallback;

    RectTransform infoPanelRect;
    InfoPanel infoPanel;
    RectTransform detailInfoPanelRect;
    InfoPanel detailInfoPanel;

    PlayerData playerData;

    protected override void Awake()
    {
        base.Awake();

        clickCallback = GetComponent<ClickCallback>();
        clickCallback.ClickEvent.AddListener(OnClickEvent);
        clickCallback.DoubleClickEvent.AddListener(OnDoubleClickEvent);
        clickCallback.DragEvent.AddListener(OnDragEvent);
        clickCallback.EnterEvent.AddListener(OnEnterEvent);
        clickCallback.ExitEvent.AddListener(OnExitEvent);

        hotKeySystem = GameObject.FindWithTag("QuickSlot").GetComponent<HotKeySystem>();

        infoPanelRect = GameObject.Find("InfoWindow").GetComponent<RectTransform>();
        infoPanel = infoPanelRect.GetComponent<InfoPanel>();

        detailInfoPanelRect = GameObject.Find("PopupWindow").GetComponent<RectTransform>();
        detailInfoPanel = detailInfoPanelRect.GetComponent <InfoPanel>();

        playerData = GameManager.Instance.GetPlayerData();

    }

    void OnClickEvent(PointerEventData eventData)
    {

        if (Item is InventoryInfoItemSO)
        {

            DetailDescriptionSO detail = ((InventoryInfoItemSO)Item).DetailInfoSO;

            if (detail != null)
            {
                //팝업.
                detailInfoPanelRect.gameObject.SetActive(true);
                detailInfoPanelRect.anchoredPosition = Vector3.zero;
                detailInfoPanel.ClearAllText();
                detailInfoPanel.SetDescriptionText(detail.DetailDescription);

            }

        }

        Debug.Log("click");
    }

    void OnDoubleClickEvent(PointerEventData eventData)
    {
        if (Item is InventoryEquableItemSO)
        {
            Debug.Log("장착이 가능(더블클릭 대응 이벤트)한 아이템입니다.");
            hotKeySystem.EquipItem(this);
        }
        Debug.Log("doubleClick");
    }

    void OnDragEvent(PointerEventData eventData)
    {
        //todo? : 확장성 여지.
        //Debug.Log("draging");
    }

    void OnEnterEvent(PointerEventData eventData)
    {
        if (Item is InventoryInfoItemSO)
        {
            infoPanelRect.gameObject.SetActive(true);

            infoPanelRect.position = transform.position;
            infoPanelRect.transform.Translate(Vector3.left * infoPanelRect.rect.width * infoPanelRect.lossyScale.x / 2);
            infoPanelRect.transform.Translate(Vector3.left * infoPanelRect.rect.width * infoPanelRect.lossyScale.x / 10);

            infoPanel.ClearAllText();

            infoPanel.SetDescriptionText(((InventoryInfoItemSO)Item).Description);

            if (Item is InventoryEquableItemSO)
            {
                ItemData data = playerData.GetItemData(Item.ItemType);
                InventoryEquableItemSO eqItem = (InventoryEquableItemSO)Item;

                if (data == null)
                {
                    Debug.LogWarning("플레이어가 해당 아이템을 가지고 있지 않음");
                }
                else
                {
                    //todo : 고정적인 공격 상승치가 아니라면, SO에 상승치 배열을 추가. 
                    //또는 레벨별 공격력 작성 -> 현재공력 - 기본공격력
                    //2중1택
                    infoPanel.SetTitleText(Item.ItemName, data.WeaponLevel);//0강?
                    infoPanel.SetLowerText(eqItem.AttackPoint, data.WeaponLevel * eqItem.AdditiveAttackPoint);
                }
            }
            else
            {
                infoPanel.SetTitleText(Item.ItemName);
            }

        }
    }

    void OnExitEvent(PointerEventData eventData)
    {
        if (Item is InventoryInfoItemSO)
        {
            infoPanelRect.gameObject.SetActive(false);
        }
    }

}

