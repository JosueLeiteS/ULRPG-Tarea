using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class IdleState : FSMState<BossController>
    {
        public IdleState(BossController controller) : base(controller)
        {
            Transitions.Add(new FSMTransition<BossController>(
                isValid : () => {
                    return Vector3.Distance(
                        mController.transform.position,
                        mController.Player.transform.position
                    ) < mController.WakeDistance;
                },
                getNextState : () => {
                    return new MovingState(mController);
                }
            ));

            Transitions.Add(new FSMTransition<BossController>(
                isValid : () => {
                    return 
                    (
                        Vector3.Distance(
                        mController.transform.position,
                        mController.Player.transform.position
                    ) <= mController.SwingDistance
                    ) && (
                        mController.StabBool == false
                    ) && (
                        mController.ChargeReady == false
                    );
                },
                getNextState : () => {
                    return new SwingState(mController);
                }
            ));

            Transitions.Add(new FSMTransition<BossController>(
                isValid : () => {
                    return 
                    (
                        Vector3.Distance(
                        mController.transform.position,
                        mController.Player.transform.position
                    ) <= mController.StabDistance
                    )  && (
                        mController.StabBool == true
                    ) && (
                        mController.ChargeReady == false
                    );
                },
                getNextState : () => {
                    return new StabState(mController);
                }
            ));

            Transitions.Add(new FSMTransition<BossController>(
                isValid : () => {
                    return 
                    (
                        Vector3.Distance(
                        mController.transform.position,
                        mController.Player.transform.position
                    ) <= mController.ChargeDistance
                    )  && (
                        mController.ChargeReady == true
                    );
                },
                getNextState : () => {
                    return new ChargeState(mController);
                }
            ));

        }
        public override void OnEnter()
        {
            mController.animator.SetBool("IsMoving", false);
            mController.AttackingEnd = false;
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
        }
    }

}
