using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUseable
{
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
}
