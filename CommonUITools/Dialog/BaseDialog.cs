﻿using CommonUITools.Utils;
using ModernWpf.Controls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace CommonUITools.View;

public class BaseDialog : ContentDialog {
    public static readonly DependencyProperty DetailTextProperty = DependencyProperty.Register("DetailText", typeof(string), typeof(BaseDialog), new PropertyMetadata(""));
    /// <summary>
    /// DetailText
    /// </summary>
    public string DetailText {
        get { return (string)GetValue(DetailTextProperty); }
        set { SetValue(DetailTextProperty, value); }
    }

    public BaseDialog(
        string title = "",
        string detailText = "",
        string okButtonText = "确定",
        string cancelButtonText = "关闭"
    ) {
        Title = title;
        DetailText = detailText;
        PrimaryButtonText = okButtonText;
        CloseButtonText = cancelButtonText;
        Init();
        if (TryFindResource("GlobalDefaultButtonStyle") is Style closeButtonStyle) {
            CloseButtonStyle = closeButtonStyle;
        }
        // 设置 ScaleAnimation
        Opened += (dialog, _) => TaskUtils.EnsureCalledOnce((dialog, Application.Current), () => {
            Utils.ScaleAnimationHelper.SetIsEnabled((DependencyObject)dialog.Content, true);
            Utils.ScaleAnimationHelper.SetScaleOption((DependencyObject)dialog.Content, Utils.ScaleAnimationHelper.ScaleOption.Center);
        });
    }

    public BaseDialog() : this("") { }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init() {
        DataContext = this;
        #region 设置 TitleTemplate
        DataTemplate dataTemplate = new DataTemplate();
        #region SimpleStackPanel
        FrameworkElementFactory simpleStackPanel = new FrameworkElementFactory(typeof(SimpleStackPanel));
        simpleStackPanel.SetValue(SimpleStackPanel.OrientationProperty, Orientation.Horizontal);
        simpleStackPanel.SetValue(SimpleStackPanel.SpacingProperty, 8.0);
        #region TextBlock
        FrameworkElementFactory textblock = new FrameworkElementFactory(typeof(TextBlock));
        textblock.SetValue(TextBlock.StyleProperty, Application.Current.Resources["ContentDialogTitleStyle"]);
        textblock.SetBinding(TextBlock.TextProperty, new Binding("Title") {
            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(BaseDialog), 1)
        });
        #endregion
        simpleStackPanel.AppendChild(textblock);
        #endregion
        dataTemplate.VisualTree = simpleStackPanel;
        TitleTemplate = dataTemplate;
        #endregion
    }
}

public class DialogResult<T> {
    public ContentDialogResult Result { get; set; }
    public T? Data { get; set; } = default;

    public override string ToString() {
        return $"{nameof(Result)}: {Result}; {nameof(Data)}: {Data}";
    }
}
