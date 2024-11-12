# ChallenagePractice
내배캠 챌린지반

# Week4 과제 내용
- 게임 소개 : 좌,우,위,아래 패들을 마우스로 움직여 날아오는 공을 받아쳐내서 점수를 획득하는 게임
1. 설정에 따른 event, interface를 활용한 동시 오브젝트 제어
2. 그냥 한번 사용해보고 싶었던 Extension Method
3. 그냥 한번 사용해보고 싶었던 RequireComponent
4. 게임 오브젝트 프리팹 사용하지 않고 모두 동적으로 생성

## 💡Ｔｈｉｎｋ
- '어디까지 동적으로 생성할 수 있을까'에 대한 연구도 해볼만 하다.

# Week5 과제 내용

## Q1
- 전략패턴을 활용하여 다양한 원거리 무기 공격 패턴을 만들어보세요.
  - 직선으로 진행하는 총알 : LinearProjectile
  - 포물선을 그리는 투척물 : ParabolicProjectile
  - 던졌을 때 위로 포물선을 그리며 움직이는 투척물 : ParabolicProjectile
  - 여러 화살을 부채꼴로 발사하는 활 : MultiplyProjectile
  - 지정한 위치를 갔다가 돌아오는 부메랑 : BoomerangProjectile

Projectile abstract class를 만들고, 이를 상속받아 여러가지 투사체 효과를 만들어 보았습니다.

## Q2
- 플레이어가 2개 이상의 스킬을 사용할 수 있고, 해당 스킬을 퀵슬롯에 등록할 수 있다고 할 때, 어떻게 구현해야할까요?
  이런 상황에 써야할 디자인 패턴에 대해 검색해보고, 어떻게 적용할 수 있을지 작성해봅시다.
  검색한 디자인 패턴을 바탕으로 퀵슬롯 기능을 실제로 구현해봅시다.

플레이어가 SkillBook 을 가지고 있고, SkillBook에 SkillCommandBase 를 추가하고 입력키에 따른 skill id를 가지고 SkillBook 의 InvokeExecute 함수를 실행하면 해당 스킬을 사용하도록 설계 하였습니다. 