using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Digiwin.Chun.Common.Tools {
    /// <summary>
    /// 时间同步
    /// </summary>
    public class SysTimeHelper {
        SysTimeHelper() {

        }

        #region   ComputerTime

        [StructLayout(LayoutKind.Sequential)]
        private struct SystemTime {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMiliseconds;
        }

        // ReSharper disable once ClassNeverInstantiated.Local
        private class Win32 {
            [DllImport("Kernel32.dll ")]
            public static extern bool SetSystemTime(ref SystemTime SysTime);

            [DllImport("Kernel32.dll ")]
            public static extern void GetSystemTime(ref SystemTime SysTime);
        }


        #endregion

        #region   时间同步

        ///   <summary> 
        ///   设置与服务器同步时间   
        ///   </summary> 
        public static void SynchronousTime(DateTime serverTime) {

            try {
                #region   更改计算机时间

                var sysTime = new SystemTime {
                    wYear = Convert.ToUInt16(serverTime.Year),
                    wMonth = Convert.ToUInt16(serverTime.Month)
                };

                //处置北京时间   

                var nBeijingHour = serverTime.Hour - 8;

                if (nBeijingHour <= 0) {
                    nBeijingHour += 24;

                    sysTime.wDay = Convert.ToUInt16(serverTime.Day - 1);

                    sysTime.wDayOfWeek = Convert.ToUInt16(serverTime.DayOfWeek - 1);
                }
                else {
                    sysTime.wDay = Convert.ToUInt16(serverTime.Day);

                    sysTime.wDayOfWeek = Convert.ToUInt16(serverTime.DayOfWeek);
                }

                sysTime.wHour = Convert.ToUInt16(nBeijingHour);

                sysTime.wMinute = Convert.ToUInt16(serverTime.Minute);

                sysTime.wSecond = Convert.ToUInt16(serverTime.Second);

                sysTime.wMiliseconds = Convert.ToUInt16(serverTime.Millisecond);

                Win32.SetSystemTime(ref sysTime);

                #endregion
            }
            catch (Exception ex) {
                LogTools.LogError(ex.ToString());
            }
        }

        #endregion
    }
}