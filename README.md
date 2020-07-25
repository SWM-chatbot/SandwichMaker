# 샌드위치메이커
2020 [Azure Chatbot Award](https://blog.naver.com/formktmkt/221994807603) 🥪 Team. SandwichMaker 

-------------------------------------------------------

## 서비스 소개
* 챗봇 접속 url https://swm-chatbot.github.io/SandwichMaker/SandwichMaker.html   
### 서비스 흐름도
* 전체 구조
<img src="https://user-images.githubusercontent.com/53828411/88382789-28dc8680-cde4-11ea-9f95-86c74d101b5c.png" title="전체 구조" alt="flow chart 1"></img>
* 샌드위치 만들기 구조   
<img src="https://user-images.githubusercontent.com/47727109/88382949-7d800180-cde4-11ea-8b15-bd9ab8d08115.png" width="30%" title="주문 구조" alt="flow chart 2"></img>

## 시연영상
* v0.1 https://youtu.be/7rXMKE97_oA
* v0.5 https://youtu.be/bTkqM2z_5sU
* v1.0 https://youtu.be/CQRpyrL__ls

## 프로젝트 
Bot Framework v4 core bot based C#
* 프로젝트 코드 설명 

### 사전준비
* [Install](https://visualstudio.microsoft.com/ko/vs/) Visual Studio 2019 including ASP.NET, Azure개발
* [Install](https://marketplace.visualstudio.com/items?itemName=BotBuilder.botbuilderv4) Bot Framework v4 SDK Templates for Visual Studio 
* [Install](https://github.com/microsoft/BotFramework-Emulator) Bot Framework emulator 
* [Get](https://azure.microsoft.com/ko-kr/free/) Azure Trial Account 

### Azure 리소스
* [azure portal](https://portal.azure.com/) 로그인
#### Web App bot 
* 웹 챗봇 리소스 생성
1. 리소스 그룹 만들기
2. 리소스 만들기 > 'Web App Bot' 선택
 
#### Cosmos DB
* Cosmos DB 생성
1. 리소스 만들기 > 'Azure Cosmos DB' 선택
2. 컨테이너 만들기

    A) 생성된 Cosmos DB 리소스 관리창 이동   
    B) '데이터 탐색기' 선택   
    C) 'new Database'로 데이터베이스 생성   
    D) 생성된 데이터베이스에서 'new Container' 생성    
    > 이 프로젝트에서는 Partition Key를 AccountNumber로 사용했습니다
    
   * 참고 [Azure Bot Service 설명 - 스토리지에 직접 작성](https://docs.microsoft.com/ko-kr/azure/bot-service/bot-builder-howto-v4-storage?view=azure-bot-service-4.0&tabs=csharp)
   
#### Text Analytics
* Text Analytics 생성
1. 리소스 만들기 > 'Text Analytics' 선택
        
### 프로젝트 실행하기
* In a terminal, navigate to SWMproject
```
cd SWMproject
```
* Run the bot from a terminal or from Visual Studio, choose option A or B.
   A) From a terminal
   ```dotnet run```
   B) Or from Visual Studio
   * Launch Visual Studio
    * File -> Open -> Project/Solution
    * Navigate to SWMproject folder
    * Select SWMproject.csproj file
    * 'appsettings.json' open > Azure Resource 키 및 엔드포인트 입력
    ```
  "MicrosoftAppId": "{your web app bot id}",
  "MicrosoftAppPassword": "{your web app bot pw}",

  "CosmosDbEndpoint": "{your cosmos db url}",
  "CosmosDbAuthKey": "{your cosmos db key}",
  "CosmosDbDatabaseId": "{your cosmos db name}",
  "CosmosDbContainerId": "{your cosmos db container name}",

  "TextAnalyticsKey": "{your text analytics key}",
  "TextAnalyticsEndpoint": "{your text analytics url}"
    ```
    * Press F5 to run the project

### 봇 테스트
* Testing the bot using Bot Framework Emulator   
*Bot Framework Emulator is a desktop application that allows bot developers to test and debug their bots on localhost or running remotely through a tunnel.

   A) Launch Bot Framework Emulator   
   B) File -> Open Bot   
   C) Enter a Bot URL of http://localhost:3978/api/messages   

### 봇 배포하기
* 참고 [Bot Framework 4.0 개발 가이드 (3) 배포하기|작성자 warit](http://blog.naver.com/warit/221558237007)
    
    
### 참고자료
* [azure bot service설명서](https://docs.microsoft.com/ko-kr/azure/bot-service/?view=azure-bot-service-4.0)
* [Azure Cosmos sql](https://docs.microsoft.com/ko-kr/azure/cosmos-db/sql-api-dotnet-v3sdk-samples)
* [Azure Cosmos](https://github.com/Azure/azure-cosmos-dotnet-v3)
* [Bot Framework 4.0 (2) Hello Bot](http://youngwook.com/221557638246)
* [Bot Framework 4.0 (3) 배포하기](http://youngwook.com/221558237007)
* [Bot Framework 4.0 (4) ActivityHandler 클래스](http://youngwook.com/221559799475)
