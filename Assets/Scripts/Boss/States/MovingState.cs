using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class MovingState : FSMState<BossController>
    {
        private Vector3 mDirection;

        public MovingState(BossController controller) : base(controller)
        {
            Transitions.Add(new FSMTransition<BossController>(
                isValid : () => {
                    return Vector3.Distance(
                        mController.transform.position,
                        mController.Player.transform.position
                    ) >= mController.WakeDistance;
                },
                getNextState : () => {
                    return new IdleState(mController);
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
            mController.animator.SetBool("IsMoving", true);
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
            var playerPosition = mController.Player.transform.position;
            var bossPosition = mController.transform.position;

            mDirection = (playerPosition - bossPosition).normalized;

            if (mDirection != Vector3.zero)
            {
                mController.animator.SetFloat("Direction", mDirection.x);
            }

            mController.rb.MovePosition(
                mController.transform.position + (mDirection * mController.Speed * deltaTime)
            );
        }
    }

}
