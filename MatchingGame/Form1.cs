using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
	public partial class Form1 : Form
	{
		// --< автоматически созданный код
		public Form1()
		{
			InitializeComponent();
			РасстановкаВсехЗначков(); //вызов расстановки значков при открытии программы
		}
		// >--

		//код для работы программы

		/// <summary>
		/// объект рандом для случайного подбора значка в таблице.
		/// </summary>
		Random рндм = new Random();
		/// <summary>
		/// коллекция со всеми 16 значками для таблицы.
		/// </summary>
		List<string> значки = new List<string>()
		{
			"'", "'", "L", "L",
			"l", "l", "x", "x",
			"#", "#", "m", "m",
			"r", "r", "ь", "ь",
		};
		/// <summary>
		/// метод для расстановки всех значков в ячейки таблицы.
		/// </summary>
		private void РасстановкаВсехЗначков()
		{
			foreach(Control control in tableLayoutPanel1.Controls)
			{
				Label iconLabel = control as Label;
				if (iconLabel != null)
				{
					int случ = рндм.Next(значки.Count);
					iconLabel.Text = значки[случ];
					iconLabel.ForeColor = iconLabel.BackColor; // <-------
					значки.RemoveAt(случ);
				}
			}
		}
	}
}