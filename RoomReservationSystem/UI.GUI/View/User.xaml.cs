﻿using Core;
using System.Windows;
using System.Windows.Controls;

namespace UI.GUI.View
{
	/// <summary>
	/// Interaction logic for User.xaml
	/// </summary>
	public partial class User : Window
	{
		public User()
		{
			Permission permissionLevel = LoggedIn.User.PermissionLevel;
			InitializeComponent();

			if (permissionLevel == Permission.Student)
			{
				ManageRoomsButton.Visibility = Visibility.Hidden;
				ManageReservationsButton.Visibility = Visibility.Hidden;
				RegisterRoomButton.Visibility = Visibility.Hidden;
				ReserveRoomAdminButton.Visibility = Visibility.Hidden;
			}

			if (permissionLevel == Permission.Teacher)
			{
				ManageRoomsButton.Visibility = Visibility.Hidden;
				ManageReservationsButton.Visibility = Visibility.Hidden;
				RegisterRoomButton.Visibility = Visibility.Hidden;
				ReserveRoomAdminButton.Visibility = Visibility.Hidden;
			}

			if (permissionLevel == Permission.Admin)
			{
				ReserveRoomButton.Visibility = Visibility.Hidden;
				SeeMyReservationButton.Visibility = Visibility.Hidden;
			}

			
		}


		private void ReserveRoomButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Content = new ReserveRoom();
		}

		private void SeeMyReservationButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Content = new SeeMyReservationsV();

		}

		private void ManageRoomsButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Content = new ManageRoomsV();
		}

		private void ManageReservationsButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Content = new ManageReservationsV();
		}

		private void RegisterRoomButtonClick(object sender, RoutedEventArgs e)
		{
			Frame.Content = new RegisterRoom();

		}

        private void ReserveRoomAdminButtonClick(object sender, RoutedEventArgs e)
        {
			Frame.Content = new ReserveRoomAdminV();
        }
    }
}
