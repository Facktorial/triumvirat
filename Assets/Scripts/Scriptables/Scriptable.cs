using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Item")]
public class ItemScriptable : ScriptableObject
{
    public float itemSpeed;
    public float stoppingForceMultiplier;
    public int damage;
    public string audioClipName;
    public Transform visuals;

    public enum ItemType
    {
        Breakable,
        NonBreakable
    }
    public ItemType itemType;
}