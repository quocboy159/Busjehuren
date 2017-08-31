using Busjehuren.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Busjehuren.Core.EF
{
    public partial class Language : IEntity
    {
        public enum Languages
        {
            Dutch = 1,
            English = 2,
            German = 3
        }
    }
}
