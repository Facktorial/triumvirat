using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] Material sellectedMaterial;
    [SerializeField] Material baseMaterial;
    MeshRenderer mesh;
    ItemHolder holder;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        holder = GetComponent<ItemHolder>();

        mesh.material = baseMaterial;
    }

    public void Sellect()
    {
        mesh.material = sellectedMaterial;
    }

    public void Desellect()
    {
        mesh.material = baseMaterial;
    }

    public ItemScriptable GetItem()
    {
        return holder.item;
    }
}
