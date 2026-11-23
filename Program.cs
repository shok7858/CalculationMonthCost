using CalculationMonthCost.Services;
using CalculationMonthCost.Settings;

var builder = WebApplication.CreateBuilder(args);

// ==================================================
// Cloud Run対応
// ==================================================
// Cloud Runから指定されたPORTを取得する
var port = Environment.GetEnvironmentVariable("PORT");

if (!string.IsNullOrWhiteSpace(port))
{
    // Cloud Runから外部接続できるように設定
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");
}

builder.Services.AddControllers();

// 固定費に関する設定情報を読み込む。
builder.Services.Configure<FixedCostOptions>(
    builder.Configuration.GetSection("FixedCosts"));
// 計算処理サービスをDIコンテナに登録する。
builder.Services.AddScoped<ICostCalculationService, CostCalculationService>();

var app = builder.Build();


// ==================================================
// HTTPS設定
// ==================================================

// 【従来の設定】
// app.UseHttpsRedirection();
//
// ローカル環境ではHTTPSリダイレクトが必要なため使用する。
// Cloud RunではGoogle Cloud側でHTTPS通信を終端し、
// コンテナにはHTTPで転送するため、Cloud Run環境では不要。
// そのためDevelopment環境のみ有効にする
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// ==================================================
// トップページの表示
// ==================================================

// ルートURL（/）にアクセスした場合、index.htmlを返す。
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        var indexPath = Path.Combine(
            app.Environment.WebRootPath ?? "wwwroot",
            "index.html");

        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync(indexPath);

        return;
    }

    await next();
});

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        var indexPath = Path.Combine(
            app.Environment.WebRootPath ?? "wwwroot",
            "index.html");

        context.Response.ContentType = "text/html; charset=utf-8";
        await context.Response.SendFileAsync(indexPath);

        return;
    }

    await next();
});
// wwwroot配下の静的ファイル（HTML、CSS、JavaScriptなど）を配信する。
app.UseStaticFiles();
// Controllerのルーティングを有効にする。
app.MapControllers();
// アプリケーションを起動する
app.Run();