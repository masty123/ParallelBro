using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAblePlatform : ITargetable
{
    public SpriteRenderer sprite;
    public Collider2D _collider;

    public bool isOn = false;
    public bool IsToggle = true;


    public override void Turn()
    {
        if(isOn && !IsToggle)
        {
            return;
        }

        isOn = !isOn;
        Toggle(this.isOn);
    }

    // Start is called before the first frame update
    void Start()
    {
        Toggle(this.isOn);
    }

    private void Toggle(bool isOn)
    {
        sprite.color = isOn ? Color.white : Color.black;
        _collider.enabled = isOn;
    }
}
