﻿using Core.Interfaces;
using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ReservationRepository
    {
        List<Reservation> _reservationRepository = new List<Reservation>();
        
        RoomRepository roomRepo =  RoomRepository.Instance;
        private static ReservationRepository _instance = new ReservationRepository();
        public static ReservationRepository Instance { get { return _instance; } }

        public IRoom RequestReservation(DateTime from, DateTime to, int peoplenr)
        {
            
            IRoom currentRoom;
            IRoom foundRoom = null;
            Stack<IRoom> rooms = roomRepo.GetPossible(LoggedIn.User.PermissionLevel, peoplenr);
            while (foundRoom == null && rooms.Count > 1)
            {
                currentRoom = rooms.Pop();
                bool roomAvailable = currentRoom.IsAvailable(from, to);
                if (roomAvailable == true)
                {
                    foundRoom = currentRoom;
                }
            }
            if (foundRoom == null)
            {
                throw new NoRoomsAvailable();
            }
            else
            {
                Reservation reservation = new Reservation(LoggedIn.User, foundRoom, peoplenr, from, to);
                this.Add(reservation);
                return foundRoom;
            }
            
        }
        public void Clear()
        {
            _reservationRepository.Clear();
        }

        public void Add(Reservation reservation)
        {
			_reservationRepository.Add(reservation);
			reservation.Room.AddReservation(reservation);
            reservation.User.AddReservation(reservation);
        }

        public void Delete(Reservation reservation)
        {
            _reservationRepository.Remove(reservation);
            reservation.Room.DeleteReservation(reservation);
            reservation.User.DeleteReservation(reservation);
        }

        public void Add(IUser user, IRoom room, int peoplenr, DateTime datefrom, DateTime dateto)
        {
            Reservation reservation = new Reservation(user, room, peoplenr, datefrom, dateto);
            this.Add(reservation);
        }

        public List<Reservation> Get()
        {
            return _reservationRepository;
        }

        //public List<Reservation> Get(IUser user)    INTEGRATION!!!
        //{
        //    throw new NotImplementedException();
        //}

        //public List<Reservation> Get(IRoom room)
        //{
        //    throw new NotImplementedException();
        //}

        public Reservation Get(Reservation checkreservation)
        {
            Reservation result = null;

            foreach(Reservation reservation in _reservationRepository)
            {
                if(reservation.Equals(checkreservation))
                {
                    result = reservation;
                }
            }
            return result;
        }
    }
}
