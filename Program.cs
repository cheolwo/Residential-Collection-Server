using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using 국토교통부_공공데이터Common;
using 국토교통부_공공데이터Common.개발사용료정보제공서비스;
using 국토교통부_공공데이터Common.공동주택_기본정보_제공서비스;
using 국토교통부_공공데이터Common.공용관리비_정보제공서비스.MappingProfile;
using 국토교통부_공공데이터Common.공용관리비정보제공서비스;
using 국토교통부_공공데이터Common.국토교통부_공동주택단지목록제공서비스;
using 국토교통부_공공데이터Common.에너지사용정보_정보서비스;
using 국토교통부_공공데이터Common.장기수선충당금_정보서비스;
using 국토교통부_공공데이터Common.장기수선충당금_정보서비스.MappingProfile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

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
//// MediatR 핸들러 등록
//builder.Services.AddMediatR(cfg =>
//{
//    cfg.AddMediatR(Assembly.GetExecutingAssembly()); // 현재 어셈블리에 있는 모든 MediatR 핸들러 자동 등록
//    // 여러 어셈블리가 있다면, 아래와 같이 추가할 수 있습니다.
//    // cfg.AddMediatR(typeof(SomeOtherHandler).Assembly);
//});


// 공동주택단지APIService
builder.Services.AddTransient<공동주택단지목록APIService>();
builder.Services.AddTransient<공동주택단지정보APIService>();
builder.Services.AddTransient<공동주택개별관리비APIService>();
builder.Services.AddTransient<공동주택공용관리비APIService>();
builder.Services.AddTransient<공동주택에너지사용정보APIService>();
builder.Services.AddTransient<공동주택장기수선충당금APIService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
