# MineSweeperWPF 🎯

基于 WPF 和 .NET 8.0 开发的经典扫雷游戏。

![C#](https://img.shields.io/badge/C%23-.NET%208.0-blue)
![License](https://img.shields.io/badge/License-MIT-green)

## 特性

- ✅ 经典扫雷玩法
- ✅ 三种难度级别（简单、中等、困难）
- ✅ 首次点击安全保护
- ✅ 右键插旗标记地雷
- ✅ 数字格子的智能揭开的快捷操作
- ✅ Windows 原生界面

## 游戏规则

1. **左键点击**：揭开格子
2. **右键点击**：标记/取消标记地雷
3. **数字含义**：表示周围8个格子中的地雷数量
4. **胜利条件**：揭开所有非地雷格子
5. **失败条件**：点击到地雷

## 难度设置

| 难度 | 格子大小 | 地雷数量 |
|------|---------|---------|
| 简单 | 10×10 | 10 |
| 中等 | 15×15 | 40 |
| 困难 | 20×20 | 99 |

## 技术栈

- **.NET 8.0** - 运行时
- **WPF** - UI 框架
- **C# 12** - 编程语言

## 构建与运行

### 前置要求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Windows 10/11

### 编译步骤

```bash
# 克隆仓库
git clone https://github.com/saderickhuang/MineSweeperWPF.git
cd MineSweeperWPF

# 还原依赖
dotnet restore

# 编译
dotnet build

# 运行
dotnet run
```

### 发布为可执行文件

```bash
dotnet publish -c Release -r win-x64 --self-contained false -o ./publish
```

## 项目结构

```
MineSweeperWPF/
├── App.xaml                    # 应用程序入口
├── MainWindow.xaml             # 主窗口
├── MineField.xaml.cs           # 雷区逻辑核心
├── MineButton.xaml.cs          # 格子控件
├── Enums.cs                    # 游戏枚举定义
├── Algo.cs                     # 算法（预留）
├── AssemblyInfo.cs              # 程序集信息
├── Resources/                  # 游戏资源图片
│   ├── covered.png             # 覆盖状态
│   ├── flag.png                # 插旗状态
│   ├── mine.png                # 地雷
│   ├── num_0~8.png             # 数字1-8
│   ├── normal.png              # 游戏中
│   ├── win.png                 # 胜利
│   └── lost.png                # 失败
└── CODE_REVIEW.md              # 代码审查报告
```

## 代码审查

详见 [CODE_REVIEW.md](./CODE_REVIEW.md)

## 贡献指南

欢迎提交 Pull Request！

1. Fork 本仓库
2. 创建特性分支 (`git checkout -b feature/xxx`)
3. 提交更改
4. 推送到远程仓库
5. 创建 Pull Request

## 许可证

MIT License - 查看 [LICENSE](LICENSE) 文件

---

**作者**: CodePoet  
**项目创建于**: 2024-02-20
