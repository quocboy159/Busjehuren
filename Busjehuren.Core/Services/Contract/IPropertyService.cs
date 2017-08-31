using Busjehuren.Core.Dto;
using Busjehuren.Core.EF;
using Busjehuren.Core.Models;
using System.Collections.Generic;

namespace Busjehuren.Core.Services.Contract
{
    public interface IPropertyService
    {
        List<PropertyModel> GetProperties();
    }
}
