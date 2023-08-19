using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M3.Cord.Utils
{
    public class MathEx
    {
        #region Round
        public static decimal Round(decimal value)
        {
            decimal output = decimal.Zero;

            try
            {
                if (value != decimal.Zero)
                {
                    output = Math.Round(value, 2, MidpointRounding.AwayFromZero);
                    return output;
                }
                else
                {
                    return output;
                }
            }
            catch
            {
                return output;
            }
        }
        #endregion

        #region Round
        /// <summary>
        /// Simulate arithmatic rounding like in Excel 
        /// </summary>
        /// <param name="value">A Decimal value to round</param>
        /// <param name="digits">presision digit</param>
        /// <returns>A value rounded to decimals number of decimal places</returns>
        public static decimal Round(decimal value, int digits)
        {
            decimal scale = (decimal)Math.Pow(10.0, (double)(digits + 1));
            value = Decimal.Floor(value * scale) / scale;

            if ((Math.Floor((double)value * Math.Pow(10.0, digits)) % 2) == 0)	// check for even nearest
                value += (decimal)1.0 / scale;

            return Math.Round(value, digits);
        }

        #endregion

        #region Round
        /// <summary>
        /// Simulate arithmatic rounding like in Excel 
        /// </summary>
        /// <param name="value">A Decimal value to round</param>
        /// <param name="digits">presision digit</param>
        /// <returns>A value rounded to decimals number of decimal places</returns>
        public static decimal Round(float value, int digits)
        {
            try
            {
                decimal valueF = (decimal)Math.Round(value, digits + 1);

                decimal scale = (decimal)Math.Pow(10.0, (double)(digits + 1));
                valueF = Decimal.Floor(valueF * scale) / scale;

                if ((Math.Floor((double)value * Math.Pow(10.0, digits)) % 2) == 0)	// check for even nearest
                    valueF += (decimal)1.0 / scale;

                return Math.Round(valueF, digits);
            }
            catch
            {
                return 0;
            }
        }

        #endregion

        public static decimal TruncateDecimal(decimal value, int precision)
        {
            decimal step = (decimal)Math.Pow(10, precision);
            int tmp = (int)Math.Truncate(step * value);
            return tmp / step;
        }
    }
}
