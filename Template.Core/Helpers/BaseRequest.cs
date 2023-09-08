using System;
using System.Collections.Generic;

namespace Template.Core.Helpers
{
    public class BaseRequest
    {
        public virtual IDictionary<string, string> DefaultRequestHeaders { get; private set; } = new Dictionary<string, string>();
        public BaseRequest() {  }
        public BaseRequest(Guid protocolo) { }
        public virtual void AddHeader(string key, string value)
        {
            if (!DefaultRequestHeaders.ContainsKey(key))
            {
                DefaultRequestHeaders.Add(key, value);
            }
            else
            {
                DefaultRequestHeaders[key] = value;
            }
        }
        public virtual string GetHeader(string key)
        {
            if (!DefaultRequestHeaders.TryGetValue(key, out var value))
            {
                return string.Empty;
            }
            return value;
        }
    }
}
