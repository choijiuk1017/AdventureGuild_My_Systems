using Core.Unit.FSM;
using UnityEngine;
using Utility.Algorithm;
using System.Collections.Generic;
using System.Collections;

namespace Core.Unit.State.Adventure
{
    public class MoveState : State<AdventureAI>
    {
        [SerializeField]
        private GameObject targetObject;

        private const string animationName = "walk";

        private bool isRight = false;

        private bool isMove = false;

        private PathFinding pathFinding;

        protected override void Awake()
        {
            base.Awake();
            pathFinding = GetComponent<PathFinding>();
        }

        public override void Enter(AdventureAI entity)
        {
            Debug.Log("이동 중");
            targetObject = entity.targetObject;

            entity.unitAnimation.SetAnimation(animationName);

            isMove = true;
            //pathFinding.StartFindPath(this.transform.position, targetObject.transform);
        }

        public override void Execute(AdventureAI entity)
        {
            


            if ((transform.position - targetObject.transform.position).magnitude < 1f)
            {
                Debug.Log("목적지 도착");
                entity.ChangeState(AdventureStateType.Interaction);

                
            }

            var x = targetObject.transform.position.x > transform.position.x ? 1 : -1;

            if (isRight)
            {
                if (x == -1)
                    Flip();
            }
            else
            {
                if (x == 1)
                    Flip();
            }

            if(isMove)
            {
                pathFinding.StartFindPathToWorldPosition(this.transform.position, targetObject.transform.position);

                isMove = false;
            }

            
            //transform.position += new Vector3(x, 0, 0) * Time.deltaTime;
        }

        public override void Exit(AdventureAI entity)
        {

        }

        public override void OnTransition(AdventureAI entity)
        {

        }

        private void Flip()
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
            isRight = !isRight;
        }
    }
}