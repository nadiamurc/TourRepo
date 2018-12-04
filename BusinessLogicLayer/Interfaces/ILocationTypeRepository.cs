using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tour.DomainModel;

namespace BusinessLogicLayer.Interfaces
{
    public interface ILocationTypeRepository /*: IGenericRepository<LocationType>*/
    {
        LocationType GetLocationTypeById(int Id);

        IList<LocationType> GetAllLocationTypes();
        void SaveLocationType(LocationType locationType);
    }
}
