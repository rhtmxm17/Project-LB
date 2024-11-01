using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Npc/Dialogue")]
public class DialogueSO : ScriptableObject
{
    [field: SerializeField]
    [field: TextArea(5,15)]
    public string[] Texts {  get; private set; }

}
