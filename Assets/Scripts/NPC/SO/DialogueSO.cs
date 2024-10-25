using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Npc/Dialogue")]
public class DialogueSO : ScriptableObject
{

    [field: SerializeField]
    public string[] Texts {  get; private set; }

}
