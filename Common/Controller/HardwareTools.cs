﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management;
using System.Reflection;
using Digiwin.Chun.Common.Model;

namespace Digiwin.Chun.Common.Controller
{
    /// <summary>
    /// 获取系统硬件信息
    /// </summary>
    public static class HardwareTools
    {
        /// <summary>
        /// 
        /// </summary>
        public static void GetHardwareInfo() {
            try {
                var cpuInfo = GetCpuInfo();
                var mainBoardInfo = GetMainBoardInfo();
                var diskDriveInfo = GetDiskDriveInfo();
                var networkInfo = GetNetworkInfo();
                var osInfo = GetOsInfo();
                MyTools.HardwareInfo.CpuInfos = cpuInfo;
                MyTools. HardwareInfo.MainBoardInfos = mainBoardInfo;
                MyTools. HardwareInfo.DiskDriveInfos = diskDriveInfo;
                MyTools. HardwareInfo.NetworkInfos = networkInfo;
                MyTools. HardwareInfo.OsInfo = osInfo;
            }
            catch (Exception ex) {
                LogTools.LogError($"GetHardwareInfo error! detail {ex.Message}");
            }
       
        }

        /// <summary>
        /// 獲取硬件信息集合
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static ManagementObjectCollection GetManagementObjectCollection(string model) {
            var mc = new ManagementClass(model);
            return mc.GetInstances();
        }

        /// <summary>
        /// Cpu信息
        /// </summary>
        /// <returns></returns>
        public static List<CpuInfo> GetCpuInfo()
        {
            var cpuInfos = new List<CpuInfo>();
            try {
                var moc = GetManagementObjectCollection(WMIPath.Win32_Processor.ToString());
                var empStr = string.Empty;
                foreach (var mo in moc)
                {
                    var cpuInfo = new CpuInfo() {
                        ProcessorId = (mo.Properties["ProcessorId"].Value?? empStr).ToString(),
                        Name = (mo.Properties["Name"].Value?? empStr).ToString(),
                        Status = (mo.Properties["Status"].Value?? empStr).ToString(),
                        SystemName = (mo.Properties["SystemName"].Value?? empStr).ToString()
                    };
                    cpuInfos.Add(cpuInfo);
                }
            }
            catch(Exception ex) {
                LogTools.LogError($"GetCpuInfo Error! Detail:{ex.Message}");
            }
            return cpuInfos;
        }
        /// <summary>
        /// 主板信息
        /// </summary>
        public static List<MainBoardInfo> GetMainBoardInfo()
        {
            var mainBoardInfos = new List<MainBoardInfo>();
            try
            {
                var moc = GetManagementObjectCollection(WMIPath.Win32_BaseBoard.ToString());
                var empStr = string.Empty;
                foreach (var mo in moc)
                {
                    var serialNumber = (mo.Properties["SerialNumber"].Value ?? empStr).ToString();
                    var manufacturer = (mo.Properties["Manufacturer"].Value ?? empStr).ToString();
                    var product = (mo.Properties["Product"].Value ?? empStr).ToString();
                    var version = (mo.Properties["Version"].Value ?? empStr).ToString();
                    var mainBoardInfo = new MainBoardInfo() {
                        SerialNumber = serialNumber,
                        Manufacturer = manufacturer,
                        Product = product,
                        Version = version
                    };
                    mainBoardInfos.Add(mainBoardInfo);
                }
            }
            catch(Exception ex)
            {
                LogTools.LogError($"GetMainBoardInfo Error! Detail:{ex.Message}");
            }
            return mainBoardInfos;
        }
        /// <summary>
        /// 硬盘信息
        /// </summary>
        public static List<DiskDriveInfo> GetDiskDriveInfo( ){
            var diskDriverInfos = new List<DiskDriveInfo>();
            try
            {
                var empStr = string.Empty;
                var moc = GetManagementObjectCollection(WMIPath.Win32_DiskDrive.ToString());
                foreach (var mo in moc)
                {
                    var diskDriverInfo = new DiskDriveInfo() {
                        SerialNumber = (mo.Properties["SerialNumber"].Value??empStr).ToString(),
                        Model = (mo.Properties["Model"].Value??empStr).ToString(),
                        Size = ((Convert.ToDouble(mo.Properties["Size"].Value??0) / (1024 * 1024 * 1024))).ToString(CultureInfo.InvariantCulture)
                  
                    };
                    diskDriverInfos.Add(diskDriverInfo);
              
                }
            }
            catch(Exception ex)
            {
                LogTools.LogError($"GetDiskDriveInfo Error! Detail:{ex.Message}");
            }
            return diskDriverInfos;
        }
        /// <summary>
        /// 网络连接信息
        /// </summary>
        public static List<NetworkInfo> GetNetworkInfo() {
            var networkInfos = new List<NetworkInfo>();
            try
            {
                var empStr = string.Empty;
                var moc = GetManagementObjectCollection(WMIPath.Win32_NetworkAdapterConfiguration.ToString());
                foreach (var mo in moc) {
                    if (!(bool)mo["IPEnabled"]) continue;
                    var ipAr =(Array)(mo.Properties["IpAddress"].Value);
                    var subnets = (string[])mo["IPSubnet"];            //子网掩码
                    var gateways = (string[])mo["DefaultIPGateway"];   //网关
                    var dnses = (string[])mo["DNSServerSearchOrder"];  //DNS
                    var description = mo["Description"].ToString();
                    var ipAddress = ipAr.GetValue(0).ToString();
                    var ipSubnet = subnets?.GetValue(0).ToString() ?? empStr;
                    var defaultIpGateway = gateways?.GetValue(0).ToString() ?? empStr;
                    var dnsServerSearchOrder = dnses?.GetValue(0).ToString() ?? empStr;
                    var networkInfo = new NetworkInfo() {
                        MacAddress = (mo.Properties["MACAddress"].Value ?? empStr).ToString(),
                        IpAddress = ipAddress,
                        Caption = mo.Properties["Caption"].Value.ToString(),
                        IPSubnet = ipSubnet,
                        DefaultIPGateway = defaultIpGateway,
                        DNSServerSearchOrder = dnsServerSearchOrder,
                        Description = description
                    };
                    networkInfos.Add(networkInfo);
                }
            }
            catch(Exception ex)
            {
                LogTools.LogError($"GetNetworkInfo Error! Detail:{ex.Message}");
            }
            return networkInfos;
        }
        /// <summary>
        /// 操作系统信息
        /// </summary>
        public static List<OsInfo> GetOsInfo() {
            var osInfos= new List<OsInfo>();
            try
            {
                var empStr = string.Empty;
                var moc = GetManagementObjectCollection(WMIPath.Win32_OperatingSystem.ToString());
                foreach (var mo in moc)
                {
                    var osInfo = new OsInfo() {
                        Name = (mo.Properties["Name"].Value ?? empStr).ToString(),
                        Version = (mo.Properties["Version"].Value ?? empStr).ToString(),
                        SystemDirectory = (mo.Properties["SystemDirectory"].Value ?? empStr).ToString(),
                    };
                    osInfos.Add(osInfo);
                }
            }
            catch(Exception ex)
            {
                LogTools.LogError($"GetOsInfo Error! Detail:{ex.Message}");
            }
            return osInfos;
        }
    }
}
