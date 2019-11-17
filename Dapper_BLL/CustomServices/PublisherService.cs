using AutoMapper;
using Dapper_BLL.Entities;
using Dapper_DAL;
using Dapper_DAL.Entities;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dapper_BLL
{
    public class PublisherService : BLLServices<BLLPublisher, DALPublisher>
    {
        public PublisherService()
        {
            this._curDapperRep = new PublisherDapperRepository();
        }
    }
}
