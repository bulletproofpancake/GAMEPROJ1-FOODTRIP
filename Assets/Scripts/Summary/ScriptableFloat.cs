using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableFloat", menuName = "Data/New Float")]
public class ScriptableFloat : ScriptableObject
{
    [SerializeField] private float value;

    public float Value
    {
        get => value;
        set => this.value = value;
    }
}