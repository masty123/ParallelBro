using UnityEngine;

public class Switch : IInteractable
{
    [Tooltip("this is for switch animator, not related with the target")]
    public bool isToggle = true;
    [HideInInspector]
    public bool isOn = false;
    public ITargetable[] Targets;
    private Animator animator;
    public bool isForStepOn = false;
    public GameObject requiredKey;

    public override void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        this.turn();
        foreach (ITargetable target in Targets)
        {
            if (target != null)
            {
                target.Turn();
            }
        }
    }

    private void turn()
    {
        if (isToggle)
        {
            isOn = !isOn;
        }
        else
        {
            if (isOn)
            {
                return;
            }
            isOn = true;
        }
        animator.SetBool("IsOn", isOn);
    }
}
