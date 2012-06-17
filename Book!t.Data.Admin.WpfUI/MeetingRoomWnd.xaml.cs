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
    /// Interaction logic for MeetingRoomWnd.xaml
    /// </summary>
    public partial class MeetingRoomWnd : Window
    {
        private BookitDB db = new BookitDB();
        public MeetingRoomWnd()
        {
            InitializeComponent();
            this.dgridMR.DataContext = db.MeetingRooms.ToList();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            //db.MeetingRooms.Add(new MeetingRoom()
            //            {
            //                 Name = tbName.Text,
            //                 Capacity = int.Parse(tbCapicity.Text)
            //            }
            //            );

            db.MapNodes.Add(new MeetingRoom()
                        {
                            Name = tbName.Text,
                            Capacity = int.Parse(tbCapicity.Text),
                            Description = tbDescription.Text,
                            Email = tbEmail.Text,
                            LocationDisplayName = tbName.Text,
                            Facilities = tbFacilities.Text,
                            IsBookable = cbBookable.IsChecked.Value
                        }
                        );

            db.SaveChanges();
            this.dgridMR.DataContext = db.MeetingRooms.ToList();
        }
    }
}
