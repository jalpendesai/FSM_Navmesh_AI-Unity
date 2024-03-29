﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : StateMachineBehaviour {

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AI ai = animator.gameObject.GetComponent<AI>();
        ai.ChasePlayer();
        Material newMat = Resources.Load("Chase", typeof(Material)) as Material;
        animator.gameObject.GetComponent<Renderer>().material = newMat;
    }

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        AI ai = animator.gameObject.GetComponent<AI>();
        ai.ChasePlayer();
    }

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Material newMat = Resources.Load("Patrol", typeof(Material)) as Material;
        animator.gameObject.GetComponent<Renderer>().material = newMat;
    }

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
	//
	//}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
