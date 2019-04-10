using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VfxExplodePlayerShip : MonoBehaviour
{
    private Animator[] anim;
    private Animator mainAnim;

    // Start is called before the first frame update
    void Start()
    {
        //mainAnim = GetComponent<Animator>();
        //mainAnim.Play("explode");
        anim = GetComponentsInChildren<Animator>();
        anim.ToList().ForEach(animator =>
        {
            var tmp = animator.GetCurrentAnimatorStateInfo(0);
            var tmp2 = animator.runtimeAnimatorController.animationClips[0];
            
            animator.updateMode = AnimatorUpdateMode.Normal;
            animator.Play("explode");
            //animator.Play(tmp);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
