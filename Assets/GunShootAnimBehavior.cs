using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunShootAnimBehavior : StateMachineBehaviour
{
	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//if (!animator.GetBool("isFire"))
		//	return;

		//Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
		//if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
		//{
		//	animator.SetBool("isFire", false);

		//	if (animator.GetBool("isMoving"))
		//		animator.Play("Move"); //, 0.1f);
		//	else
		//		animator.Play("Idle"); //, 0.1f);
		//}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//animator.SetFloat("normTime", 1f);
	}



	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    
	//}



	// OnStateMove is called right after Animator.OnAnimatorMove()
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that processes and affects root motion
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK()
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	//{
	//    // Implement code that sets up animation IK (inverse kinematics)
	//}
}
