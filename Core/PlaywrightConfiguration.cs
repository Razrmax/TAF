using TAF.Core.Utils;
using Microsoft.Playwright;
using System.Text.Json.Serialization;

namespace TAF.Core
{
    public class BrowserOptions
    {

        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("headed")]
        public string? Headed { get; set; }
    }

    public class PlaywrightConfiguration
    {
        public readonly BrowserOptions browserOptions;
        public readonly BrowserNewContextOptions browserNewContextOptions;

        private class Config
        {
            [JsonInclude]
            public BrowserOptions Browser { get; set; } = new BrowserOptions();
            [JsonInclude]
            public BrowserNewContextOptions BrowserContextOptions { get; set; } = new BrowserNewContextOptions();
        }
        public PlaywrightConfiguration()
        {
            var config = JsonReader.Read<Config>("config.json");
            browserOptions = config.Browser;
            browserNewContextOptions = config.BrowserContextOptions;
        }
    }
}
