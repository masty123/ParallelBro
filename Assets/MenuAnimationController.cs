using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAnimationController : MonoBehaviour
{
    private Animator boyAnimator;
    private Animator girlAnimator;
    
    private int actualTime;
    private int timeBeforeBoyGlitch;
    private int timeBeforeGirlGlitch;


    // Start is called before the first frame update
    void Start()
    {
        boyAnimator = transform.Find("Boy").gameObject.GetComponent<Animator>();
        girlAnimator = transform.Find("Girl").gameObject.GetComponent<Animator>();

        timeBeforeBoyGlitch = RandomTime();
        timeBeforeGirlGlitch = RandomTime();
    }

    // Update is called once per frame
    void Update()
    {
        actualTime = Mathf.RoundToInt(Time.time);
        //Debug.Log(timeBeforeBoyGlitch+" "+actualTime);
        if (actualTime == timeBeforeBoyGlitch)
        {
            boyAnimator.SetTrigger("GlitchTrigger");
            timeBeforeBoyGlitch = actualTime + RandomTime();
        }
        if (actualTime == timeBeforeGirlGlitch)
        {
            girlAnimator.SetTrigger("GlitchTrigger");
            timeBeforeGirlGlitch = actualTime + RandomTime();
        }
    }

    private int RandomTime()
    {
        return Random.Range(4, 6);
    }
}
