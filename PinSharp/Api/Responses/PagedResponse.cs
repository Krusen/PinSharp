using System.Collections;
using System.Collections.Generic;

namespace PinSharp.Api.Responses
{
    public class PagedResponse<T> : IReadOnlyList<T>
    {
        private IReadOnlyList<T> Items { get; }

        public PagedResponse(IEnumerable<T> pins) : this(pins, null)
        {
        }

        public PagedResponse(IEnumerable<T> pins, string cursor)
        {
            Items = new List<T>(pins);
            NextPageCursor = cursor;
        }

        public string NextPageCursor { get; set; }

        public int? Ratelimit { get; set; }
        public int? RatelimitRemaining { get; set; }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => Items.Count;

        public T this[int index] => Items[index];
    }
}
