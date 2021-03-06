﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AddEmployees.Services.Payment.Method;

namespace EmloyeePayments.Infrastructure.Services.EmployeeService
{
    public class ChangeMailTransaction : ChangeMethodTransaction
    {
        public ChangeMailTransaction(int empId) : base(empId)
        {
        }

        protected override IPaymentMethod Method
            => new MailMethod();
    }
}