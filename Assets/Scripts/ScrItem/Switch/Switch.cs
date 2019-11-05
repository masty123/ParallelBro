using UnityEngine;

public class Switch : IInteractable
{
    [Tooltip("this is for switch animator, not related with the target")]
    public bool isToggle = true;
    // [HideInInspector]
    public bool isOn = false;
    public ITargetable[] Targets;
    private Animator animator;
    public bool isForStepOn = false;
    public GameObject requiredKey;
    public bool isResetSwitch = false;

    public override void Start()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact()
    {
        if (isResetSwitch)
        {
            foreach (ITargetable target in Targets)
            {
                if (target != null)
                {
                    target.Reset();
                }
            }
            return;
        }
        this.turn();
        foreach (ITargetable target in Targets)
        {
            if (target != null)
            {
                target.Turn();
            }
        }
    }

    public override void SelfInteract()
    {
        this.turn();
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
