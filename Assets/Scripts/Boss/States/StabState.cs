using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class StabState : FSMState<BossController>
    {
        public StabState(BossController controller) : base(controller)
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
            mController.animator.SetTrigger("Stab");
            mController.hitBox.gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            mController.hitBox.gameObject.SetActive(false);
            mController.StabBool = false;
            mController.AttackCounter += 1;
        }

        public override void OnUpdate(float deltaTime)
        {
        }
    }
}
