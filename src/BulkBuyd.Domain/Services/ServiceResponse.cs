using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace BulkBuyd.Domain.Services
{
    public class ServiceResponse
    {
        public List<ValidationFailure> ErrorMessages { get; set; }
        public bool Successful => !ErrorMessages.Any();

        public ServiceResponse()
        {
            ErrorMessages = new List<ValidationFailure>();
        }
    }

    public class ServiceResponse<T> : ServiceResponse
    {
        public T Data { get; set; }
    }
}
