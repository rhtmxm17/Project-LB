using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 플레이어가 퀵슬롯에 장착 및 사용 가능한 아이템
/// </summary>
public interface IUseable
{
    public GameObject gameObject { get; }

    /// <summary>
    /// 플레이어가 '사용' 입력을 시작할 때 호출됩니다.
    /// 즉시 사용되는 소모품, 총기 발사 시작 등
    /// </summary>
    public void UseBegin();

    /// <summary>
    /// 플레이어가 '사용' 입력을 종료할 때 호출됩니다.
    /// 총기 발사 종료 등
    /// </summary>
    public void UseEnd();

    /// <summary>
    /// 화면에 아이템을 드러내고 숨기는 애니메이션을 재생합니다
    /// </summary>
    /// <param name="show">드러낸다면 true</param>
    public void ShowAnimation(bool show);
}
