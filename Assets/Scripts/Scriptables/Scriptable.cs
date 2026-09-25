using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Item")]
public class ItemScriptable : ScriptableObject
{
    public float itemSpeed;
    public float movementSpeed;
    public float damage;

    public enum ItemType
    {
        Breakable,
        NonBreakable
    }
    public ItemType itemType;
}