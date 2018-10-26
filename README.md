# Luban-net

#### 项目介绍
.net version of Luban(鲁班)—Image compression with efficiency very close to WeChat Moments/可能是最接近微信朋友圈的图片压缩算法

## Luban.cs
```
//支持Web上传
Luban(HttpPostedFile hpf)
//支持本地文件
Luban(string path)

// 忽略压缩
public int IgnoreBy { get; set; } = 102400;
//质量
public int Quality { get; set; } = 60;

//压缩输出到新的文件
Compress(string outputPath)
```

>参考 [Luban-Py](https://github.com/Freefighter/Luban-Py)

## 结果
![测试](LubanSample/LubanSample/Images/20181026/测试.jpg)
