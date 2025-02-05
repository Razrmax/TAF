using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using TAF.Core;

namespace TAF.Tests
{
    public abstract class BaseTest : PageTest
    {
        protected internal static readonly PlaywrightConfiguration _config;
        static BaseTest()
        {
            _config = new PlaywrightConfiguration();
        }

        [OneTimeSetUp]
        public async Task Setup()
        {

            Environment.SetEnvironmentVariable("Browser", _config.browserOptions.Name);
            Environment.SetEnvironmentVariable("HEADED", _config.browserOptions.Headed);
        }

        public override BrowserNewContextOptions ContextOptions()
        {
            return _config.browserNewContextOptions;
        }
    }
}
