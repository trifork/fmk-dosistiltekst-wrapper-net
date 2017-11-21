using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fmk_dosistiltekst_wrapper_net.vowrapper
{
    public class LocalTime
    {

        public int Hour { get; protected set; }
        public int Minute { get; protected set; }
        public int? Second { get; protected set; }

        public LocalTime(int hour, int minute)
        {
            Hour = hour;
            Minute = minute;
            Second = null;
        }

        public LocalTime(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }

        /**
         * Construct a new LocalTime object from a date.
         * Note! uses Europe/Copenhagen timezone
         * @param time time
         */
        public LocalTime(DateTime time)
        {
            Hour = time.Hour;
            Minute = time.Minute;
            if (time.Second > 0)
            {
                Second = time.Second;
            }
            else
            {
                Second = null;
            }
        }

        public override string ToString()
        {
            if (Second.HasValue)
            {
                return string.Format("{0:2}:{1:2}:{2:2}", Hour, Minute, Second);
            }
            else
            {
                return string.Format("{0:2}:{1:2}", Hour, Minute);
            }
        }
    }
}