using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour, IUseable
{
    // (기획 확인 필요)폭발 대기시간
    [SerializeField] float explosinTimer = 2.5f;

    [SerializeField] GrenadeData data;
    public GrenadeData Data
    {
        get => data;
        set
        {
            data = value;
            invMaxChargeTime = 1f / value.MaxChargeTime;
        }
    }

    [field: SerializeField] public Grenade Prefab { get; set; }

    private float chargeBeginTime;
    private float invMaxChargeTime;

    private void Awake()
    {
        if (Data != null)
        {
            invMaxChargeTime = 1f / Data.MaxChargeTime;
        }
    }

    public void UseBegin()
    {
        chargeBeginTime = Time.time;
    }

    public void UseEnd()
    {
        // 누르고 있던 시간
        float chargedTime = Time.time - chargeBeginTime;
        if (chargedTime > Data.MaxChargeTime)
            chargedTime = Data.MaxChargeTime;

        // 최종 던지는 힘
        float throwForce = Mathf.Lerp(Data.MinThrowForce, Data.MaxThrowForce, chargedTime * invMaxChargeTime);

        Grenade instance = Instantiate(Prefab, this.transform.position, this.transform.rotation);
        instance.Data = this.Data;
        StartCoroutine(instance.ReadyExplosion(explosinTimer));

        instance.GetComponent<Rigidbody>().AddForce(this.transform.forward * throwForce, ForceMode.Impulse);
    }
}
