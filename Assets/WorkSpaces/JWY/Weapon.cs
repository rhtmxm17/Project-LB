using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // 변수 목록
    public enum Type { Melee, Range };  //무기타입
    public Type type;
    public int damage;                   // 공격력  
    public float rate;                  // 공격 속도
    public BoxCollider meleeAer;        // 공격 범위
    public TrailRenderer trailEffect;   // 효과변수 생성

    public void Use()
    {
        if(type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
    }

    IEnumerator Swing()
    {
        
        yield return new WaitForSeconds(0.1f);
        meleeAer.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.3f);
        meleeAer.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
        
    }
}
