using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DotnetCore_Console_.NetGeneric_Host_NLog_Sample
{
    /// <summary>
    /// 建立一個App，並繼承IHostedService介面
    /// 並實作工作起始函式與結束函式內容
    /// </summary>
    public class AppTask : IHostedService
    {
        #region Variables

        private readonly ILogger<AppTask> logger;
        private readonly IHostApplicationLifetime appLifetime;

        #endregion Variables

        #region Constructor

        public AppTask(
            ILogger<AppTask> _logger,
            IHostApplicationLifetime _appLifetime
        )
        {
            logger = _logger;
            appLifetime = _appLifetime;
        }

        #endregion Constructor

        /// <summary>
        /// App工作開始進入點階段
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                // 在這裡實作App 內容
                logger.LogInformation("App running at: {time}", DateTimeOffset.Now);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled exception!");
            }
            finally
            {
                // 不管有沒有錯誤，最後都要宣告當前工作已經結束，進入工作結束進入點階段
                appLifetime.StopApplication();
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// App工作結束進入點階段
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("App stopped at: {time}", DateTimeOffset.Now);

            // 告訴上層程式當前工作已經結束完成
            return Task.CompletedTask;
        }
    }
}