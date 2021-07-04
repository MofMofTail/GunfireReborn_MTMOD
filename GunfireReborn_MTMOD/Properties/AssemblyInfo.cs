using GunfireReborn_MTMOD;
using MelonLoader;
using System.Reflection;
using System.Runtime.InteropServices;

// 有关程序集的一般信息由以下
// 控制。更改这些特性值可修改
// 与程序集关联的信息。
[assembly: AssemblyTitle(GunfireReborn_MTMOD.BuildInfo.Description)]
[assembly: AssemblyDescription(GunfireReborn_MTMOD.BuildInfo.Description)]
[assembly: AssemblyCompany(GunfireReborn_MTMOD.BuildInfo.Company)]
[assembly: AssemblyProduct(GunfireReborn_MTMOD.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + GunfireReborn_MTMOD.BuildInfo.Author)]
[assembly: AssemblyTrademark(GunfireReborn_MTMOD.BuildInfo.Company)]
[assembly: AssemblyVersion(GunfireReborn_MTMOD.BuildInfo.Version)]
[assembly: AssemblyFileVersion(GunfireReborn_MTMOD.BuildInfo.Version)]
[assembly: MelonInfo(typeof(MTMOD), GunfireReborn_MTMOD.BuildInfo.Name, GunfireReborn_MTMOD.BuildInfo.Version, GunfireReborn_MTMOD.BuildInfo.Author, GunfireReborn_MTMOD.BuildInfo.DownloadLink)]
[assembly: MelonColor()]

// Create and Setup a MelonGame Attribute to mark a Melon as Universal or Compatible with specific Games.
// If no MelonGame Attribute is found or any of the Values for any MelonGame Attribute on the Melon is null or empty it will be assumed the Melon is Universal.
// Values for MelonGame Attribute can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame(null, null)]

// 将 ComVisible 设置为 false 会使此程序集中的类型
//对 COM 组件不可见。如果需要从 COM 访问此程序集中的类型
//请将此类型的 ComVisible 特性设置为 true。
[assembly: ComVisible(false)]

// 如果此项目向 COM 公开，则下列 GUID 用于类型库的 ID
[assembly: Guid("0060abb6-847b-43e7-8ed0-1b21906cd28e")]

// 程序集的版本信息由下列四个值组成: 
//
//      主版本
//      次版本
//      生成号
//      修订号
//
//可以指定所有这些值，也可以使用“生成号”和“修订号”的默认值
//通过使用 "*"，如下所示:
// [assembly: AssemblyVersion("1.0.*")]
