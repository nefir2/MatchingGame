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
		//автоматически созданный конструктор.
		/// <summary>
		/// конструктор формы (окна) программы.
		/// </summary>
		public Form1()
		{
			InitializeComponent();
			ЗнакиВЯчейки();
		}

		//поля
		/// <summary>
		/// объект рандом.
		/// </summary>
		/// <remarks>
		/// с помощью этого поля все знаки будут случайным образом расставляться в таблице.
		/// </remarks>
		Random рнд = new Random();

		/// <summary>
		/// коллекция со всеми знаками для игры.
		/// </summary>
		/// <remarks>
		/// каждого по паре, чтобы заполнить всё поле всеми знаками, а не дублировать каждый знак.
		/// </remarks>
		List<string> знаки = new List<string>()
		{
			"'", "'", "L", "L",
			"l", "l", "x", "x",
			"#", "#", "m", "m",
			"r", "r", "ь", "ь",
		};

		/// <summary>
		/// место первого нажатия пользователя.
		/// </summary>
		/// <remarks>
		/// если пользователь ещё ничего не сделал, то нажатие равно <see langword="null"/>.
		/// </remarks>
		Label первый = null;

		/// <summary>
		/// место второго нажатия пользователя.
		/// </summary>
		/// <remarks>
		/// если пользователь ещё ничего не сделал, то нажатие равно <see langword="null"/>.
		/// </remarks>
		Label второй = null;

		//методы
		/// <summary>
		/// метод для случайной расстановки каждого знака из коллекции, в каждую ячейку таблицы.
		/// </summary>
		private void ЗнакиВЯчейки()
		{
			//цикл для расстановки знаков.
			foreach (Control ячейка in tableLayoutPanel1.Controls) //элемент ячейки из коллекции ячеек в таблице.
			{
				Label лейбл = ячейка as Label; //получение лейбла из ячейки таблицы.
				if (лейбл != null) //если лейбл не null
				{
					int рчисл = рнд.Next(знаки.Count); //получение очередного случайного числа, не больше количества знаков.
					лейбл.Text = знаки[рчисл]; //установка значения 
					лейбл.ForeColor = лейбл.BackColor; //скрытие значка в таблице
					знаки.RemoveAt(рчисл); //удаление знака выведенного в таблицу из коллекции.
				}
			}
		}

		/// <summary>
		/// нажатие на любую из ячеек.
		/// </summary>
		/// <remarks>
		/// метод раскрывает знак под нажатой ячейкой.
		/// </remarks>
		/// <param name="sender">нажатый лейбл</param>
		/// <param name="e"></param>
		private void нажатиеНаЯчейку(object sender, EventArgs e)
		{
			Label нажатый = sender as Label; //нажатый лейбл
			if (нажатый != null) //если нажатый лейбл не null
			{
				if (нажатый.ForeColor == Color.Black) return; //если цвет нажатого лейбла уже чёрный, то нажатие игнорируется.
				if (первый == null) //если первого нажатия ещё не было
				{
					первый = нажатый; //установка нажатого лейбла в поле первого нажатого лейбла.
					первый.ForeColor = Color.Black; //установка цвета нажатого значка на чёрный.
				}
			}
		}
	}
}