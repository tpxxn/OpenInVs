# OpenInVs
在 Fork 中使用该项目可以搜索项目目录中的sln和csproj文件并用 vs 打开

## 使用方法
* 打开 Fork-Preferences-Custom Commands
* 点击左下角的加号(+)选择 Add Repository Custom Command 选项
* Title 填写 Visual Studio
* Script Path 填写本项目生成的可执行文件路径
* Parameters 填写参数，第一个参数为 VS 路径（devenv.exe），第二个参数为 $path（在 Fork 中用来指代项目根目录），第三个可选参数为搜索选项，可以填写 SlnOnly/CsprojOnly/All