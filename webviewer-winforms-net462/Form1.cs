using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace webviewer_winforms_net462
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var options = new CoreWebView2EnvironmentOptions();
            options.AdditionalBrowserArguments = "--disable-web-security";

            var environment = await CoreWebView2Environment.CreateAsync(null, null, options);
            await webView2.EnsureCoreWebView2Async(environment);

            UriBuilder uriBuilder = new UriBuilder(Path.Combine(Directory.GetCurrentDirectory(), "webviewer/index.html"));
            webView2.Source = uriBuilder.Uri;
        }
    }
}
