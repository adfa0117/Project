using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stage", menuName = "Stage", order = 2)]
public class Stage : ScriptableObject
{
    [SerializeField]
    public float[] delays;

    [SerializeField]
    public char[] texts;
}
