using System.Diagnostics.CodeAnalysis;

namespace ProjektKatastrophenschutz.Utils.Windows
{
    /// <summary>
    ///     Just a small class with some constant values needed for windows calls.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class WinConstants
    {
        public const uint WM_CHAR = 0x102;
        public const uint WM_SETTEXT = 0xC;
        public const uint WM_KEYDOWN = 0x100;
        public const uint WM_KEYUP = 0x101;
        public const uint WM_LBUTTONDOWN = 0x201;
        public const uint WM_LBUTTONUP = 0x202;
        public const uint WM_CLOSE = 0x10;
        public const uint WM_COMMAND = 0x111;
        public const uint WM_CLEAR = 0x303;
        public const uint WM_DESTROY = 0x2;
        public const uint WM_GETTEXT = 0xD;
        public const uint WM_LBUTTONDBLCLK = 0x203;
        public const uint WM_GETTEXTLENGTH = 0x000E;
        public const uint WM_SYSCOMMAND = 0x0112;

        public const int GWL_ID = (-12);
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);

        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        public const int GW_ENABLEDPOPUP = 6;
        public const int GW_MAX = 6;

        public const uint WS_OVERLAPPED = 0;
        public const uint WS_POPUP = 0x80000000;
        public const uint WS_CHILD = 0x40000000;
        public const uint WS_MINIMIZE = 0x20000000;
        public const uint WS_VISIBLE = 0x10000000;
        public const uint WS_DISABLED = 0x8000000;
        public const uint WS_CLIPSIBLINGS = 0x4000000;
        public const uint WS_CLIPCHILDREN = 0x2000000;
        public const uint WS_MAXIMIZE = 0x1000000;
        public const uint WS_CAPTION = 0xC00000;     // WS_BORDER or WS_DLGFRAME  
        public const uint WS_BORDER = 0x800000;
        public const uint WS_DLGFRAME = 0x400000;
        public const uint WS_VSCROLL = 0x200000;
        public const uint WS_HSCROLL = 0x100000;
        public const uint WS_SYSMENU = 0x80000;
        public const uint WS_THICKFRAME = 0x40000;
        public const uint WS_GROUP = 0x20000;
        public const uint WS_TABSTOP = 0x10000;
        public const uint WS_MINIMIZEBOX = 0x20000;
        public const uint WS_MAXIMIZEBOX = 0x10000;
        public const uint WS_TILED = WS_OVERLAPPED;
        public const uint WS_ICONIC = WS_MINIMIZE;
        public const uint WS_SIZEBOX = WS_THICKFRAME;
        public const uint WS_EX_TRANSPARENT = 0x00000020;

        public const uint BM_CLICK = 0x00F5;

        public static class WindowStyle
        {
            /// <summary>Hides the window and activates another window.</summary>
            /// <remarks>See SW_HIDE</remarks>
            public static uint Hide = 0;

            /// <summary>Activates and displays a window. If the window is minimized 
            /// or maximized, the system restores it to its original size and 
            /// position. An application should specify this flag when displaying 
            /// the window for the first time.</summary>
            /// <remarks>See SW_SHOWNORMAL</remarks>
            public static uint ShowNormal = 1;

            /// <summary>Activates the window and displays it as a minimized window.</summary>
            /// <remarks>See SW_SHOWMINIMIZED</remarks>
            public static uint ShowMinimized = 2;
            /// <summary>Activates the window and displays it as a maximized window.</summary>
            /// <remarks>See SW_SHOWMAXIMIZED</remarks>
            public static uint ShowMaximized = 3;

            /// <summary>Maximizes the specified window.</summary>
            /// <remarks>See SW_MAXIMIZE</remarks>
            public static uint Maximize = 3;

            /// <summary>Displays a window in its most recent size and position. 
            /// This value is similar to "ShowNormal", except the window is not 
            /// actived.</summary>
            /// <remarks>See SW_SHOWNOACTIVATE</remarks>
            public static uint ShowNormalNoActivate = 4;

            /// <summary>Activates the window and displays it in its current size 
            /// and position.</summary>
            /// <remarks>See SW_SHOW</remarks>
            public static uint Show = 5;

            /// <summary>Minimizes the specified window and activates the next 
            /// top-level window in the Z order.</summary>
            /// <remarks>See SW_MINIMIZE</remarks>
            public static uint Minimize = 6;

            /// <summary>Displays the window as a minimized window. This value is 
            /// similar to "ShowMinimized", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWMINNOACTIVE</remarks>
            public static uint ShowMinNoActivate = 7;

            /// <summary>Displays the window in its current size and position. This 
            /// value is similar to "Show", except the window is not activated.</summary>
            /// <remarks>See SW_SHOWNA</remarks>
            public static uint ShowNoActivate = 8;

            /// <summary>Activates and displays the window. If the window is 
            /// minimized or maximized, the system restores it to its original size 
            /// and position. An application should specify this flag when restoring 
            /// a minimized window.</summary>
            /// <remarks>See SW_RESTORE</remarks>
            public static uint Restore = 9;

            /// <summary>Sets the show state based on the SW_ value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.</summary>
            /// <remarks>See SW_SHOWDEFAULT</remarks>
            public static uint ShowDefault = 10;

            /// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
            /// that owns the window is hung. This flag should only be used when 
            /// minimizing windows from a different thread.</summary>
            /// <remarks>See SW_FORCEMINIMIZE</remarks>
            public static uint ForceMinimized = 11;
        }

        /// <summary>
        /// Standard arrow and small hourglass
        /// </summary>
        public const uint OCR_APPSTARTING = 32650;
        /// <summary>
        /// Standard arrow
        /// </summary>
        public const uint OCR_NORMAL = 32512;
        /// <summary>
        /// Crosshair
        /// </summary>
        public const uint OCR_CROSS = 32515;
        /// <summary>
        /// Windows 2000/XP: Hand
        /// </summary>
        public const uint OCR_HAND = 32649;
        /// <summary>
        /// Arrow and question mark
        /// </summary>
        public const uint OCR_HELP = 32651;
        /// <summary>
        /// I-beam
        /// </summary>
        public const uint OCR_IBEAM = 32513;
        /// <summary>
        /// Slashed circle
        /// </summary>
        public const uint OCR_NO = 32648;
        /// <summary>
        /// Four-pointed arrow pointing north; south; east; and west
        /// </summary>
        public const uint OCR_SIZEALL = 32646;
        /// <summary>
        /// Double-pointed arrow pointing northeast and southwest
        /// </summary>
        public const uint OCR_SIZENESW = 32643;
        /// <summary>
        /// Double-pointed arrow pointing north and south
        /// </summary>
        public const uint OCR_SIZENS = 32645;
        /// <summary>
        /// Double-pointed arrow pointing northwest and southeast
        /// </summary>
        public const uint OCR_SIZENWSE = 32642;
        /// <summary>
        /// Double-pointed arrow pointing west and east
        /// </summary>
        public const uint OCR_SIZEWE = 32644;
        /// <summary>
        /// Vertical arrow
        /// </summary>
        public const uint OCR_UP = 32516;
        /// <summary>
        /// Hourglass
        /// </summary>
        public const uint OCR_WAIT = 32514;

        public const uint IDC_ARROW = 32512;
        public const uint IDC_IBEAM = 32513;
        public const uint IDC_WAIT = 32514;
        public const uint IDC_CROSS = 32515;
        public const uint IDC_UPARROW = 32516;
        public const uint IDC_SIZE = 32640;
        public const uint IDC_ICON = 32641;
        public const uint IDC_SIZENWSE = 32642;
        public const uint IDC_SIZENESW = 32643;
        public const uint IDC_SIZEWE = 32644;
        public const uint IDC_SIZENS = 32645;
        public const uint IDC_SIZEALL = 32646;
        public const uint IDC_NO = 32648;
        public const uint IDC_APPSTARTING = 32650;
        public const uint IDC_HELP = 32651;

        public const int SC_SIZE = 0xF000;
        public const int SC_MOVE = 0xF010;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_NEXTWINDOW = 0xF040;
        public const int SC_PREVWINDOW = 0xF050;
        public const int SC_CLOSE = 0xF060;
        public const int SC_VSCROLL = 0xF070;
        public const int SC_HSCROLL = 0xF080;
        public const int SC_MOUSEMENU = 0xF090;
        public const int SC_KEYMENU = 0xF100;
        public const int SC_ARRANGE = 0xF110;
        public const int SC_RESTORE = 0xF120;
        public const int SC_TASKLIST = 0xF130;
        public const int SC_SCREENSAVE = 0xF140;
        public const int SC_HOTKEY = 0xF150;
        //#if(WINVER >= 0x0400) //Win95
        public const int SC_DEFAULT = 0xF160;
        public const int SC_MONITORPOWER = 0xF170;
        public const int SC_CONTEXTHELP = 0xF180;
        public const int SC_SEPARATOR = 0xF00F;
        //#endif /* WINVER >= 0x0400 */
        //#if(WINVER >= 0x0600) //Vista
        public const int SCF_ISSECURE = 0x00000001;
        //#endif /* WINVER >= 0x0600 */
        /*
        * Obsolete names
        */
        public const int SC_ICON = SC_MINIMIZE;
        public const int SC_ZOOM = SC_MAXIMIZE;

        // End

        public const uint MAXDWORD = 0xFFFFFFFF;

        public const uint FLASHW_ALL = 3;


        public enum DeviceKey
        {
            Win32_1394Controller,
            Win32_1394ControllerDevice,
            Win32_Account,
            Win32_AccountSID,
            Win32_ACE,
            Win32_ActionCheck,
            Win32_AllocatedResource,
            Win32_ApplicationCommandLine,
            Win32_ApplicationService,
            Win32_AssociatedBattery,
            Win32_AssociatedProcessorMemory,
            Win32_BaseBoard,
            Win32_BaseService,
            Win32_Battery,
            Win32_Binary,
            Win32_BindImageAction,
            Win32_BIOS,
            Win32_BootConfiguration,
            Win32_Bus,
            Win32_CacheMemory,
            Win32_CDROMDrive,
            Win32_CheckCheck,
            Win32_CIMLogicalDeviceCIMDataFile,
            Win32_ClassicCOMApplicationClasses,
            Win32_ClassicCOMClass,
            Win32_ClassicCOMClassSetting,
            Win32_ClassicCOMClassSettings,
            Win32_ClassInfoAction,
            Win32_ClientApplicationSetting,
            Win32_CodecFile,
            Win32_COMApplication,
            Win32_COMApplicationClasses,
            Win32_COMApplicationSettings,
            Win32_COMClass,
            Win32_ComClassAutoEmulator,
            Win32_ComClassEmulator,
            Win32_CommandLineAccess,
            Win32_ComponentCategory,
            Win32_ComputerSystem,
            Win32_ComputerSystemProcessor,
            Win32_ComputerSystemProduct,
            Win32_COMSetting,
            Win32_Condition,
            Win32_CreateFolderAction,
            Win32_CurrentProbe,
            Win32_DCOMApplication,
            Win32_DCOMApplicationAccessAllowedSetting,
            Win32_DCOMApplicationLaunchAllowedSetting,
            Win32_DCOMApplicationSetting,
            Win32_DependentService,
            Win32_Desktop,
            Win32_DesktopMonitor,
            Win32_DeviceBus,
            Win32_DeviceMemoryAddress,
            Win32_DeviceSettings,
            Win32_Directory,
            Win32_DirectorySpecification,
            Win32_DiskDrive,
            Win32_DiskDriveToDiskPartition,
            Win32_DiskPartition,
            Win32_DisplayConfiguration,
            Win32_DisplayControllerConfiguration,
            Win32_DMAChannel,
            Win32_DriverVXD,
            Win32_DuplicateFileAction,
            Win32_Environment,
            Win32_EnvironmentSpecification,
            Win32_ExtensionInfoAction,
            Win32_Fan,
            Win32_FileSpecification,
            Win32_FloppyController,
            Win32_FloppyDrive,
            Win32_FontInfoAction,
            Win32_Group,
            Win32_GroupUser,
            Win32_HeatPipe,
            Win32_IDEController,
            Win32_IDEControllerDevice,
            Win32_ImplementedCategory,
            Win32_InfraredDevice,
            Win32_IniFileSpecification,
            Win32_InstalledSoftwareElement,
            Win32_IRQResource,
            Win32_Keyboard,
            Win32_LaunchCondition,
            Win32_LoadOrderGroup,
            Win32_LoadOrderGroupServiceDependencies,
            Win32_LoadOrderGroupServiceMembers,
            Win32_LogicalDisk,
            Win32_LogicalDiskRootDirectory,
            Win32_LogicalDiskToPartition,
            Win32_LogicalFileAccess,
            Win32_LogicalFileAuditing,
            Win32_LogicalFileGroup,
            Win32_LogicalFileOwner,
            Win32_LogicalFileSecuritySetting,
            Win32_LogicalMemoryConfiguration,
            Win32_LogicalProgramGroup,
            Win32_LogicalProgramGroupDirectory,
            Win32_LogicalProgramGroupItem,
            Win32_LogicalProgramGroupItemDataFile,
            Win32_LogicalShareAccess,
            Win32_LogicalShareAuditing,
            Win32_LogicalShareSecuritySetting,
            Win32_ManagedSystemElementResource,
            Win32_MemoryArray,
            Win32_MemoryArrayLocation,
            Win32_MemoryDevice,
            Win32_MemoryDeviceArray,
            Win32_MemoryDeviceLocation,
            Win32_MethodParameterClass,
            Win32_MIMEInfoAction,
            Win32_MotherboardDevice,
            Win32_MoveFileAction,
            Win32_MSIResource,
            Win32_NetworkAdapter,
            Win32_NetworkAdapterConfiguration,
            Win32_NetworkAdapterSetting,
            Win32_NetworkClient,
            Win32_NetworkConnection,
            Win32_NetworkLoginProfile,
            Win32_NetworkProtocol,
            Win32_NTEventlogFile,
            Win32_NTLogEvent,
            Win32_NTLogEventComputer,
            Win32_NTLogEventLog,
            Win32_NTLogEventUser,
            Win32_ODBCAttribute,
            Win32_ODBCDataSourceAttribute,
            Win32_ODBCDataSourceSpecification,
            Win32_ODBCDriverAttribute,
            Win32_ODBCDriverSoftwareElement,
            Win32_ODBCDriverSpecification,
            Win32_ODBCSourceAttribute,
            Win32_ODBCTranslatorSpecification,
            Win32_OnBoardDevice,
            Win32_OperatingSystem,
            Win32_OperatingSystemQFE,
            Win32_OSRecoveryConfiguration,
            Win32_PageFile,
            Win32_PageFileElementSetting,
            Win32_PageFileSetting,
            Win32_PageFileUsage,
            Win32_ParallelPort,
            Win32_Patch,
            Win32_PatchFile,
            Win32_PatchPackage,
            Win32_PCMCIAController,
            Win32_Perf,
            Win32_PerfRawData,
            Win32_PerfRawData_ASP_ActiveServerPages,
            Win32_PerfRawData_ASPNET_114322_ASPNETAppsv114322,
            Win32_PerfRawData_ASPNET_114322_ASPNETv114322,
            Win32_PerfRawData_ASPNET_ASPNET,
            Win32_PerfRawData_ASPNET_ASPNETApplications,
            Win32_PerfRawData_IAS_IASAccountingClients,
            Win32_PerfRawData_IAS_IASAccountingServer,
            Win32_PerfRawData_IAS_IASAuthenticationClients,
            Win32_PerfRawData_IAS_IASAuthenticationServer,
            Win32_PerfRawData_InetInfo_InternetInformationServicesGlobal,
            Win32_PerfRawData_MSDTC_DistributedTransactionCoordinator,
            Win32_PerfRawData_MSFTPSVC_FTPService,
            Win32_PerfRawData_MSSQLSERVER_SQLServerAccessMethods,
            Win32_PerfRawData_MSSQLSERVER_SQLServerBackupDevice,
            Win32_PerfRawData_MSSQLSERVER_SQLServerBufferManager,
            Win32_PerfRawData_MSSQLSERVER_SQLServerBufferPartition,
            Win32_PerfRawData_MSSQLSERVER_SQLServerCacheManager,
            Win32_PerfRawData_MSSQLSERVER_SQLServerDatabases,
            Win32_PerfRawData_MSSQLSERVER_SQLServerGeneralStatistics,
            Win32_PerfRawData_MSSQLSERVER_SQLServerLatches,
            Win32_PerfRawData_MSSQLSERVER_SQLServerLocks,
            Win32_PerfRawData_MSSQLSERVER_SQLServerMemoryManager,
            Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationAgents,
            Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationDist,
            Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationLogreader,
            Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationMerge,
            Win32_PerfRawData_MSSQLSERVER_SQLServerReplicationSnapshot,
            Win32_PerfRawData_MSSQLSERVER_SQLServerSQLStatistics,
            Win32_PerfRawData_MSSQLSERVER_SQLServerUserSettable,
            Win32_PerfRawData_NETFramework_NETCLRExceptions,
            Win32_PerfRawData_NETFramework_NETCLRInterop,
            Win32_PerfRawData_NETFramework_NETCLRJit,
            Win32_PerfRawData_NETFramework_NETCLRLoading,
            Win32_PerfRawData_NETFramework_NETCLRLocksAndThreads,
            Win32_PerfRawData_NETFramework_NETCLRMemory,
            Win32_PerfRawData_NETFramework_NETCLRRemoting,
            Win32_PerfRawData_NETFramework_NETCLRSecurity,
            Win32_PerfRawData_Outlook_Outlook,
            Win32_PerfRawData_PerfDisk_PhysicalDisk,
            Win32_PerfRawData_PerfNet_Browser,
            Win32_PerfRawData_PerfNet_Redirector,
            Win32_PerfRawData_PerfNet_Server,
            Win32_PerfRawData_PerfNet_ServerWorkQueues,
            Win32_PerfRawData_PerfOS_Cache,
            Win32_PerfRawData_PerfOS_Memory,
            Win32_PerfRawData_PerfOS_Objects,
            Win32_PerfRawData_PerfOS_PagingFile,
            Win32_PerfRawData_PerfOS_Processor,
            Win32_PerfRawData_PerfOS_System,
            Win32_PerfRawData_PerfProc_FullImage_Costly,
            Win32_PerfRawData_PerfProc_Image_Costly,
            Win32_PerfRawData_PerfProc_JobObject,
            Win32_PerfRawData_PerfProc_JobObjectDetails,
            Win32_PerfRawData_PerfProc_Process,
            Win32_PerfRawData_PerfProc_ProcessAddressSpace_Costly,
            Win32_PerfRawData_PerfProc_Thread,
            Win32_PerfRawData_PerfProc_ThreadDetails_Costly,
            Win32_PerfRawData_RemoteAccess_RASPort,
            Win32_PerfRawData_RemoteAccess_RASTotal,
            Win32_PerfRawData_RSVP_ACSPerRSVPService,
            Win32_PerfRawData_Spooler_PrintQueue,
            Win32_PerfRawData_TapiSrv_Telephony,
            Win32_PerfRawData_Tcpip_ICMP,
            Win32_PerfRawData_Tcpip_IP,
            Win32_PerfRawData_Tcpip_NBTConnection,
            Win32_PerfRawData_Tcpip_NetworkInterface,
            Win32_PerfRawData_Tcpip_TCP,
            Win32_PerfRawData_Tcpip_UDP,
            Win32_PerfRawData_W3SVC_WebService,
            Win32_PhysicalMemory,
            Win32_PhysicalMemoryArray,
            Win32_PhysicalMemoryLocation,
            Win32_PNPAllocatedResource,
            Win32_PnPDevice,
            Win32_PnPEntity,
            Win32_PointingDevice,
            Win32_PortableBattery,
            Win32_PortConnector,
            Win32_PortResource,
            Win32_POTSModem,
            Win32_POTSModemToSerialPort,
            Win32_PowerManagementEvent,
            Win32_Printer,
            Win32_PrinterConfiguration,
            Win32_PrinterController,
            Win32_PrinterDriverDll,
            Win32_PrinterSetting,
            Win32_PrinterShare,
            Win32_PrintJob,
            Win32_PrivilegesStatus,
            Win32_Process,
            Win32_Processor,
            Win32_ProcessStartup,
            Win32_Product,
            Win32_ProductCheck,
            Win32_ProductResource,
            Win32_ProductSoftwareFeatures,
            Win32_ProgIDSpecification,
            Win32_ProgramGroup,
            Win32_ProgramGroupContents,
            Win32_ProgramGroupOrItem,
            Win32_Property,
            Win32_ProtocolBinding,
            Win32_PublishComponentAction,
            Win32_QuickFixEngineering,
            Win32_Refrigeration,
            Win32_Registry,
            Win32_RegistryAction,
            Win32_RemoveFileAction,
            Win32_RemoveIniAction,
            Win32_ReserveCost,
            Win32_ScheduledJob,
            Win32_SCSIController,
            Win32_SCSIControllerDevice,
            Win32_SecurityDescriptor,
            Win32_SecuritySetting,
            Win32_SecuritySettingAccess,
            Win32_SecuritySettingAuditing,
            Win32_SecuritySettingGroup,
            Win32_SecuritySettingOfLogicalFile,
            Win32_SecuritySettingOfLogicalShare,
            Win32_SecuritySettingOfObject,
            Win32_SecuritySettingOwner,
            Win32_SelfRegModuleAction,
            Win32_SerialPort,
            Win32_SerialPortConfiguration,
            Win32_SerialPortSetting,
            Win32_Service,
            Win32_ServiceControl,
            Win32_ServiceSpecification,
            Win32_ServiceSpecificationService,
            Win32_SettingCheck,
            Win32_Share,
            Win32_ShareToDirectory,
            Win32_ShortcutAction,
            Win32_ShortcutFile,
            Win32_ShortcutSAP,
            Win32_SID,
            Win32_SMBIOSMemory,
            Win32_SoftwareElement,
            Win32_SoftwareElementAction,
            Win32_SoftwareElementCheck,
            Win32_SoftwareElementCondition,
            Win32_SoftwareElementResource,
            Win32_SoftwareFeature,
            Win32_SoftwareFeatureAction,
            Win32_SoftwareFeatureCheck,
            Win32_SoftwareFeatureParent,
            Win32_SoftwareFeatureSoftwareElements,
            Win32_SoundDevice,
            Win32_StartupCommand,
            Win32_SubDirectory,
            Win32_SystemAccount,
            Win32_SystemBIOS,
            Win32_SystemBootConfiguration,
            Win32_SystemDesktop,
            Win32_SystemDevices,
            Win32_SystemDriver,
            Win32_SystemDriverPNPEntity,
            Win32_SystemEnclosure,
            Win32_SystemLoadOrderGroups,
            Win32_SystemLogicalMemoryConfiguration,
            Win32_SystemMemoryResource,
            Win32_SystemNetworkConnections,
            Win32_SystemOperatingSystem,
            Win32_SystemPartitions,
            Win32_SystemProcesses,
            Win32_SystemProgramGroups,
            Win32_SystemResources,
            Win32_SystemServices,
            Win32_SystemSetting,
            Win32_SystemSlot,
            Win32_SystemSystemDriver,
            Win32_SystemTimeZone,
            Win32_SystemUsers,
            Win32_TapeDrive,
            Win32_TemperatureProbe,
            Win32_Thread,
            Win32_TimeZone,
            Win32_Trustee,
            Win32_TypeLibraryAction,
            Win32_UninterruptiblePowerSupply,
            Win32_USBController,
            Win32_USBControllerDevice,
            Win32_UserAccount,
            Win32_UserDesktop,
            Win32_VideoConfiguration,
            Win32_VideoController,
            Win32_VideoSettings,
            Win32_VoltageProbe,
            Win32_WMIElementSetting,
            Win32_WMISetting
        }

        public class VirtualKey
        {
            /*
            * Virtual Keys, Standard Set */
            public const uint VK_LBUTTON = 0x01;
            public const uint VK_RBUTTON = 0x02;
            public const uint VK_CANCEL = 0x03;
            public const uint VK_MBUTTON = 0x04;    /* NOT contiguous with L & RBUTTON */

            //#if(_WIN32_WINNT >= 0x0500)
            public const uint VK_XBUTTON1 = 0x05;    /* NOT contiguous with L & RBUTTON */
            public const uint VK_XBUTTON2 = 0x06;    /* NOT contiguous with L & RBUTTON */
                                                     //#endif /* _WIN32_WINNT >= 0x0500 */
                                                     /*
                                                     * 0x07 : unassigned */
            public const uint VK_BACK = 0x08;
            public const uint VK_TAB = 0x09;

            /*
            * 0x0A - 0x0B : reserved */
            public const uint VK_CLEAR = 0x0C;
            public const uint VK_RETURN = 0x0D;

            public const uint VK_SHIFT = 0x10;
            public const uint VK_CONTROL = 0x11;
            public const uint VK_MENU = 0x12;
            public const uint VK_PAUSE = 0x13;
            public const uint VK_CAPITAL = 0x14;

            public const uint VK_KANA = 0x15;
            public const uint VK_HANGEUL = 0x15;  /* old name - should be here for compatibility */
            public const uint VK_HANGUL = 0x15;
            public const uint VK_JUNJA = 0x17;
            public const uint VK_FINAL = 0x18;
            public const uint VK_HANJA = 0x19;
            public const uint VK_KANJI = 0x19;

            public const uint VK_ESCAPE = 0x1B;

            public const uint VK_CONVERT = 0x1C;
            public const uint VK_NONCONVERT = 0x1D;
            public const uint VK_ACCEPT = 0x1E;
            public const uint VK_MODECHANGE = 0x1F;

            public const uint VK_SPACE = 0x20;
            public const uint VK_PRIOR = 0x21;
            public const uint VK_NEXT = 0x22;
            public const uint VK_END = 0x23;
            public const uint VK_HOME = 0x24;
            public const uint VK_LEFT = 0x25;
            public const uint VK_UP = 0x26;
            public const uint VK_RIGHT = 0x27;
            public const uint VK_DOWN = 0x28;
            public const uint VK_SELECT = 0x29;
            public const uint VK_PRINT = 0x2A;
            public const uint VK_EXECUTE = 0x2B;
            public const uint VK_SNAPSHOT = 0x2C;
            public const uint VK_INSERT = 0x2D;
            public const uint VK_DELETE = 0x2E;
            public const uint VK_HELP = 0x2F;

            /*
            public const uint VK_LWIN = 0x5B;CII '0' - '9' (0x30 - 0x39)
            * 0x40 : unassigned * VK_A - VK_Z are the same as ASCII 'A' - 'Z' (0x41 - 0x5A) */
            public const uint VK_LWIN = 0x5B;
            public const uint VK_RWIN = 0x5C;
            public const uint VK_APPS = 0x5D;

            /*
            * 0x5E : reserved */
            public const uint VK_SLEEP = 0x5F;

            public const uint VK_NUMPAD0 = 0x60;
            public const uint VK_NUMPAD1 = 0x61;
            public const uint VK_NUMPAD2 = 0x62;
            public const uint VK_NUMPAD3 = 0x63;
            public const uint VK_NUMPAD4 = 0x64;
            public const uint VK_NUMPAD5 = 0x65;
            public const uint VK_NUMPAD6 = 0x66;
            public const uint VK_NUMPAD7 = 0x67;
            public const uint VK_NUMPAD8 = 0x68;
            public const uint VK_NUMPAD9 = 0x69;
            public const uint VK_MULTIPLY = 0x6A;
            public const uint VK_ADD = 0x6B;
            public const uint VK_SEPARATOR = 0x6C;
            public const uint VK_SUBTRACT = 0x6D;
            public const uint VK_DECIMAL = 0x6E;
            public const uint VK_DIVIDE = 0x6F;
            public const uint VK_F1 = 0x70;
            public const uint VK_F2 = 0x71;
            public const uint VK_F3 = 0x72;
            public const uint VK_F4 = 0x73;
            public const uint VK_F5 = 0x74;
            public const uint VK_F6 = 0x75;
            public const uint VK_F7 = 0x76;
            public const uint VK_F8 = 0x77;
            public const uint VK_F9 = 0x78;
            public const uint VK_F10 = 0x79;
            public const uint VK_F11 = 0x7A;
            public const uint VK_F12 = 0x7B;
            public const uint VK_F13 = 0x7C;
            public const uint VK_F14 = 0x7D;
            public const uint VK_F15 = 0x7E;
            public const uint VK_F16 = 0x7F;
            public const uint VK_F17 = 0x80;
            public const uint VK_F18 = 0x81;
            public const uint VK_F19 = 0x82;
            public const uint VK_F20 = 0x83;
            public const uint VK_F21 = 0x84;
            public const uint VK_F22 = 0x85;
            public const uint VK_F23 = 0x86;
            public const uint VK_F24 = 0x87;

            /*
            * 0x88 - 0x8F : unassigned */
            public const uint VK_NUMLOCK = 0x90;
            public const uint VK_SCROLL = 0x91;

            /*
            * NEC PC-9800 kbd definitions */
            public const uint VK_OEM_NEC_EQUAL = 0x92;   // '=' key on numpad

            /*
            * Fujitsu/OASYS kbd definitions */
            public const uint VK_OEM_FJ_JISHO = 0x92;   // 'Dictionary' key
            public const uint VK_OEM_FJ_MASSHOU = 0x93;   // 'Unregister word' key
            public const uint VK_OEM_FJ_TOUROKU = 0x94;   // 'Register word' key
            public const uint VK_OEM_FJ_LOYA = 0x95;   // 'Left OYAYUBI' key
            public const uint VK_OEM_FJ_ROYA = 0x96;   // 'Right OYAYUBI' key

            /*
            * 0x97 - 0x9F : unassigned */
            /*
            *VK_L* & VK_R* - left and right Alt, Ctrl and Shift virtual keys. * Used only as parameters to GetAsyncKeyState() and GetKeyState(). * 
            No other API or message will distinguish left and right keys in this way. */
            public const uint VK_LSHIFT = 0xA0;
            public const uint VK_RSHIFT = 0xA1;
            public const uint VK_LCONTROL = 0xA2;
            public const uint VK_RCONTROL = 0xA3;
            public const uint VK_LMENU = 0xA4;
            public const uint VK_RMENU = 0xA5;

            //#if(_WIN32_WINNT >= 0x0500)
            public const uint VK_BROWSER_BACK = 0xA6;
            public const uint VK_BROWSER_FORWARD = 0xA7;
            public const uint VK_BROWSER_REFRESH = 0xA8;
            public const uint VK_BROWSER_STOP = 0xA9;
            public const uint VK_BROWSER_SEARCH = 0xAA;
            public const uint VK_BROWSER_FAVORITES = 0xAB;
            public const uint VK_BROWSER_HOME = 0xAC;

            public const uint VK_VOLUME_MUTE = 0xAD;
            public const uint VK_VOLUME_DOWN = 0xAE;
            public const uint VK_VOLUME_UP = 0xAF;
            public const uint VK_MEDIA_NEXT_TRACK = 0xB0;
            public const uint VK_MEDIA_PREV_TRACK = 0xB1;
            public const uint VK_MEDIA_STOP = 0xB2;
            public const uint VK_MEDIA_PLAY_PAUSE = 0xB3;
            public const uint VK_LAUNCH_MAIL = 0xB4;
            public const uint VK_LAUNCH_MEDIA_SELECT = 0xB5;
            public const uint VK_LAUNCH_APP1 = 0xB6;
            public const uint VK_LAUNCH_APP2 = 0xB7;

            //#endif /* _WIN32_WINNT >= 0x0500 */

            /*
            * 0xB8 - 0xB9 : reserved */
            public const uint VK_OEM_1 = 0xBA;   // ';:' for US
            public const uint VK_OEM_PLUS = 0xBB;   // '+' any country
            public const uint VK_OEM_COMMA = 0xBC;   // ',' any country
            public const uint VK_OEM_MINUS = 0xBD;   // '-' any country
            public const uint VK_OEM_PERIOD = 0xBE;   // '.' any country
            public const uint VK_OEM_2 = 0xBF;   // '/?' for US
            public const uint VK_OEM_3 = 0xC0;   // '`~' for US

            /*
            * 0xC1 - 0xD7 : reserved */
            /*
            * 0xD8 - 0xDA : unassigned */
            public const uint VK_OEM_4 = 0xDB;  //  '[{' for US
            public const uint VK_OEM_5 = 0xDC;  //  '\|' for US
            public const uint VK_OEM_6 = 0xDD;  //  ']}' for US
            public const uint VK_OEM_7 = 0xDE;  //  ''"' for US
            public const uint VK_OEM_8 = 0xDF;

            /*
            * 0xE0 : reserved */
            /*
            * Various extended or enhanced keyboards */
            public const uint VK_OEM_AX = 0xE1;  //  'AX' key on Japanese AX kbd
            public const uint VK_OEM_102 = 0xE2;  //  "<>" or "\|" on RT 102-key kbd.
            public const uint VK_ICO_HELP = 0xE3;  //  Help key on ICO
            public const uint VK_ICO_00 = 0xE4;  //  00 key on ICO

            //#if(WINVER >= 0x0400)
            public const uint VK_PROCESSKEY = 0xE5;
            //#endif /* WINVER >= 0x0400 */

            public const uint VK_ICO_CLEAR = 0xE6;

            //#if(_WIN32_WINNT >= 0x0500)
            public const uint VK_PACKET = 0xE7;
            //#endif /* _WIN32_WINNT >= 0x0500 */

            /*
            * 0xE8 : unassigned */
            /*
            * Nokia/Ericsson definitions */
            public const uint VK_OEM_RESET = 0xE9;
            public const uint VK_OEM_JUMP = 0xEA;
            public const uint VK_OEM_PA1 = 0xEB;
            public const uint VK_OEM_PA2 = 0xEC;
            public const uint VK_OEM_PA3 = 0xED;
            public const uint VK_OEM_WSCTRL = 0xEE;
            public const uint VK_OEM_CUSEL = 0xEF;
            public const uint VK_OEM_ATTN = 0xF0;
            public const uint VK_OEM_FINISH = 0xF1;
            public const uint VK_OEM_COPY = 0xF2;
            public const uint VK_OEM_AUTO = 0xF3;
            public const uint VK_OEM_ENLW = 0xF4;
            public const uint VK_OEM_BACKTAB = 0xF5;

            public const uint VK_ATTN = 0xF6;
            public const uint VK_CRSEL = 0xF7;
            public const uint VK_EXSEL = 0xF8;
            public const uint VK_EREOF = 0xF9;
            public const uint VK_PLAY = 0xFA;
            public const uint VK_ZOOM = 0xFB;
            public const uint VK_NONAME = 0xFC;
            public const uint VK_PA1 = 0xFD;
            public const uint VK_OEM_CLEAR = 0xFE;

            /*
            * 0xFF : reserved */
            /* missing letters and numbers for convenience*/
            public static int VK_0 = 0x30;
            public static int VK_1 = 0x31;
            public static int VK_2 = 0x32;
            public static int VK_3 = 0x33;
            public static int VK_4 = 0x34;
            public static int VK_5 = 0x35;
            public static int VK_6 = 0x36;
            public static int VK_7 = 0x37;
            public static int VK_8 = 0x38;
            public static int VK_9 = 0x39;

            /* 0x40 : unassigned*/
            public static int VK_A = 0x41;
            public static int VK_B = 0x42;
            public static int VK_C = 0x43;
            public static int VK_D = 0x44;
            public static int VK_E = 0x45;
            public static int VK_F = 0x46;
            public static int VK_G = 0x47;
            public static int VK_H = 0x48;
            public static int VK_I = 0x49;
            public static int VK_J = 0x4A;
            public static int VK_K = 0x4B;
            public static int VK_L = 0x4C;
            public static int VK_M = 0x4D;
            public static int VK_N = 0x4E;
            public static int VK_O = 0x4F;
            public static int VK_P = 0x50;
            public static int VK_Q = 0x51;
            public static int VK_R = 0x52;
            public static int VK_S = 0x53;
            public static int VK_T = 0x54;
            public static int VK_U = 0x55;
            public static int VK_V = 0x56;
            public static int VK_W = 0x57;
            public static int VK_X = 0x58;
            public static int VK_Y = 0x59;
            public static int VK_Z = 0x5A;
        }
    }
}
