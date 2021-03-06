﻿using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core
{
    public class RoomRepository
    {
        List<IRoom> _roomRepository = new List<IRoom>();
        private static RoomRepository _instance = new RoomRepository();
        public static RoomRepository Instance { get { return _instance; } }
        private DALFacade _dalFacade = new DALFacade();

        public void Clear()
        {
            ReservationRepository.Instance.Clear();

            _dalFacade.DeleteAllRooms();
            _roomRepository.Clear();
        }

        public void Add(IRoom room)
        {
            _roomRepository.Add(room);
            _dalFacade.InsertRoom(room);
        }

        public void Add(char building, int floor, int nr, int maxPeople, Permission minpermissionlevel)
        {
            Room room = new Room(building, floor, nr, maxPeople, minpermissionlevel);
            this.Add(room);
        }

        public void LoadFromDatabase(IRoom room)
        {
            _roomRepository.Add(room);
        }

        public void DeleteFromRepository(IRoom room)
        {
            _roomRepository.Remove(room);
        }

        public List<IRoom> Get()
        {
            return _roomRepository;
        }

        public List<IRoom> Get(Permission permissionlevel)
        {
            List<IRoom> roomsByPermissionLevel = new List<IRoom>();

            foreach (IRoom room in _roomRepository)
            {
                if (room.MinPermissionLevel <= permissionlevel)
                {
                    roomsByPermissionLevel.Add(room);
                }
            }

            return roomsByPermissionLevel;
        }

        public IRoom Get(IRoom checkroom)
        {
            IRoom foundRoom = null;
            foreach (IRoom room in _roomRepository)
            {
                if (room.Equals(checkroom))
                {
                    foundRoom = room;
                }
            }

            if (foundRoom == null)
            {
                throw new IndexOutOfRangeException();
            }
            else
            {
                return foundRoom;
            }
        }

        public IRoom Get(Reservation reservation)
        {
            Room roomsByReservation = null;
            foreach (Room room in _roomRepository)
            {
                if (room == reservation.Room)
                {
                    roomsByReservation = room;
                }
            }
            return roomsByReservation;
        }

        public List<IRoom> GetPossible(Permission permissionlevel, int people)
        {
            List<IRoom> permission = this.Get(permissionlevel);
            List<IRoom> possible = new List<IRoom>();

            foreach (IRoom room in permission)
            {
                if (room.MaxPeople >= people)
                {
                    possible.Add(room);
                }
            }

            possible.Sort();

            return possible;
        }

        public List<IRoom> GetPossible(Permission permissionlevel)
        {
            List<IRoom> possible = this.Get(permissionlevel);

            possible.Sort();
            return possible;

        }

        public void Delete(IRoom room)
        {
            foreach (Reservation res in room.GetReservations())
            {
                ReservationRepository.Instance.Delete(res);
            }
            _roomRepository.Remove(room);
            _dalFacade.DeleteRoom(room);
        }
    }
}
