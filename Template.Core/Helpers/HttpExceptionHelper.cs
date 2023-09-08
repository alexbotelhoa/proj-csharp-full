namespace Template.Core.Helpers
{
    public class HttpExceptionHelper
    {
        public string Service { get; set; }
        public string Url { get; set; }
        public object Header { get; set; }
        public object Request { get; set; }
        public object Result { get; set; }
        public object Response { get; set; }
    }
}
