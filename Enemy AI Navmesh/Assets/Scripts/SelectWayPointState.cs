using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWayPointState : StateMachineBehaviour {

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AI ai = animator.gameObject.GetComponent<AI>();
        ai.SetNextPoint();
        ai.AmountOfAmmo = 10;
    }

}
