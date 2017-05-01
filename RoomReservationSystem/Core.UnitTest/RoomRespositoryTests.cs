﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Interfaces;
using Core;

namespace Core.UnitTest
{
    [TestClass]
    public class RoomRespositoryTests
    {
        RoomRepository _repoRoom;

        IUser _student;
        IUser _teacher;
        IUser _admin;

        IRoom _room1;
        IRoom _room2;
        IRoom _room3;

        Reservation _reservation1;
        Reservation _reservation2;
        Reservation _reservation3;

        DateTime _dateFrom;
        DateTime _dateTo;

        [TestInitialize]
        public void TestInitialize()
        {
            _repoRoom = new RoomRepository();
            _student = new User("matt2694", "matt2694@edu.eal.dk", Permission.Student);
            _teacher = new User("alhe", "alhe@eal.dk", Permission.Teacher);
            _admin = new User("frje", "frje@eal.dk", Permission.Admin);
            _room1 = new Room('A', 2, 9, 6, Permission.Student);
            _room2 = new Room('A', 2, 15, 6, Permission.Teacher);
            _room3 = new Room('A', 2, 115, 6, Permission.Admin);
            _dateFrom = new DateTime(2016, 4, 29, 8, 0, 0);
            _dateTo = new DateTime(2016, 4, 29, 16, 0, 0);
            _reservation1 = new Reservation(_student, _room1, 6, _dateFrom, _dateTo);
            _reservation2 = new Reservation(_teacher, _room2, 6, _dateFrom, _dateTo);
            _reservation3 = new Reservation(_admin, _room3, 6, _dateFrom, _dateTo);
        }
    }
}
