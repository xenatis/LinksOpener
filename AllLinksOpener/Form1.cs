using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using HtmlAgilityPack;

namespace AllLinksOpener
{
	public partial class Form1 : Form
	{
		Timer tmr = new Timer();
		List<string> _urls = new List<string>();
		public Form1()
		{
			InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			tmr.Tick += new EventHandler(tmr_Tick);
		}

		void tmr_Tick(object sender, EventArgs e)
		{
			string txt = Clipboard.GetText();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ExtractLinks();
			FillList();
		}

		private void FillList()
		{
			listBox1.DataSource = null;
			listBox1.DataSource = _urls;
		}

		private void ExtractLinks()
		{
			string txt = Clipboard.GetText(TextDataFormat.Html);
			
			HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
			doc.LoadHtml(txt);

			HtmlAgilityPack.HtmlNodeCollection coll = doc.DocumentNode.SelectNodes("//a");
			if (coll != null)
			{
				foreach (HtmlAgilityPack.HtmlNode nd in coll)
				{
					string url = nd.Attributes["href"].Value;
					if (!_urls.Contains(url))
						_urls.Add(url);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			_urls.Clear();
			FillList();
		}

		int _pos = 0;
		private void button3_Click(object sender, EventArgs e)
		{
			//foreach (string url in _urls)
			for (int i = _pos; i < 5 + _pos; i++)
			{
				if (i < listBox1.Items.Count)
				{
					string url = listBox1.Items[i].ToString();
					Process.Start(url);
					listBox1.SelectedIndex = i;
				}
			}
			_pos += 5;
			lblPos.Text = _pos.ToString();
		}
	}
}
