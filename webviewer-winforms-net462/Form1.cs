using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Windows.Forms;

namespace webviewer_winforms_net462
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Load += Form1_Load;

            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem openItem = new ToolStripMenuItem("Open...", null, OnOpenClick);
            fileMenu.DropDownItems.Add(openItem);
            menuStrip.Items.Add(fileMenu);
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);
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

        private async void OnOpenClick(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Open a File";
                openFileDialog.Filter = "All Files (*.*)|*.*|PDF Files (*.pdf)|*.pdf|DOCX Files (*.docx)|*.docx|PowerPoint Files (*.pptx)|*.pptx";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string pdfPath = openFileDialog.FileName;
                    string pdfUrl = new Uri(pdfPath).AbsoluteUri;
                    string filePath = openFileDialog.FileName.Replace("\\", "/");
                    string script = $"window.webViewerInstance.UI.loadDocument('file://{filePath}');";
                    await webView2.CoreWebView2.ExecuteScriptAsync(script);
                }
            }
        }
    }
}
