namespace PlaywrightSample.Core
{
    public class TestConfiguration
    {
        public string? TestUrl { get; set; }
    }

    public class TestOptions
    {
        public TestConfiguration? TestUrl { get; set; } = new TestConfiguration();
    }
}
