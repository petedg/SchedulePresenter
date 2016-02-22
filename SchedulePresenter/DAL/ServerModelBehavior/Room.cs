using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class Room
    {
        private serverDBEntities context;

        public Room(serverDBEntities context)
        {
            this.context = context;
        }

        public List<Room> GetListForLocation(Location location)
        {
            var rooms = from room in context.Room
                        where room.Location_ID == location.ID && room.ID != 4   // != specjalna lokalizacja
                        select room;

            return rooms.ToList();
        }

        public Room GetRoomById(int roomId)
        {
            var rooms = from room in context.Room
                        where room.ID == roomId
                        select room;

            return rooms.FirstOrDefault();
        }        
    }
}
