using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Info", menuName = "Data/New Info")]
public class InfoData : ScriptableObject
{
    [SerializeField] private string foodName;
    [TextArea]
    [SerializeField] private string foodDescription;
    [SerializeField] private Sprite foodImage;
}
