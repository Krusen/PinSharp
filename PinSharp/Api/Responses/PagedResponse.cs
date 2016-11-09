using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        internal static async Task<PagedResponse<T>> FromTask(Task<PagedApiResponse<IEnumerable<T>>> task)
        {
            var response = await task.ConfigureAwait(false);
            return new PagedResponse<T>(response.Data, response.Page?.Cursor);
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
