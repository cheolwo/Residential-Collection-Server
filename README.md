# Configuration Settings

Below you will find a basic example of the necessary `appsettings.json` configuration for this project. Please make sure to adjust the settings according to your local environment and security requirements.

## Basic Configuration

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "공동주택": "Server=localhost\\SQLEXPRESS01;Database=공동주택Db;Trusted_Connection=True;"
  },
   "공공데이터ServiceKey": "Your Service Key"
}

```
## Asp.net Core Service Container
```ServiceContainer
builder.Services.AddHttpClient();
builder.Services.AddDbContext<공동주택DbContext>(options =>
    options.UseInMemoryDatabase("공동주택Db"));
// Registering AutoMapper
builder.Services.AddAutoMapper(
    typeof(공동주택목록MappingProfile),

    typeof(가스사용료MappingProfile),
    typeof(건물보험료MappingProfile),
    typeof(급탕비MappingProfile),
    typeof(난방비MappingProfile),
    typeof(생활폐기물수수료MappingProfile),
    typeof(선거관리위원회운영비MappingProfile),
    typeof(수도료MappingProfile),
    typeof(입주자대표회의운영비MappingProfile),
    typeof(전기료MappingProfile),
    typeof(정화조오물수수료MappingProfile),

    typeof(공동주택기본정보MappingProfile),
    typeof(공동주택상세정보MappingProfile),

    typeof(경비비MappingProfile),
    typeof(교육훈련비MappingProfile),
    typeof(기타부대비용MappingProfile),
    typeof(소독비MappingProfile),
    typeof(수선비MappingProfile),
    typeof(승강기유지비MappingProfile),
    typeof(시설유지비MappingProfile),
    typeof(안전점검비MappingProfile),
    typeof(위탁관리수수료MappingProfile),
    typeof(인건비MappingProfile),
    typeof(재해예방비MappingProfile),
    typeof(제사무비MappingProfile),
    typeof(제세공과금MappingProfile),
    typeof(지능형홈네트워크설비유지비MappingProfile),
    typeof(차량유지비MappingProfile),
    typeof(청소비MappingProfile),
    typeof(피복비MappingProfile),

    typeof(단지별적립요율MappingProfile),
    typeof(단지별충당금잔액MappingProfile),
    typeof(단지별월부과액MappingProfile),
    typeof(단지별월사용액MappingProfile)
    );

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddTransient<공동주택단지목록APIService>();
builder.Services.AddTransient<공동주택단지정보APIService>();
builder.Services.AddTransient<공동주택개별관리비APIService>();
builder.Services.AddTransient<공동주택공용관리비APIService>();
builder.Services.AddTransient<공동주택에너지사용정보APIService>();
builder.Services.AddTransient<공동주택장기수선충당금APIService>();
```

<h2 align="center"><strong><span style="color:red;">공동주택정보수집Controller의 sequenceDiagram</span></strong></h2>

```mermaid
sequenceDiagram
    participant Client as Client
    participant Controller as 공동주택정보수집Controller
    participant Mediator as IMediator
    participant Handler as 공동주택단지Handler

    Client->>+Controller: GET /CollectAllComplexes
    Controller->>+Mediator: Send(new 공동주택단지Request())
    Mediator->>+Handler: Handle(공동주택단지Request)
    Handler->>-Mediator: Processing and Updates
    Mediator->>-Controller: Unit.Value
    Controller-->>-Client: Ok("All complexes collected and updated")
    Note over Client,Handler: Exception Handling by Controller
    
    Client->>+Controller: GET /CollectComplexDetails/{kaptCode}
    Controller->>+Mediator: Send(new 공동주택상세정보Request())
    Mediator->>+Handler: Handle(공동주택상세정보Request)
    Handler->>-Mediator: Processing and Updates
    Mediator->>-Controller: Unit.Value
    Controller-->>-Client: Ok("Details collected and updated")
    Note over Client,Handler: Exception Handling by Controller

```

<h2 align="center"><strong><span style="color:red;">비용정보수집Controller의 sequenceDiagram</span></strong></h2>

```mermaid
sequenceDiagram
    participant Client as Client
    participant Controller as 비용정보수집Controller
    participant Mediator as IMediator
    participant 개별사용료Handler as 개별사용료Handler
    participant 공용관리비Handler as 공용관리비Handler
    participant 에너지사용량Handler as 에너지사용량Handler
    participant 장기수선충당금Handler as 장기수선충당금Handler

    Client->>+Controller: POST /비용수집
    Controller->>+Mediator: Send(개별사용료Request)
    Mediator->>+개별사용료Handler: Handle
    개별사용료Handler->>-Mediator: Process and Update
    Controller->>+Mediator: Send(공용관리비Request)
    Mediator->>+공용관리비Handler: Handle
    공용관리비Handler->>-Mediator: Process and Update
    Controller->>+Mediator: Send(에너지사용량Request)
    Mediator->>+에너지사용량Handler: Handle
    에너지사용량Handler->>-Mediator: Process and Update
    Controller->>+Mediator: Send(장기수선충당금Request)
    Mediator->>+장기수선충당금Handler: Handle
    장기수선충당금Handler->>-Mediator: Process and Update
    Mediator->>-Controller: Unit.Value
    Controller-->>-Client: Ok("Cost information collected")
    Note over Client,장기수선충당금Handler: Exception Handling by Controller
```
