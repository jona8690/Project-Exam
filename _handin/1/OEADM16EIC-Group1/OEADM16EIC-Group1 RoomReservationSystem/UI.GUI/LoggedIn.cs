﻿using Core.Interfaces;

namespace UI.GUI
{
	public static class LoggedIn {
		// Login a user:
		// Core.LoggedIn.User = new User(...);

		// Eg. read logged in user permission:
		// Core.LoggedIn.User.PermissionLevel
		public static IUser User { get; set; }
	}
}
