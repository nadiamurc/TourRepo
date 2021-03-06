﻿using BusinessLogicLayer.Interfaces;
using DAL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Tour.DomainModel;
using System;

namespace BusinessLogicLayer.Repositories
{
    public class LocationTypeRepository : ILocationTypeRepository /*GenericRepository<LocationType>,*/
    {
        protected DbContext _entities;
        protected readonly IDbSet<LocationType> _dbset;
        public LocationTypeRepository(DbContext context)
           : base()
        {
            _entities = context;
            _dbset = context.Set<LocationType>();
        }

        //public void AddLocationType(LocationType locationType)
        //{
        //    using (var context = new DatabaseEntities())
        //    {
                
        //        context.LocationTypes.Add(locationType);
        //        context.SaveChanges();
        //    }
        //}

        public IList<LocationType> GetAllLocationTypes()
        {
           
            using (var context = new DatabaseEntities())
            {
                return context.Set<LocationType>().ToList();
            }
        }

        public IList<CountyDetail> GetAllCounties()
        {
            using (var context = new DatabaseEntities())
            {
                return context.Set<CountyDetail>().ToList();
            }
        }

        public LocationType GetLocationTypeById(int Id)
        {
            using (var context = new DatabaseEntities())
            {
                return new LocationType();
            }
            
        }

        public void SaveLocationType(LocationType locationType)
        {
            using (var context = new DatabaseEntities())
            {
                context.LocationTypes.Add(locationType);
                context.SaveChanges();
            }
        }

        public void SaveLocationDetail(LocationDetail locationDetail)
        {
            using (var context = new DatabaseEntities())
            {
                if (locationDetail.LocationDetailsId == 0)
                {
                    context.LocationDetails.Add(locationDetail);
                }
                else
                {
                    context.Entry(locationDetail).State = EntityState.Modified;
                }
                
                context.SaveChanges();
            }
        }

        public IList<LocationDetail> GetAllLocationDetails()
        {
            using (var context = new DatabaseEntities())
            {
                return context.Set<LocationDetail>().Where(x=>x.IsActive == true).ToList();
            }
        }

        public LocationDetail GetLocationDetailById(int locationDetailsId)
        {
            using (var context = new DatabaseEntities())
            {
                return context.LocationDetails.Find(locationDetailsId);
               // return new LocationDetail();
            }
        }

        public void EditLocationDetail(LocationDetail locationDetail)
        {
            using (var context = new DatabaseEntities())
            {
                context.Entry(locationDetail).State = EntityState.Modified;
                context.LocationDetails.Add(locationDetail);
                context.SaveChanges();
            }
        }
    }
}
