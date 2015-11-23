using System.Collections;
using System.Collections.Generic;

namespace PinSharp.Models.Responses
{
    public class PinResponse<T> : IReadOnlyList<T>
    {
        private IReadOnlyList<T> Pins { get; }

        public string NextPageCursor { get; set; }

        public PinResponse(IEnumerable<T> pins)
            : this(pins, null)
        {
        }

        public PinResponse(IEnumerable<T> pins, string cursor)
        {
            Pins = new List<T>(pins);
            NextPageCursor = cursor;
        }

        public IEnumerator<T> GetEnumerator() => Pins.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => Pins.Count;

        public T this[int index] => Pins[index];
    }
}
