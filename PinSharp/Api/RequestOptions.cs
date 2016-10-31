using System.Collections.Generic;
using System.Linq;

namespace PinSharp.Api
{
    internal class RequestOptions
    {
        public string SearchQuery { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public string Cursor { get; set; }
        public int Limit { get; set; }
        public object CustomData { get; set; }

        public RequestOptions()
            : this(Enumerable.Empty<string>())
        {
        }

        public RequestOptions(string cursor, int limit)
            : this(Enumerable.Empty<string>(), cursor, limit)
        {
        }

        public RequestOptions(IEnumerable<string> fields)
            : this(fields, null, 0)
        {
        }

        public RequestOptions(IEnumerable<string> fields, object customData)
            : this(fields, null, 0, customData)
        {
        }

        public RequestOptions(IEnumerable<string> fields, int limit)
            : this(fields, null, limit)
        {
        }

        public RequestOptions(IEnumerable<string> fields, int limit, object customData)
            : this(fields, null, limit, customData)
        {
        }

        public RequestOptions(IEnumerable<string> fields, string cursor)
            : this(fields, cursor, 0)
        {
        }

        public RequestOptions(IEnumerable<string> fields, string cursor, object customData)
            : this(fields, cursor, 0, customData)
        {
        }

        public RequestOptions(IEnumerable<string> fields, string cursor, int limit)
            : this(fields, cursor, limit, null)
        {
        }

        public RequestOptions(IEnumerable<string> fields, string cursor, int limit, object customData)
        {
            Fields = fields ?? Enumerable.Empty<string>();
            Cursor = cursor;
            Limit = limit;
            CustomData = customData;
        }

        public RequestOptions(string searchQuery)
            : this(searchQuery, Enumerable.Empty<string>())
        {
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields)
            : this(searchQuery, fields, null, 0)
        {
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields, int limit)
            : this(searchQuery, fields, null, limit)
        {
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields, string cursor)
            : this(searchQuery, fields, cursor, 0)
        {
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields, string cursor, int limit)
            : this(searchQuery, fields, cursor, limit, null)
        {
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields, string cursor, int limit, object customData)
        {
            SearchQuery = searchQuery;
            Fields = fields ?? Enumerable.Empty<string>();
            Cursor = cursor;
            Limit = limit;
            CustomData = customData;
        }
    }
}
