using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterModel : MonoBehaviour
{
    [field:SerializeField] public MonsterData DataTable { get; set; }

    [SerializeField] int monsterHp; // 에디터에서 보여주기 용도, 실제 값은 DataTable에서 읽어옴
    [SerializeField] int monsterCurHp;
    private AudioSource audioSource;

    public int MonsterHP { get => DataTable.MaxHp; }
    public int MonsterCurHP { get => monsterCurHp; set { monsterCurHp = value; OnMonsterHPChanged?.Invoke(monsterCurHp); } }
    public int MonsterAP { get { return DataTable.AttackDamage; } }

    public UnityAction<int> OnMonsterHPChanged;

    /// <summary>
    /// 몬스터 초기화시 DataTable 기반으로 수행할 초기화 작업 등록
    /// </summary>
    public UnityAction OnInit;

    /// <summary>
    /// MonsterData 설정 후 초기화를 위해 호출할 함수
    /// </summary>
    public void Init()
    {
        if (DataTable != null)
        {
            // 몬스터 최대HP
            monsterHp = DataTable.MaxHp;
            // 몬스터 현재HP
            monsterCurHp = DataTable.MaxHp;
        }

        OnInit?.Invoke();
    }

    // 테스트코드
    private IEnumerator Start()
    {
        yield return null;

        if (DataTable != null)
            Init();
        else
            Debug.LogWarning("몬스터 데이터가 비어있음");
    }

    private void OnEnable()
    {
        // 몬스터 공격 사운드
        if (DataTable.SpawnClip != null)
        {
            audioSource.PlayOneShot(DataTable.SpawnClip, DataTable.SpawnVolumeScale);
        }
        // 몬스터 사망 사운드        
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();        
    }
}
