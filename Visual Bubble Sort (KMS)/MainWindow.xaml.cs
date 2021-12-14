using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Visual_Bubble_Sort
{
	/// <summary>
	/// NTZippo
	/// A visual reprensentation of bubble sort inspired by reddit user u/rd07-chan completion of the task
	/// I thought it was a good exercise and i think it came out well
	/// </summary>
	public partial class MainWindow : Window
	{
		// variable and list creation
		const double WIDTH = 10;
		const int SIZE = 50;
		List<Rectangle> rectList = new List<Rectangle>();
		public MainWindow()
		{
			InitializeComponent();
		}
		// method to generate the rectangles and space them properly throghtout the form
		public void GenerateRectangle()
		{
			//clears all current rectangles from the canvas
			for(int x = 0; x< rectList.Count;x++)
			{
			grid.Children.Remove(rectList[x]);
			}
			//clears rectList
			rectList.Clear();
			Random rand = new Random();
			// generates new rectangles with set width and random heights. Makes the black with a green background
			for (int i = 0; i < SIZE; i++)
			{
				rectList.Add(new Rectangle
				{
					Width = WIDTH,
					Height = rand.Next(30,350),
					Fill = Brushes.Black,
					StrokeThickness = 2,
					Stroke = Brushes.Green,

				}
				); ;
				// adds current rect to canvas and spaces them properly
				grid.Children.Add(rectList[i]);
				Canvas.SetTop(rectList[i], 1);
				Canvas.SetLeft(rectList[i], WIDTH * i);
			}
		}
		// method that calls generateRectangle. could make take out the middle man but oh well
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			GenerateRectangle();
		}
		// calls the bubble sort method could take out the middle man but oh well
		private void Sort(object sender, RoutedEventArgs e)
		{
			Bubble_Sort(); //
		}
		// when called will refresh the current display and display changes after each swap
		private void refresh()
		{
			for (int i = 0; i < rectList.Count; i++)
				grid.Children.Remove(rectList[i]);

			for (int i = 0; i < rectList.Count; i++)
			{
				Canvas.SetLeft(rectList[i], i * WIDTH);
				grid.Children.Add(rectList[i]);
			}
		}
		// an async method allows for Task.Delay to see what actually happens in the changes (Could not get timers working
		// every other attempt i made causes no changes to be show and the program seemingly crashed but actually it was doing changes
		// the visuals didnt not reflect that until after the program ended. (Probably because of the rendering thread idk)
		//please help me figure that out. I want to learn why
		private async void Bubble_Sort()
		{
			// bubble sort
			Rectangle temp;
			for(int i = 0; i < rectList.Count-1; i++)
			{
				for(int k = 0; k < rectList.Count - i - 1; k++)
				{
					// sets color of current index to white and the index being checked against red for clarity sake
					rectList[k].Fill = Brushes.White;
					rectList[k+1].Fill = Brushes.Red;
					// causes the algorithm to halt for 1 msecs (i think) to allow for visuals to be displayed
					await Task.Delay(1);
					if(rectList[k].Height > rectList[k+1].Height)
					{
						temp = rectList[k];
						rectList[k] = rectList[k + 1];
						rectList[k + 1] = temp;
					}
					// reverts colors back and and refreshes visuals
					rectList[k].Fill = Brushes.Black;
					rectList[k+1].Fill = Brushes.Black;
					refresh();
				}
			}
			// does a cool highlight after completely sorted like the fancy sorting algorithm videos
			await Finished();
		}
		// method that does the color chang thought the whole sorted loop looks cool
		private async Task Finished()
		{
			for(int i = 0; i < rectList.Count; i++)
			{
				rectList[i].Fill = Brushes.White;
				await Task.Delay(3);
				rectList[i].Fill = Brushes.Black;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
		}
		private async void Selection_Sort()
		{
			int min_ind;
			Rectangle temp;
			for (int i = 0; i < rectList.Count; i++)
			{
				min_ind = i;
				for (int k = i; k < rectList.Count; k++)
				{
					rectList[i].Fill = Brushes.White;
					await Task.Delay(1);
					if (rectList[k].Height < rectList[min_ind].Height)
					{
						rectList[min_ind].Fill = Brushes.Black;
						min_ind = k;
						rectList[k].Fill = Brushes.Red;
					}
					refresh();
				}
				temp = rectList[min_ind];
				rectList[min_ind] = rectList[i];
				rectList[i] = temp;
				rectList[min_ind].Fill = Brushes.Black;
				rectList[i].Fill = Brushes.Black;

			}
			await Finished();

		}
		private void Selection_Click(object sender, RoutedEventArgs e)
		{
			Selection_Sort();
		}
	}
}