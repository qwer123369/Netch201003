﻿using System;

namespace NetSpeedMonitor.Utils
{
    public static class Util
    {
        public static string ToByteSize(long size)
        {
            var mStrSize = @"0";
            const double step = 1024.00;
            var factSize = size;
            if (factSize < step)
            {
                mStrSize = $@"{factSize:0.##} B";
            }
            else if (factSize >= step && factSize < 1048576)
            {
                mStrSize = $@"{factSize / step:0.##} KiB";
            }
            else if (factSize >= 1048576 && factSize < 1073741824)
            {
                mStrSize = $@"{factSize / step / step:0.##} MiB";
            }
            else if (factSize >= 1073741824 && factSize < 1099511627776)
            {
                mStrSize = $@"{factSize / step / step / step:0.##} GiB";
            }
            else if (factSize >= 1099511627776)
            {
                mStrSize = $@"{factSize / step / step / step / step:0.##} TiB";
            }

            return mStrSize;
        }

        public static long ToLong(string str)
        {
            if (str.EndsWith(@"/S"))
            {
                str = str.Substring(0, str.Length - 2);
            }

            if (!str.EndsWith(@"B"))
            {
                return 0;
            }

            var s = str.Split(' ');
            if (s.Length != 2)
            {
                throw new ArgumentException(@"Wrong Byte Size");
            }

            switch (s[1])
            {
                case @"TiB" when double.TryParse(s[0], out var res):
                    return Convert.ToInt64(res * 1099511627776);
                case @"GiB" when double.TryParse(s[0], out var res):
                    return Convert.ToInt64(res * 1073741824);
                case @"MiB" when double.TryParse(s[0], out var res):
                    return Convert.ToInt64(res * 1048576);
                case @"KiB" when double.TryParse(s[0], out var res):
                    return Convert.ToInt64(res * 1024);
                case @"B" when double.TryParse(s[0], out var res):
                    return Convert.ToInt64(res);
                default:
                    throw new ArgumentException(@"Wrong Byte Size");
            }
        }


    }
}
