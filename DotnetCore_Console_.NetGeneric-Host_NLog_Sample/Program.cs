using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace DotnetCore_Console_.NetGeneric_Host_NLog_Sample
{
    internal class Program
    {
        /**
         * 參考 使用 .NET Generic Host 建立 Console 主控台應用程式 (.NET Core 3.1+)
         * https://blog.miniasp.com/post/2020/12/08/NET-Generic-Host-Build-Console-App
         */

        private static void Main(string[] args)
        {
            // 取得 IHostBuilder 物件實體
            IHostBuilder hostBuilder = CreateHostBuilder(args);
            // 取得 IHost物件實體
            IHost host = hostBuilder.Build();

            // 透過IHost啟動Host應用程式去執行App
            host.Run();
        }

        /**
        * 關於LogLevel
        * https://docs.microsoft.com/zh-tw/dotnet/api/microsoft.extensions.logging.loglevel?view=dotnet-plat-ext-5.0&WT.mc_id=DT-MVP-4015686
        * 透過Appsettings內設置最小輸出顯示紀錄層級
        * Host.CreateDefaultBuilder(args) 預設就有包含設定載入 appsettings.json
        *
        * 關於NLog
        * https://github.com/NLog/NLog/wiki/Getting-started-with-ASP.NET-Core-5
        */

        /// <summary>
        /// 實作建立IHost物件配置
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args).ConfigureLogging(logger =>
            {
                // 透過 IHostBuilder - ConfigureLogging
                // 設置清除掉預設Logger提供者
                // 設置最小輸出層級，目前是吃Appsettings內("Logging")配置
                logger.ClearProviders();
                //logger.SetMinimumLevel(LogLevel.Trace);
            }).UseNLog() // 注入NLog作為輸出Log提供者
            .ConfigureServices((hostContext, services) =>
            {
                // 注入一個App 服務
                services.AddHostedService<AppTask>();
            });
    }
}