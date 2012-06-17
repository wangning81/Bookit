using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Bookit.Data;
using Bookit.Domain;

namespace Bookit.Data.Admin.WpfUI
{
    /// <summary>
    /// Interaction logic for CubeWnd.xaml
    /// </summary>
    public partial class CubeWnd : Window
    {
        private BookitDB db = new BookitDB();
        public CubeWnd()
        {
            InitializeComponent();
            this.dataGrid1.DataContext = db.MapNodes.ToList();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.label1.Content = ((MapNode)dataGrid1.SelectedItem).Name;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.textBox1.Text.Trim().Split(',').ToList().
                ForEach(
                name =>
                {
                    if (null == ((CubeIsland)dataGrid1.SelectedItem).Cubes ||
                        ((CubeIsland)dataGrid1.SelectedItem).Cubes.Count == 0)
                    {
                        ((CubeIsland)dataGrid1.SelectedItem).Cubes =
                            new List<Cube>(){
                                new Cube()
                                {
                                    Name = name.Trim()
                                }
                            };
                    }
                    else
                    {
                        ((CubeIsland)dataGrid1.SelectedItem).Cubes.Add(
                            new Cube()
                            {
                                Name = name.Trim()
                            });
                    }

                    db.SaveChanges();
                }
                );

            this.textBox1.Text = string.Empty;
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (textBox2.Text.Trim().Length == textBox3.Text.Trim().Length &&
                int.Parse(textBox2.Text.Trim()) <= int.Parse(textBox3.Text.Trim()))
            {
                var strlist = new List<string>();
                for (int n = int.Parse(textBox2.Text.Trim()); n <= int.Parse(textBox3.Text.Trim()); n++)
                {
                    strlist.Add(n.ToString());
                }

                textBox1.Text = string.Join(",", strlist);

                if (IsNoDuplicated(strlist))
                    this.label2.Content = string.Format("{0} records will be inserted.",
                                                strlist.Count.ToString());
                else
                    this.label2.Content = "Duplicate detected.";

            }
            else
                this.label2.Content = "wait for input.";
        }

        private bool IsNoDuplicated(List<string> nums)
        {
            return nums.TrueForAll(n =>
                db.Cubes.All(c => c.Name != n)
                );
        }
    }
}
