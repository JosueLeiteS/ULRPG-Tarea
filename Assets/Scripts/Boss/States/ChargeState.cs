using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class ChargeState : FSMState<BossController>
    {
        public ChargeState(BossController controller) : base(controller)
        {
            Transitions.Add(new FSMTransition<BossController>(
                isValid : () => {
                    return mController.AttackingEnd;
                },
                getNextState : () => {
                    return new IdleState(mController);
                }
            ));
        }

        public override void OnEnter()
        {
            mController.animator.SetTrigger("Charge");
            mController.hitBox.gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            mController.hitBox.gameObject.SetActive(false);
            mController.ChargeReady = false;
        }

        public override void OnUpdate(float deltaTime)
        {
        }
    }

}
