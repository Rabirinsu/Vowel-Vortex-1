using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WordGame/AllLeters", fileName = "AllLeters")]
public class AllLeters : ScriptableObject
{
    public List<Letter> letters;
}
