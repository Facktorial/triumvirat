using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Item")]
public class ItemScriptable : ScriptableObject
{
    public float itemSpeed;
    public float stoppingForceMultiplier;
    public float damage;
    public string audioClipName;

    public enum ItemType
    {
        Breakable,
        NonBreakable
    }
    public ItemType itemType;
}