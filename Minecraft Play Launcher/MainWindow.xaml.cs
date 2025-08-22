using KMCCC.Launcher;
using System.Windows;
using System.Windows.Controls;
using KMCCC.Authentication;
using System.IO;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Minecraft_Play_Launcher;
using Minecraft_Play_Launcher.resource;
using SquareMinecraftLauncher;
using SquareMinecraftLauncherWPF;
using SquareMinecraftLauncher.Minecraft;
using Brush = System.Windows.Media.Brush;

namespace Minecarft_Play_Launcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly LauncherCore Core = LauncherCore.Create();
        private const string _configJson = "config.json";
        private readonly string settingPath = @"mpl.json";
        private Brush DefaultBackground;


        private readonly Resource _resource = JsonResource.LoadOrDefault(_configJson, new Resource
        {
            AppInfo = new AppInfo
            {
                BackGround = ""
            },
            StartOption = StartOption.CreateEmptyOption()
        });
        
        
        
        public MainWindow()
        {
            InitializeComponent();
            //版本
            var versions = Core.GetVersions();
            versionCombo.ItemsSource = versions;
            //选择
            MemoryTextBox.Text = _resource.StartOption.MaxRuntimeMemory.ToString();
            filePath.Text = _resource.StartOption.JavaPath;
            IdBox.Text = _resource.StartOption.Name;
            DefaultBackground = StartGrid.Background;
            UpdateBackground();
        }

        private void GameStart()
        {
            _resource.StartOption.Set(
                IdBox.Text,
                filePath.Text,
                this.Try(() => Convert.ToInt32(MemoryTextBox.Text))
                );

            Core.JavaPath = _resource.StartOption.JavaPath;
            var result = Core.Launch(new LaunchOptions
            {
                Version = (KMCCC.Launcher.Version)versionCombo.SelectedItem,
                MaxMemory = _resource.StartOption.MaxRuntimeMemory,
                Authenticator = new OfflineAuthenticator(_resource.StartOption.Name)
            });


            _resource.StartOption.Store(settingPath);
            _resource.Store(_configJson);
        }
        private void StartClick(object sender, RoutedEventArgs e)
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

        private void SwitchDefaultBackground(object sender, RoutedEventArgs e)
        {
            lujing.Text = "/2023050447m92i.webp";
            _resource.AppInfo.BackGround = "/2023050447m92i.webp";
            UpdateBackground(DefaultBackground);
        }

        private void SwitchBackground(object sender, RoutedEventArgs e)
        {
            // 创建 OpenFileDialog 实例（WPF 下使用 Microsoft.Win32.OpenFileDialog）
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                // 设置对话框标题
                Title = "请选择一个文件",
                // 可选：设置文件类型过滤器 —— 注意修正你的写法
                Filter = "所有文件 (*.*)|*.*",
                FilterIndex = 1, // 默认选中第一个（即 *.exe）
                // 不允许多选
                Multiselect = false
            };

            // 显示对话框，并判断用户是否点击了“确定”
            if (openFileDialog.ShowDialog() != true) return;
            var selectedFilePath = openFileDialog.FileName;

            // ✅ 只更新 x:Name="lujing" 的 TextBlock
            lujing.Text = selectedFilePath;

            _resource.AppInfo.BackGround = selectedFilePath;
            UpdateBackground();
        }

        private void UpdateBackground()
        {
            _resource.AppInfo.Let(info =>
            {
                var background = this
                    .Try(() => new ImageBrush(new BitmapImage(new Uri(info!.BackGround))))
                    .OrElse(StartGrid.Background);
                UpdateBackground(background);
            });
        }

        private void UpdateBackground(Brush background)
        {
            StartGrid.Background = background;
            LoginGrid.Background = background;
            SettingGrid.Background = background;
        }
    }
}