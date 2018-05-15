// ReSharper disable All
namespace Digiwin.Chun.Common.Controller {
    /// <summary>
    ///     model文件类型
    /// </summary>
    public enum ModelType {
        /// <summary>
        ///     xml类型
        /// </summary>
        Xml,

        /// <summary>
        ///     json类型
        /// </summary>
        Json,

        /// <summary>
        ///     二进制
        /// </summary>
        Binary
    }

    /// <summary>
    /// 系统硬件信息
    /// </summary>
    public enum WMIPath
    {
        // 硬件
        /// <summary>
        /// CPU 处理器
        /// </summary>
        Win32_Processor,
        /// <summary>
        /// // 物理内存条
        /// </summary>
        Win32_PhysicalMemory,
        /// <summary>
        /// // 键盘
        /// </summary>
        Win32_Keyboard,
        /// <summary>
        /// // 点输入设备，包括鼠标。
        /// </summary>
        Win32_PointingDevice,
        /// <summary>
        /// // 软盘驱动器
        /// </summary>
        Win32_FloppyDrive,
        /// <summary>
        ///  // 硬盘驱动器
        /// </summary>
        Win32_DiskDrive,
        /// <summary>
        /// // 光盘驱动器
        /// </summary>
        Win32_CDROMDrive,
        /// <summary>
        /// // 主板
        /// </summary>
        Win32_BaseBoard,
        /// <summary>
        ///  // BIOS 芯片
        /// </summary>
        Win32_BIOS,
        /// <summary>
        /// // 并口
        /// </summary>
        Win32_ParallelPort,
        /// <summary>
        /// // 串口
        /// </summary>
        Win32_SerialPort,
        /// <summary>
        /// // 串口配置
        /// </summary>
        Win32_SerialPortConfiguration,
        /// <summary>
        ///  // 多媒体设置，一般指声卡。
        /// </summary>
        Win32_SoundDevice,
        /// <summary>
        /// // 主板插槽 (ISA & PCI & AGP)
        /// </summary>
        Win32_SystemSlot,
        /// <summary>
        ///  // USB 控制器
        /// </summary>
        Win32_USBController,
        /// <summary>
        /// // 网络适配器
        /// </summary>
        Win32_NetworkAdapter,
        /// <summary>
        /// // 网络适配器设置
        /// </summary>
        Win32_NetworkAdapterConfiguration,
        /// <summary>
        /// // 打印机
        /// </summary>
        Win32_Printer,
        /// <summary>
        /// // 打印机设置
        /// </summary>
        Win32_PrinterConfiguration,
        /// <summary>
        ///  // 打印机任务
        /// </summary>
        Win32_PrintJob,
        /// <summary>
        ///  // 打印机端口
        /// </summary>
        Win32_TCPIPPrinterPort,
        /// <summary>
        /// // MODEM
        /// </summary>
        Win32_POTSModem,     
        Win32_POTSModemToSerialPort, // MODEM 端口
        Win32_DesktopMonitor,  // 显示器
        Win32_DisplayConfiguration, // 显卡
        Win32_DisplayControllerConfiguration, // 显卡设置
        Win32_VideoController, // 显卡细节。
        Win32_VideoSettings,  // 显卡支持的显示模式。
        // 操作系统
        Win32_TimeZone,     // 时区
        Win32_SystemDriver,   // 驱动程序
        Win32_DiskPartition,  // 磁盘分区
        Win32_LogicalDisk,   // 逻辑磁盘
        Win32_LogicalDiskToPartition,   // 逻辑磁盘所在分区及始末位置。
        Win32_LogicalMemoryConfiguration, // 逻辑内存配置
        Win32_PageFile,     // 系统页文件信息
        Win32_PageFileSetting, // 页文件设置
        Win32_BootConfiguration, // 系统启动配置
        Win32_ComputerSystem,  // 计算机信息简要
        Win32_OperatingSystem, // 操作系统信息
        Win32_StartupCommand,  // 系统自动启动程序
        Win32_Service,     // 系统安装的服务
        Win32_Group,      // 系统管理组
        Win32_GroupUser,    // 系统组帐号
        Win32_UserAccount,   // 用户帐号
        Win32_Process,     // 系统进程
        Win32_Thread,      // 系统线程
        Win32_Share,      // 共享
        Win32_NetworkClient,  // 已安装的网络客户端
        Win32_NetworkProtocol, // 已安装的网络协议
    }
    internal class EnumTools {
    }
}