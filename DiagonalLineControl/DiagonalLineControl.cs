using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DiagonalLineControl
{
    /// <summary>
    /// 斜線を描画するコントロールです
    /// </summary>
    [DesignerCategory("Code")]
    public class DiagonalLineControl : Control
    {
        /// <summary>
        /// 斜線の方向を表す列挙対
        /// </summary>
        public enum Direction
        {
            LeftUpToRightDown,
            RightUpToLeftDown,
            Cross
        }

        private Color _lineColor = Color.Black;
        private float _lineWidth = 1.0f;
        private Direction _lineDirection = Direction.LeftUpToRightDown;
        private Pen _pen;

        /// <summary>
        /// 斜線の色
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("斜線の色を指定します。")]
        public Color LineColor
        {
            get => _lineColor;
            set
            {
                _lineColor = value;
                UpdatePen();
                Invalidate();
            }
        }

        /// <summary>
        /// 斜線の太さ
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("斜線の太さを指定します。")]
        public float LineWidth
        {
            get => _lineWidth;
            set
            {
                if (value <= 0) { throw new ArgumentOutOfRangeException(nameof(LineWidth), "LineWidth は 0 より大きい値を設定してください"); }
                _lineWidth = value;
                UpdatePen();
                Invalidate();
            }
        }

        /// <summary>
        /// 斜線の方向
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("斜め線の方向を指定します。")]
        public Direction LineDirection
        {
            get => _lineDirection;
            set
            {
                _lineDirection = value;
                Invalidate();
            }
        }

        protected override Size DefaultSize => new Size(100, 100);

        [Browsable(false)]
        public override string Text { get => string.Empty; }

        [Browsable(false)]
        public override bool AutoSize { get => false; }

        [Browsable(false)]
        public override Color BackColor { get => Color.Transparent; }

        public DiagonalLineControl()
        {
            this.Size = DefaultSize;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw |ControlStyles.SupportsTransparentBackColor, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_pen != null) { UpdatePen(); }

            switch (LineDirection)
            {
                case Direction.LeftUpToRightDown:
                    e.Graphics.DrawLine(_pen, 0, 0, Width, Height);
                    break;

                case Direction.RightUpToLeftDown:
                    e.Graphics.DrawLine(_pen, Width, 0, 0, Height);
                    break;

                case Direction.Cross:
                    e.Graphics.DrawLine(_pen, 0, 0, Width, Height);
                    e.Graphics.DrawLine(_pen, Width, 0, 0, Height);
                    break;
            }
        }

        private void UpdatePen()
        {
            _pen?.Dispose();
            _pen = new Pen(LineColor, LineWidth);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _pen?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
