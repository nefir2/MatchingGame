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
		private void НажатиеНаЯчейку(object sender, EventArgs e)
		{
			if (timer1.Enabled) return; //если идёт таймер - другие нажатия игнорируются.

			Label нажатый = sender as Label; //нажатый лейбл
			if (нажатый != null) //если нажатый лейбл не null
			{
				if (нажатый.ForeColor == Color.Black) return; //если цвет нажатого лейбла уже чёрный, то нажатие игнорируется.
				if (первый == null) //если первого нажатия ещё не было
				{
					первый = нажатый; //установка нажатого лейбла в поле первого нажатого лейбла.
					первый.ForeColor = Color.Black; //установка цвета нажатого значка на чёрный.
					//завершается работа метода
					return;
				}
				//второе нажатие игрока
				второй = нажатый;
				второй.ForeColor = Color.Black;

				//проверка, открыты ли все ячейки
				ПроверкаНаПобеду();

				//если первое нажатие и второе нажатие имеют одинаковые знаки,
				//то таймер не запускается, и ячейки остаются видимыми.
				if (первый.Text == второй.Text)
				{
					//затираются нажатия
					первый = null;
					второй = null;
					//завершается работа метода
					return;
				}
				timer1.Start(); //запуска таймера после второго нажатия
			}
		}

		/// <summary>
		/// таймер запускается, 
		/// когда игрок нажимает на две ячейки, 
		/// и они оказываются неверными.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop(); //остановка таймера.

			//скрытие открытых ячеек.
			первый.ForeColor = первый.BackColor;
			второй.ForeColor = второй.BackColor;

			//очистка нажатых лейблов из памяти очереди нажатий.
			первый = null;
			второй = null;
		}

		/// <summary>
		/// проверка - открыты ли все ячейки. 
		/// </summary>
		/// <remarks>
		/// метод проверяет на то, что все ячейки открыты, <br/>
		/// сигнализирует о победе и закрывает форму (окно).
		/// </remarks>
		private void ПроверкаНаПобеду()
		{
            foreach (Control ячейка in tableLayoutPanel1.Controls)
            {
				Label лейбл = ячейка as Label; //лейбл из ячейки в таблице
				if (лейбл != null) //если лейбл не null
                {
					if (лейбл.ForeColor == лейбл.BackColor) return; //если цвет знака в лейбле равен цвету фона в лейбле - выход из метода.
                }
            }
			//если цикл не прервался, то выводится сообщение о завершении игры.
			MessageBox.Show("ты открыл все ячейки, поздравляю челик", "ну че браточек");
			Close();
		}
	}
}