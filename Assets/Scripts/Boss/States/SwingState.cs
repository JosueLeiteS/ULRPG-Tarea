using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class SwingState : FSMState<BossController>
    {
        public SwingState(BossController controller) : base(controller)
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
            mController.animator.SetTrigger("Swing");
            mController.hitBox.gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            mController.hitBox.gameObject.SetActive(false);
            mController.StabBool = true;
            mController.AttackCounter += 1;
        }

        public override void OnUpdate(float deltaTime)
        {
        }
    }
}
