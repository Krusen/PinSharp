using System.Collections;
using System.Collections.Generic;

namespace PinSharp.Models.Responses
{
    public class PagedResponse<T> : IReadOnlyList<T>
    {
        private IReadOnlyList<T> Items { get; }

        public string NextPageCursor { get; set; }

        public PagedResponse(IEnumerable<T> pins)
            : this(pins, null)
        {
        }

        public PagedResponse(IEnumerable<T> pins, string cursor)
        {
            Items = new List<T>(pins);
            NextPageCursor = cursor;
        }

        public IEnumerator<T> GetEnumerator() => Items.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => Items.Count;

        public T this[int index] => Items[index];
    }
}
