using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트의 최하위 클래스 일지 아이템입니다.
/// 충돌 시, 일지가 수집됩니다.
/// </summary>
public class Diary : Collection
{
    // 아이디
    public ItemType ItemID => itemID;
    [SerializeField] ItemType itemID = ItemType.NONE;

#if UNITY_EDITOR
    private void Awake()
    {
        if (itemID == ItemType.NONE)
        {
            Debug.LogWarning("일지 ID 설정 필요");
        }
    }
#endif

    private void Start()
    {
        GameManager.Instance.GetStageSceneManager().InitJournal(this);
    }

}
