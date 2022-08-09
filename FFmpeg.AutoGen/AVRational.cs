using System;
using System.Collections.Generic;
using System.Text;

namespace FFmpeg.AutoGen
{
    [System.Diagnostics.DebuggerDisplay("{num} / {den} = {Fraction}")]
    partial struct AVRational : IComparable<AVRational>
    {
        /// <summary>
        /// Convert an AVRational to a double.
        /// </summary>
        /// <remarks>
        /// Equivalent to av_q2d
        /// </remarks>
        public double Fraction => num / (double)den;

        /// <remarks>
        /// return One of the following values:<br/>
        /// - 0 if this == other<br/>
        /// - 1 if this &lt; other<br/>
        /// - -1 if this &gt; other<br/>
        /// - INT_MIN if one of the values is of the form 0 / 0<br/>
        /// Equivalent to av_cmp_q
        /// </remarks>        
        public int CompareTo(AVRational other)
        {
            Int64 tmp = this.num * (Int64)other.den - other.num * (Int64)this.den;

            if (tmp != 0) return (int)((tmp ^ this.den ^ other.den) >> 63) | 1;
            else if (other.den != 0 && this.den !=0) return 0;
            else if (this.num != 0 && other.num != 0) return (this.num >> 31) - (other.num >> 31);
            else return int.MinValue;
        }
    }
}
