﻿using System;
using System.Collections.Generic;
using Core.Interfaces;
using DAL;

namespace Core {
	class DALFacade {
		public List<IUser> GetAllUsers() {
			DAL.Users usersData = new DAL.Users();

			List<Dictionary<string, string>> usersInfo = usersData.GetAllUsers();
			List<IUser> users = new List<IUser>();

			foreach (Dictionary<string, string> userInfo in usersInfo) {
				int permissionLevel = int.Parse(userInfo["PermissionLevel"]);
				User newUser = new User(userInfo["Username"], userInfo["Email"], HelperFunctions.ConvertIntToPermission(permissionLevel));
				users.Add(newUser);
			}

			return users;
		}

        public List<IRoom> GetAllRooms()
        {
            DAL.Rooms roomsData = new DAL.Rooms();

            List<Dictionary<string, string>> roomsInfo = roomsData.GetAllRooms();
            List<IRoom> rooms = new List<IRoom>();

            foreach (Dictionary<string, string> roomInfo in roomsInfo)
            {
                int minPermissionLevel = int.Parse(roomInfo["MinPermissionLevel"]);
                char building = char.Parse(roomInfo["Building"]);
                int floornr = int.Parse(roomInfo["FloorNr"]);
                int nr = int.Parse(roomInfo["Nr"]);
                int maxpeople = int.Parse(roomInfo["MaxPeople"]);

                Room newRoom = new Room(building, floornr, nr, maxpeople, HelperFunctions.ConvertIntToPermission(minPermissionLevel));
                rooms.Add(newRoom);
            }

            return rooms;
        }

        public List<Reservation> GetAllReservations()
        {
            UserRepository repoUsers = UserRepository.Instance;
            RoomRepository repoRooms = RoomRepository.Instance;

            DAL.Reservations reservationsData = new DAL.Reservations();

            List<Dictionary<string, string>> reservationsInfo = reservationsData.GetAllReservations();
            List<Reservation> reservations = new List<Reservation>();

            foreach (Dictionary<string, string> reservationInfo in reservationsInfo)
            {
                IUser dummyUser = new User(reservationInfo["Username"], "", Permission.Student);
                IUser user = repoUsers.Get(dummyUser);

                char building = char.Parse(reservationInfo["Building"]);
                int floornr = int.Parse(reservationInfo["FloorNr"]);
                int nr = int.Parse(reservationInfo["Nr"]);
                IRoom dummyRoom = new Room(building, floornr, nr, 0, Permission.Student);
                IRoom room = repoRooms.Get(dummyRoom);

                DateTime from = DateTime.Parse(reservationInfo["DateFrom"]);
                DateTime to = DateTime.Parse(reservationInfo["DateTo"]);
                int peopleNr = int.Parse(reservationInfo["PeopleNr"]);

                Reservation reservation = new Reservation(user, room, peopleNr, from, to);
                reservations.Add(reservation);


            }
            return reservations;
        }
    }
}