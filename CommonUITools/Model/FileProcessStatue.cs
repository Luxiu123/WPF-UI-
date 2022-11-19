﻿using System.Windows;

namespace CommonUITools.Model;

public enum ProcessResult {
    /// <summary>
    /// 未开始
    /// </summary>
    NotStarted,
    /// <summary>
    /// 处理中
    /// </summary>
    Processing,
    /// <summary>
    /// 暂停
    /// </summary>
    Paused,
    /// <summary>
    /// 终止
    /// </summary>
    Interrupted,
    /// <summary>
    /// 成功
    /// </summary>
    Successful,
    /// <summary>
    /// 失败
    /// </summary>
    Failed,
}

/// <summary>
/// 文件进度状态
/// </summary>
public class FileProcessStatus : DependencyObject {
    public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(FileProcessStatus), new PropertyMetadata(string.Empty));
    public static readonly DependencyProperty ProcessProperty = DependencyProperty.Register("Process", typeof(double), typeof(FileProcessStatus), new PropertyMetadata(0.0));
    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register("Status", typeof(ProcessResult), typeof(FileProcessStatus), new PropertyMetadata(ProcessResult.NotStarted));

    /// <summary>
    /// 文件名
    /// </summary>
    public string FileName {
        get { return (string)GetValue(FileNameProperty); }
        set { SetValue(FileNameProperty, value); }
    }
    /// <summary>
    /// 进度，[0 - 1]
    /// </summary>
    public double Process {
        get { return (double)GetValue(ProcessProperty); }
        set { SetValue(ProcessProperty, value); }
    }
    /// <summary>
    /// 状态
    /// </summary>
    public ProcessResult Status {
        get { return (ProcessResult)GetValue(StatusProperty); }
        set { SetValue(StatusProperty, value); }
    }
}