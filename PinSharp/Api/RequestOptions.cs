using System.Collections.Generic;

namespace PinSharp.Api
{
    internal class RequestOptions
    {
        public string SearchQuery { get; set; }
        public IEnumerable<string> Fields { get; set; }
        public string Cursor { get; set; }
        public int Limit { get; set; }

        public RequestOptions()
        {

        }

        public RequestOptions(string cursor, int limit)
        {
            Cursor = cursor;
            Limit = limit;
        }

        public RequestOptions(IEnumerable<string> fields)
        {
            Fields = fields;
        }

        public RequestOptions(IEnumerable<string> fields, int limit)
        {
            Fields = fields;
            Limit = limit;
        }

        public RequestOptions(IEnumerable<string> fields, string cursor)
        {
            Fields = fields;
            Cursor = cursor;
        }

        public RequestOptions(IEnumerable<string> fields, string cursor, int limit)
        {
            Fields = fields;
            Cursor = cursor;
            Limit = limit;
        }

        public RequestOptions(string searchQuery)
        {
            SearchQuery = searchQuery;
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields)
        {
            SearchQuery = searchQuery;
            Fields = fields;
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields, int limit)
        {
            SearchQuery = searchQuery;
            Fields = fields;
            Limit = limit;
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields, string cursor)
        {
            SearchQuery = searchQuery;
            Fields = fields;
            Cursor = cursor;
        }

        public RequestOptions(string searchQuery, IEnumerable<string> fields, string cursor, int limit)
        {
            SearchQuery = searchQuery;
            Fields = fields;
            Cursor = cursor;
            Limit = limit;
        }
    }
}
