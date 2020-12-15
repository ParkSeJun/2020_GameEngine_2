# 2020_GameEngine_2

게임공학과 2015182012 박세준      
게임공학과 2015180004 김성은      

***    

# 1. 게임 개요   
### 기획 컨셉   
    수업에서 배운 유니티 기능들을 잘 활용하여 간단한 게임을 구현한다.
### 장르  
    캐주얼 FPS
### 이용 가능 연령  
    전 연령  
### 게임 플레이 방법  
    주어진 무기를 이용해 몬스터를 물리치거나 피해서 목적지까지 이동
### 조작 방법  
* A, W, S, D : 이동  
* SPACE : 점프
* 마우스 우클릭 : 줌(조준)
* 마우스 좌클릭 : 공격
* 1,2,3 : 총 종류 변경


***   

# 2. 개발에 사용한 외부 라이브러리 리스트 및 소개   
* monster model - asset store
    - https://assetstore.unity.com/packages/3d/characters/creatures/level-1-monster-pack-77703
    
* gun model - asset store
    - https://assetstore.unity.com/packages/3d/props/guns/bit-gun-22922
    
* skybox - https://assetstore.unity.com/packages/2d/textures-materials/sky/sky5x-one-6332

* bg sound -    https://bgmstore.net/view/5bb0d0e3352039d227086160/[%EA%B2%8C%EC%9E%84]%20Cloud%20Wars%20%EB%B0%B0%EA%B2%BD%EC%9D%8C%EC%95%85%20(%EA%B2%8C%EC%9E%84,%EB%8F%99%EC%8B%AC,%ED%8F%89%ED%99%94,%EC%88%9C%EC%88%98)

   
***   

# 3. 자체 제작한 에셋 리스트 및 소개  
* 참고한 이미지     
    - <https://assetstore.unity.com/packages/3d/environments/speedtutor-tutorial-scene-free-159460/>  
<div>
<img width = "300" src="https://user-images.githubusercontent.com/22375492/96914484-068bff00-14e0-11eb-9a50-df893ebf7a36.png">
</div>

![stage](https://user-images.githubusercontent.com/22375492/102170771-70b99280-3ed8-11eb-880d-1fc5ce4e3408.PNG)
![image](https://user-images.githubusercontent.com/22375492/102170933-ca21c180-3ed8-11eb-8b0b-4feb813bf84d.png)

***  

# 4. 개발 내용 중 강의를 통해 학습한 부분이 어떻게 활용 되었는지에 대한 설명  
* Light setting - spot light, point light, emission, fog
* Script - 게임플레이 스크립트  
* UI - UGUI
* Particle - 레이저 발사, 횃불, 포탈   
* Collider - mesh collider, bdx collider, capsul collider 등 오브젝트마다 적절하게 사용   
* Rigidbody - 캐릭터, 몹
* Cinemachine - 줌
* Raycast - 충돌 
    
***    

# 5. 그 외 학습한 부분     
* audio source을 통해  배경음악과 효과음을 추가   
* 게임에 필요한 스크립트 제작  
* 몬스터 UI
    
***    

# 6. 아쉬운 점  
    이펙트를 Shader Graph를 사용해서 만드려고 했는데, 프로젝트를 universal RP로 변경하면 기존에 만든 머티리얼들에 문제가 생김
    몬스터 스폰에 문제가 있어서 종류별로 배치하지 못함
    맵도 지하 3층부터 1층까지 3개를 제작했는데 버그가 있어서 지하 3층만 사용함
    UI나 마지막 맵(지하1층)의 포탈에 도달하면 timeline으로 만든 영상이 있는데, 시간관계상 넣지 못함
    여러가지 효과를 넣고 싶은게 많았는데 유니티 기능에 미숙하여 구현하지 못함
    
    
    
***   

# 감사합니다.
    
    
