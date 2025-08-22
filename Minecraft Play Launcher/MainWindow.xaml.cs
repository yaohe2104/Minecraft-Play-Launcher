using KMCCC.Launcher;
using System.Windows;
using System.Windows.Controls;
using KMCCC.Authentication;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using Minecraft_Play_Launcher;
using SquareMinecraftLauncher;
using SquareMinecraftLauncherWPF;
using SquareMinecraftLauncher.Minecraft;

namespace Minecarft_Play_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static LauncherCore Core = LauncherCore.Create();
        string settingPath = @"mpl.json";
        GameOption option = GameOption.CreateEmptyOption();
        public void LauncherIntialization()
        {
            if (File.Exists(settingPath))
            {
                option = GameOption.ReadFromJson(settingPath);
            }
        }

        

        public MainWindow()
        {
            InitializeComponent();
            LauncherIntialization();
            //版本
            var versions = Core.GetVersions();
            versionCombo.ItemsSource = versions;
            //选择
            MemoryTextBox.Text = option.MaxRuntimeMemory;
            filePath.Text = option.JavaPath;
            IdBox.Text = option.Name;

            

        }
        public void GameStart()
        {


            option.Set(
                IdBox.Text,
                filePath.Text,
                MemoryTextBox.Text
                );

            Core.JavaPath = option.JavaPath;
            var result = Core.Launch(new LaunchOptions
            {
                Version = (KMCCC.Launcher.Version)versionCombo.SelectedItem,
                MaxMemory = Convert.ToInt32(option.MaxRuntimeMemory),
                Authenticator = new OfflineAuthenticator(option.Name)
            });


            option.Store(settingPath);
        }
        private void Stary_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
        }

        private void javaCombo_Click_1(object sender, RoutedEventArgs e)
        {
            // 创建 OpenFileDialog 实例（WPF 下使用 Microsoft.Win32.OpenFileDialog）
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();

            // 设置对话框标题
            openFileDialog.Title = "请选择一个文件（如 javaw.exe）";

            // 可选：设置文件类型过滤器 —— 注意修正你的写法
            openFileDialog.Filter = "可执行文件 (*.exe)|*.exe|所有文件 (*.*)|*.*";
            openFileDialog.FilterIndex = 1; // 默认选中第一个（即 *.exe）

            // 不允许多选
            openFileDialog.Multiselect = false;

            // 显示对话框，并判断用户是否点击了“确定”
            if (openFileDialog.ShowDialog() == true)
            {
                // 获取用户选择的文件的完整路径（含文件名和后缀）
                string selectedFilePath = openFileDialog.FileName;

                // 将该路径显示到名为 filePath 的 TextBox 控件中
                filePath.Text = selectedFilePath;

                // （可选）你也可以在这里把这个路径保存到某个变量中供后续使用
                // 比如：Core.JavaPath = selectedFilePath;
            }
                
        }

        private void moren_Click(object sender, RoutedEventArgs e)
        {
            lujing.Text = "/2023050447m92i.webp";
        }

        private void zidingyi_Click(object sender, RoutedEventArgs e)
        {
            // 创建 OpenFileDialog 实例（WPF 下使用 Microsoft.Win32.OpenFileDialog）
            var openFileDialog = new Microsoft.Win32.OpenFileDialog();

            // 设置对话框标题
            openFileDialog.Title = "请选择一个文件";

            // 可选：设置文件类型过滤器 —— 注意修正你的写法
            openFileDialog.Filter = "所有文件 (*.*)|*.*" ;
            openFileDialog.FilterIndex = 1; // 默认选中第一个（即 *.exe）

            // 不允许多选
            openFileDialog.Multiselect = false;

            // 显示对话框，并判断用户是否点击了“确定”
            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                // ✅ 只更新 x:Name="lujing" 的 TextBlock
                lujing.Text = selectedFilePath;
            }
        }
    }
}