using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpeg.AutoGen
{
    [System.Diagnostics.DebuggerDisplay("{size} bytes")]
    partial struct AVBufferRef
    {
        public unsafe bool IsWriteable
        {
            get
            {
                var copy = this;
                var result = ffmpeg.av_buffer_is_writable(&copy);
                return result == 1;
            }
        }

        public unsafe ReadOnlySpan<byte> GetReadOnlyBuffer()
        {
            return new ReadOnlySpan<Byte>((void*)data, (int)size);
        }

        public unsafe Span<byte> GetWriteableBuffer()
        {
            if (!IsWriteable) throw new InvalidOperationException("The buffer is read only.");
            return new Span<Byte>((void*)data, (int)size);
        }
    }
}
