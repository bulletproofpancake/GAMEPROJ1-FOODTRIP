using UnityEngine;

namespace Customers.Dialogue
{
    [CreateAssetMenu(fileName = "Dialogue", menuName = "Data/New Dialogue")]
    public class DialogueData : ScriptableObject
    {
        [SerializeField] private DialogueInfo[] info;
        public DialogueInfo[] Info => info;
    }

    [System.Serializable]
    public class DialogueInfo
    {
        public Order order;
        public Sprite sprite;
        public string speakerName;
        [TextArea]
        public string text;
    }
}