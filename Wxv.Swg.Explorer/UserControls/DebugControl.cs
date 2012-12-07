﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using Wxv.Swg.Common;

namespace Wxv.Swg.Explorer.UserControls
{
    public partial class DebugControl : ViewerControl
    {
        public DebugControl()
        {
            InitializeComponent();
        }

        public override void InitViewer(TREInfoFile treInfoFile)
        {
            base.InitViewer(treInfoFile);

            if (treInfoFile.FileType.DebugToString != null)
            {
                using (var writer = new StringWriter())
                {
                    treInfoFile.FileType.DebugToString(treInfoFile.Data, writer);
                    richTextBox.Text = writer.ToString();
                }
            }

        }
    }
}
