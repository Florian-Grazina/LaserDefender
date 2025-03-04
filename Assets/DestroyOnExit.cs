using UnityEngine;

public class DestroyOnExit : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindFirstObjectByType<PoolManager>().DestroyDestroyPrefab(animator.gameObject, stateInfo.length);
    }
}
