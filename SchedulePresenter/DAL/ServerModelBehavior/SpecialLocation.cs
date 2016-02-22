﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulePresenter.DAL
{
    public partial class SpecialLocation
    {
        private serverDBEntities context;

        public SpecialLocation(serverDBEntities context)
        {
            this.context = context;
        }

        public SpecialLocation GetSpecialLocationById(int specialLocationId)
        {
            var specialLocations = from specialLocation in context.SpecialLocation
                                   where specialLocation.ID == specialLocationId
                                   select specialLocation;

            return specialLocations.FirstOrDefault();
        }

        public SpecialLocation GetSpecialLocationByIdWithLocalSearch(int specialLocationId)
        {
            var specialLocationsLocal = from specialLocation in context.SpecialLocation
                                        where specialLocation.ID == specialLocationId
                                        select specialLocation;

            SpecialLocation l = specialLocationsLocal.FirstOrDefault();

            if (l != null)
            {
                return l;
            }

            var specialLocations = from specialLocation in context.SpecialLocation
                                   where specialLocation.ID == specialLocationId
                                   select specialLocation;

            return specialLocations.FirstOrDefault();
        }
    }
}
