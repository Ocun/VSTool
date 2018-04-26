/*-----------------------------
 *      create by 08628
 *      create date 20180426
 *----------------------------- 
 */

using System.Collections.Generic;

namespace Digiwin.Chun.Common.Model
{
    /// <summary>
    /// 硬件信息
    /// </summary>
    public class HardwareEntity
    {
        /// <summary>
        /// CpuInfo
        /// </summary>
        public List<CpuInfo> CpuInfos { get; set; }
        /// <summary>
        /// MainBoardInfo
        /// </summary>
        public List<MainBoardInfo> MainBoardInfos { get; set; }
        /// <summary>
        /// DiskDriveInfo
        /// </summary>
        public List<DiskDriveInfo> DiskDriveInfos { get; set; }
    /// <summary>
    /// NetworkInfo
    /// </summary>
    public List<NetworkInfo> NetworkInfos { get; set; }
        /// <summary>
        /// OsInfo
        /// </summary>
        public List<OsInfo> OsInfo { get; set; }
    }

    /// <summary>
    /// CPU信息
    /// </summary>
    public class CpuInfo {
        /// <summary>
        /// 编号
        /// </summary>
        public string ProcessorId { get; set; }
        /// <summary>
        /// CPU型号
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// CPU状态
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// 主机名称
        /// </summary>
        public string SystemName { get; set; }
    }

    /// <summary>
    /// 主板信息
    /// </summary>
    public class MainBoardInfo {
        /// <summary>
        /// 主板ID
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Product { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

    }

    /// <summary>
    /// 硬盘信息
    /// </summary>
    public class DiskDriveInfo {
        /// <summary>
        /// 硬盘SN
        /// </summary>
        public string SerialNumber { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public string Model { get; set; }
        /// <summary>
        /// 大小
        /// </summary>
        public string Size { get; set; }
    }

    /// <summary>
    /// 网络信息
    /// </summary>
    public class NetworkInfo
    {
        /// <summary>
        /// MAC地址
        /// </summary>
        public string MacAddress { get; set; }
        /// <summary>
        /// IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// 子网掩码
        /// </summary>
        public string IPSubnet { get; set; }
        /// <summary>
        /// 网关
        /// </summary>
        public string DefaultIPGateway { get; set; }
        /// <summary>
        /// DNS
        /// </summary>
        public string DNSServerSearchOrder { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// 操作系统信息
    /// </summary>
    public class OsInfo
    {
        /// <summary>
        /// 操作系统
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 系统目录
        /// </summary>
        public string SystemDirectory { get; set; }
    }

}
