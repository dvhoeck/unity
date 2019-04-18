using System.Linq;
using UnityEngine;

public class VfxExplodePlayerShip : MonoBehaviour
{
    private Animator[] anim;

    // Start is called before the first frame update
    private void Start()
    {
        anim = GetComponentsInChildren<Animator>();
        anim.ToList().ForEach(animator =>
        {
            animator.updateMode = AnimatorUpdateMode.Normal;
            animator.Play("explode");
        });
    }
}