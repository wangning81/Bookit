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
    /// Interaction logic for PathWnd.xaml
    /// </summary>
    public partial class PathWnd : Window
    {
        private BookitDB db = new BookitDB();
        public PathWnd()
        {
            InitializeComponent();
            this.dataGrid1.DataContext = db.MapNodes.ToList();
            this.dataGrid2.DataContext = db.MapNodes.ToList();
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.label1.Content = dataGrid1.SelectedItem == null ? 
                                    string.Empty : ((MapNode)dataGrid1.SelectedItem).Name;

            if (null == dataGrid2.SelectedItems || dataGrid2.SelectedItems.Count == 0)
            {
                this.listBox1.DataContext = QueryResult();
                this.textBox1.IsEnabled = false;
            }
            else
                this.textBox1.IsEnabled = true;
        }

        private void dataGrid2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null == dataGrid2.SelectedItems || dataGrid2.SelectedItems.Count == 0)
            {
                this.listBox1.DataContext = QueryResult();
                this.textBox1.IsEnabled = false;
            }
            else
            {
                var col = new List<MapNode>();
                foreach (var item in dataGrid2.SelectedItems)
                {
                    col.Add(((MapNode)item));
                }

                this.listBox1.DataContext = col;

                this.textBox1.IsEnabled = true;
            }
        }

        private List<MapNode> QueryResult()
        {
            var col = new List<MapNode>();
            col = (from row1 in db.MapPathes
                   where row1.U.Id == ((MapNode)dataGrid1.SelectedItem).Id
                   select row1.V).
                Union(
            (from row2 in db.MapPathes
             where row2.V.Id == ((MapNode)dataGrid1.SelectedItem).Id
             select row2.U)).ToList<MapNode>();

            return col;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double distance;
            if (double.TryParse(this.textBox1.Text, out distance))
            {
                foreach (var item in dataGrid2.SelectedItems)
                {
                    if(((MapNode)dataGrid1.SelectedItem).Id != ((MapNode)item).Id)
                    {
                        db.MapPathes.Add(
                            new MapPath()
                            {
                                U = (MapNode)dataGrid1.SelectedItem,
                                V = (MapNode)item,
                                Distance = distance
                            }
                            );
                    }
                }

                db.SaveChanges();
            }
            else
                MessageBox.Show("distance is an invalid double.");
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //this.dataGrid1.SelectedItems.Clear();
            this.dataGrid2.SelectedItems.Clear();
            this.textBox1.Text = string.Empty;
        }

        private void listBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                var v = (
                        (from p in db.MapPathes
                         where p.U.Id == ((MapNode)this.dataGrid1.SelectedItem).Id
                               &&
                               p.V.Id == ((MapNode)this.listBox1.SelectedItem).Id
                         select p.Distance).Union(

                            from p in db.MapPathes
                            where p.U.Id == ((MapNode)this.listBox1.SelectedItem).Id
                                  &&
                                  p.V.Id == ((MapNode)this.dataGrid1.SelectedItem).Id
                            select p.Distance
                        )
                        ).First();
                this.textBox1.Text = v.ToString();
            }
        }
    }
}
