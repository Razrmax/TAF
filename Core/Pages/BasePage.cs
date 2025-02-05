using Microsoft.Playwright;
using NLog;

namespace Core.Pages
{
    public class BasePage : IDisposable, IAsyncDisposable
    {
        protected readonly IPage page;
        protected static readonly ILogger log;
        public BasePage(IPage page)
        {
            this.page = page;
            this.page.Load += PageLoad;
            this.page.Close += PageClose;
            this.page.Console += PageConsole;
            this.page.PageError += PageError;
            this.page.Crash += PageCrash;
        }

        [Obsolete]
        static BasePage()
        {
            LogManager.LoadConfiguration("nlog.config");
            log = LogManager.GetCurrentClassLogger();
        }

        private void PageCrash(object? sender, IPage e)
        {
            log.Debug($"Crashed page URL is {e.Url}");

        }

        private void PageError(object? sender, string e)
        {
            log.Error(e);

        }

        private void PageConsole(object? sender, IConsoleMessage e)
        {
            log.Debug(e.ToString());

        }

        private void PageLoad(object? sender, IPage e)
        {
            log.Debug($"Loaded page URL is {e.Url}");

        }

        private void PageClose(object? sender, IPage e)
        {
            log.Debug($"Closed page URL is {e.Url}");

        }

        public void Dispose()
        {
            this.page.Load -= PageLoad;
            this.page.Close -= PageClose;
            this.page.Console -= PageConsole;
            this.page.PageError -= PageError;
            this.page.Crash -= PageCrash;
            GC.SuppressFinalize(this);
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return ValueTask.CompletedTask;
        }
    }
}
