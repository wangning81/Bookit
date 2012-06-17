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
    /// Interaction logic for MapNodeWnd.xaml
    /// </summary>
    public partial class MapNodeWnd : Window
    {
        private BookitDB db = new BookitDB();
        public MapNodeWnd()
        {
            InitializeComponent();
            //this.dgridNodes.ItemsSource = db.MapNodes.ToList();
            this.dgridNodes.DataContext = db.MapNodes.ToList();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            //if (this.rbIsland.IsChecked.Value)
            //    db.MapNodes.Add(new CubeIsland() { Name = this.tbName.Text });
            //else if (this.rbGateway.IsChecked.Value)
            //    db.MapNodes.Add(new Gateway { Name = this.tbName.Text });
            //ignore meeting room. we will use a separate UI to populate them.

            int num;
            if (int.TryParse(this.tbNum.Text, out num))
            {
                for (int n = 1; n <= num; n++)
                    db.MapNodes.Add(new CubeIsland()
                    {
                        Name =
                            string.Format("{0}-Island{1}", this.tbName.Text, n < 10 ? "0" + n.ToString() : n.ToString())
                    });
            }
            else
                db.MapNodes.Add(new Gateway { Name = this.tbName.Text });

            db.SaveChanges();
            //this.dgridNodes.ItemsSource = db.MapNodes.Local;
            this.dgridNodes.DataContext = db.MapNodes.ToList();
        }
    }
}
