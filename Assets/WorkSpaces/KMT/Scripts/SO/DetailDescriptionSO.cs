using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Items/DetailInfo/Description")]
public class DetailDescriptionSO : ScriptableObject
{
    [field: SerializeField]
    [field: TextArea]
    public string DetailDescription { get; private set; }

}
