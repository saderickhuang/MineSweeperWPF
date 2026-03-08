# MineSweeperWPF Code Review

## 项目概述
这是一个基于 WPF 的经典扫雷游戏实现，使用 C# 和 .NET 8.0 开发。

---

## 🔴 高优先级问题

### 1. 未实现的异常抛出 (MainWindow.xaml.cs:31)
```csharp
private void MineButton_NumCellRightMouseBtnDown(object sender, RoutedEventArgs e)
{
    throw new NotImplementedException();
}
```
**问题**: 点击数字格子右键时抛出 `NotImplementedException`，导致游戏崩溃。  
**建议**: 实现这个功能或删除该方法。

### 2. 潜在的死循环风险 (MineField.xaml.cs:242-261)
`GenerateMines` 方法使用 `while` 循环但没有上限保护，当雷区几乎满了时可能陷入死循环。
```csharp
while (mineCount < levelMineCount[(int)currentLevel])
{
    // ... 随机布雷逻辑
}
```
**建议**: 添加最大尝试次数限制，或使用更高效的布雷算法（如洗牌算法）。

---

## 🟡 中优先级问题

### 3. 未使用的代码 (Algo.cs)
`Algo.cs` 中的 `GenerateMineField` 方法未被使用，且不完整（没有随机布雷功能）。

### 4. 未使用的命名空间 (MineField.xaml.cs:5)
```csharp
using System.DirectoryServices; // 实际未使用
```

### 5. 事件处理不一致 (MineButton.xaml.cs:89)
左键点击调用了 `OnMouseLeftButtonUp` 而不是 `OnMouseLeftButtonDown`，这可能导致意外的交互行为。

### 6. 空值检查缺失 (MineField.xaml.cs:91)
```csharp
var mineButton = (MineButton)sender; // 没有 null 检查
```

### 7. 重复代码模式
`uncoverAroundCells` 和 `FindNeighborButtons` 方法有大量重复的边界检查逻辑，可以提取为通用方法。

---

## 🟢 低优先级问题

### 8. 资源文件重复声明 (MineSweeperWPF.csproj)
`<None Remove>` 和 `<Resource>` 中有重复的文件列表，可以合并。

### 9. 缺少 XML 文档注释
大多数公共方法缺少文档注释。

### 10. 硬编码的单元格大小
`cellSize = 30` 硬编码在代码中，建议提取为配置项。

---

## ✨ 改进建议

1. **添加计时器功能** - 记录游戏耗时
2. **添加雷数计数器** - 显示剩余未标记的雷数
3. **添加首步保护** - 确保第一步不会踩雷（当前已有，但可以优化）
4. **添加游戏难度选择** - 已有基础实现，可添加自定义难度
5. **添加音效和动画** - 增强用户体验

---

## ✅ 代码亮点

- 使用 MVVM 模式的基本结构
- 自定义路由事件实现右键功能
- 递归展开空白格子的算法实现正确
- 首次点击后才生成雷区，公平性良好
