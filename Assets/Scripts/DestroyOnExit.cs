using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DamageAnimationPoolManager.Instance.DestroyDamageAnimationPrefab(animator.gameObject, stateInfo.length);
    }
}
