﻿using Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Core.UnitTest
{
    [TestClass]
    public class ClientCreateReservation
    {

        IRoom _room1;
        IRoom _room2;
        IRoom _room3;
        //IRoom _room4;

        IUser _student;
        //IUser _teacher;
        //IUser _admin;

        RoomRepository _repoRoom = RoomRepository.Instance; // Singletons
        ReservationRepository _repoReserv = ReservationRepository.Instance;
        UserRepository _repoUser = UserRepository.Instance;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            SystemSettings.Environment = Environment.Test;
        }

        [ClassCleanup]
        public static void ClassClean()
        {
            SystemSettings.Environment = Environment.Prod;
        }

        [TestInitialize]
        public void TestsInitialize()
        {
            _repoRoom.Clear();
            _repoReserv.Clear();
            _repoUser.Clear();

            _room1 = new Room('A', 1, 2, 4, Permission.Student);
            _room2 = new Room('A', 1, 99, 8, Permission.Student);
            _room3 = new Room('B', 1, 45, 2, Permission.Student);

            _repoRoom.Add(_room1);
            _repoRoom.Add(_room2);
            _repoRoom.Add(_room3);

            _student = new User("roxa0198", "roxa0188@edu.eal.dk", Permission.Student);

            _repoUser.Add(_student);
        }

        [TestMethod]
        public void SortRoomsByMaxPeopleIntoFILOStack()
        {
            List<IRoom> StackRooms = _repoRoom.GetPossible(Permission.Student, 4);

            Assert.AreEqual(StackRooms[0], _room1);
            Assert.AreEqual(StackRooms[1], _room2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void SortRoomsByMaxPeopleIntoFILOStackOutOfRooms()
        {
            List<IRoom> Rooms = _repoRoom.GetPossible(Permission.Student, 4);

            IRoom test = Rooms[2];
        }

        [TestMethod]
        public void RoomIsAvailable()
        {
            DateTime from = new DateTime(2017, 05, 01, 13, 0, 0);
            DateTime to = new DateTime(2017, 05, 01, 14, 0, 0);
            bool roomAvailable = _room1.IsAvailable(from, to);
            Assert.IsTrue(roomAvailable);
        }
        [TestMethod]
        public void RoomIsNotAvailable()
        {
            DateTime from = new DateTime(2017, 05, 01, 13, 0, 0);
            DateTime to = new DateTime(2017, 05, 01, 14, 0, 0);
            Reservation res = new Reservation(_student, _room1, 3, from, to);
            _repoReserv.Add(res);
            bool roomAvailable = _room1.IsAvailable(from, to);
            Assert.IsFalse(roomAvailable);
        }
    }
}
