using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boss
{
    public class ChargeState : FSMState<BossController>
    {
        public ChargeState(BossController controller) : base(controller)
        {
        }

        public override void OnEnter()
        {
            mController.animator.SetTrigger("Charge");
        }

        public override void OnExit()
        {
        }

        public override void OnUpdate(float deltaTime)
        {
        }
    }

}
