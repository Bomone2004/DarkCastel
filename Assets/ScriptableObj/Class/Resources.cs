using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName ="Resouece S0", menuName = "Scriptable Object/Resource", order = 0)]
    public class Resources : ScriptableObject
    {
        public enum ResourceType
        {
            None,
            Gold,
            Trap
        }

        [SerializeField] private bool oneTime = true;
        [SerializeField] ResourceType resourceType = ResourceType.None;
        [SerializeField] private int amout = 0;
        [SerializeField] private string grabMessage;

        public ResourceType Type => resourceType;
        public int Amount => amout;
        public string GrabMessage => grabMessage;
        public bool OneTime => oneTime;
    }
}

