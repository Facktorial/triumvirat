using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private Transform Visuals;

    private Movement Movement;
    public Animator animator;

    private void Start()
    {
        Movement = GetComponent<Movement>();
    }

    private void Update()
    {
        Vector3 targetDirection = new Vector3(
            Mathf.Lerp(Visuals.forward.x, Movement.GetDirection().x, Time.deltaTime * 33f),
            Mathf.Lerp(Visuals.forward.y, Movement.GetDirection().y, Time.deltaTime * 33f),
            Mathf.Lerp(Visuals.forward.z, Movement.GetDirection().z, Time.deltaTime * 33f))
            + Visuals.position + new Vector3 (0.001f, 0, 0.001f);
        Visuals.LookAt(targetDirection);

        if (Movement.moving)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    public void PlayThrow()
    {
        animator.SetTrigger("throw");
    }

    public void PlayDie()
    {
        animator.SetTrigger("die");
    }

    public void PlayRewive()
    { 
        animator.SetTrigger("rewive");
    }

    public void PlayPickUp()
    {
        animator.SetTrigger("pickUp");
    }
}
