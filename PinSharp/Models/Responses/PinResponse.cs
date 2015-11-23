using System.Collections;
using System.Collections.Generic;

namespace PinSharp.Models.Responses
{
    public class PinResponse<T> : IEnumerable<T>
    {
        private IEnumerable<T> Pins { get; }

        public string NextPageCursor { get; set; }

        public PinResponse(IEnumerable<T> pins)
            : this(pins, null)
        {
        }

        public PinResponse(IEnumerable<T> pins, string cursor)
        {
            Pins = pins;
            NextPageCursor = cursor;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return Pins.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
