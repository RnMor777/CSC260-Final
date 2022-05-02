using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC260_Final {
    internal class ColorSettings {
        private readonly System.Drawing.Color _nordRed = System.Drawing.Color.FromArgb(191,97,106);
        private readonly System.Drawing.Color _nordGreen = System.Drawing.Color.FromArgb(163,190,140);
        private readonly System.Drawing.Color _nordWhite = System.Drawing.Color.FromArgb(236,239,244);
        private readonly System.Drawing.Color _nordBlack = System.Drawing.Color.FromArgb(94,129,172);
        private readonly System.Drawing.Color _nordPrev = System.Drawing.Color.FromArgb(143,188,187);
        private readonly System.Drawing.Color _nordCurr = System.Drawing.Color.FromArgb(180,142,173);

        private readonly System.Drawing.Color _regRed = System.Drawing.Color.Crimson;
        private readonly System.Drawing.Color _regGreen = System.Drawing.Color.LimeGreen;
        private readonly System.Drawing.Color _regWhite = System.Drawing.Color.FromArgb(240,217,181);
        private readonly System.Drawing.Color _regBlack = System.Drawing.Color.FromArgb(181,136,99);
        private readonly System.Drawing.Color _regPrev = System.Drawing.Color.FromArgb(143,188,187);
        private readonly System.Drawing.Color _regCurr = System.Drawing.Color.Violet;

        private int _scheme;

        public System.Drawing.Color Red {
            get { return _scheme == 0 ? _regRed : _nordRed; }
        }

        public System.Drawing.Color Green {
            get { return _scheme == 0 ? _regGreen : _nordGreen; }
        }

        public System.Drawing.Color White {
            get { return _scheme == 0 ? _regWhite : _nordWhite; }
        }

        public System.Drawing.Color Black {
            get { return _scheme == 0 ? _regBlack : _nordBlack; }
        }

        public System.Drawing.Color Prev {
            get { return _scheme == 0 ? _regPrev : _nordPrev; }
        }

        public System.Drawing.Color Curr {
            get { return _scheme == 0 ? _regCurr : _nordCurr; }
        }

        public ColorSettings () {
            _scheme = 0;
        }

        public void ChangeColorScheme (int color) {
            _scheme = color;
        }
    }
}
