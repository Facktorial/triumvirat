using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private Transform Visuals;

    private Movement Movement;

    private void Start()
    {
        Movement = GetComponent<Movement>();
    }

    private void Update()
    {
        Vector3 targetDirection = new Vector3 (
            Mathf.Lerp(Movement.GetDirection().x, Visuals.forward.x, Time.deltaTime * 0.01f),
            Mathf.Lerp(Movement.GetDirection().y, Visuals.forward.y, Time.deltaTime * 0.01f),
            Mathf.Lerp(Movement.GetDirection().z, Visuals.forward.z, Time.deltaTime * 0.01f))
            + Visuals.position;
        Visuals.LookAt (targetDirection);
    }
}
