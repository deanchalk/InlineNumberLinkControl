using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace InlineTextLinkControlExampleFramework
{
    [TemplatePart(Name = "PART_TextBlock", Type = typeof(TextBlock))]
    public class TextNumberLinkControl : Control
    {
        private static readonly Regex _regex = new Regex(@"[0-9]{6}", RegexOptions.Compiled);

        public static readonly DependencyProperty LinkCommandProperty =
            DependencyProperty.Register("LinkCommand", typeof(ICommand), typeof(TextNumberLinkControl),
                new PropertyMetadata(null));

        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextNumberLinkControl),
                new PropertyMetadata(string.Empty, OnTextPropertyChanged));

        private TextBlock _textBlock;

        static TextNumberLinkControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextNumberLinkControl),
                new FrameworkPropertyMetadata(typeof(TextNumberLinkControl)));
        }

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public ICommand LinkCommand
        {
            get => (ICommand)GetValue(LinkCommandProperty);
            set => SetValue(LinkCommandProperty, value);
        }

        private static void OnTextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lControl = d as TextNumberLinkControl;
            if (!(d is TextNumberLinkControl) || lControl._textBlock == null) return;
            lControl.ResetText();

            // if (d is not TextNumberLinkControl lControl || lControl._textBlock == null) return;
            // lControl.ResetText();
        }

        private void ResetText()
        {
            _textBlock.Inlines.Clear();
            if (string.IsNullOrWhiteSpace(Text)) return;
            var matches = _regex.Matches(Text);
            var cursor = 0;
            foreach (var match in matches.OfType<Match>())
            {
                var text = Text.Substring(cursor, match.Index - cursor);
                _textBlock.Inlines.Add(new Run(text));
                var link = new Hyperlink();
                link.Inlines.Add(new TextBlock { Text = match.Value });
                link.Tag = int.Parse(match.Value);
                link.Click += OnLinkClick;
                _textBlock.Inlines.Add(link);
                cursor = match.Index + 6;
            }
        }

        private void OnLinkClick(object sender, RoutedEventArgs e)
        {
            var linkVal = (sender as Hyperlink)?.Tag;
            if (linkVal == null || LinkCommand == null) return;
            LinkCommand.Execute((int)linkVal);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _textBlock = GetTemplateChild("PART_TextBlock") as TextBlock;
            if (_textBlock != null) ResetText();
        }
    }
}