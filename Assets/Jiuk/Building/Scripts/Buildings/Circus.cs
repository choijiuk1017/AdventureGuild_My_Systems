using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Unit;
using Core.Guild;
using Core.Manager;

namespace Core.Building.Circus
{
    public class Circus : Building
    {
        //서커스 이용 티켓 관련 변수
        public int maxTickets = 3;
        public int currentTickets = 0;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            Init(1);
            DayCycle.OnDayChanged += HandleDayChanged;
        }

        protected override void InitEntity()
        {
            Debug.Log("써커스");
            GuildManager.Instance.AddGuildEntity(GuildEntityType.Circus, this);
        }

        public override void OnInteraction(Adventure adventureEntity)
        {
            Debug.Log("써커스 사용");
            StartCoroutine(UsingCircus(adventureEntity));
        }

        public override void EndInteraction()
        {
            // 엔티티와의 상호작용 종료 시 실행할 로직을 추가
        }

        //티켓을 추가해주는 함수
        //날짜나 특정 이벤트에 따라 추가해줄 때 이용
        void AddRandomTickets()
        {
            int randomTickets = UnityEngine.Random.Range(1, 4); // 1에서 3 사이의 랜덤한 티켓 수
            currentTickets += randomTickets;
            //Debug.Log(randomTickets + "개의 티켓이 서커스에 추가되었습니다.");
        }

        // 서커스 사용 부분
        private IEnumerator UsingCircus(Adventure adventure)
        {
            currentTickets--;
            Debug.Log("써커스 사용 중");
            var delayTime = buildingData.buildingTime;
            yield return new WaitForSeconds(delayTime);

            PerformActionBasedOnBuildingType(adventure, buildingData.buildingType, buildingData.buildingValue);

            Debug.Log("모험가 서커스 퇴장");

            adventureInside = false;
            adventure.AdventureAI.ChangeState(AdventureStateType.Idle);
        }

        // 가격 변동에 따른 이벤트 함수
        private void OnDestroy()
        {
            // 스크립트가 소멸될 때 핸들러 제거
            DayCycle.OnDayChanged -= HandleDayChanged;
        }

        // 가격 변동에 따른 이벤트 함수
        private void HandleDayChanged()
        {
            AddRandomTickets();
        }
    }
}
