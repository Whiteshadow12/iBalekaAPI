using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iBalekaAPI.Data.Infastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
