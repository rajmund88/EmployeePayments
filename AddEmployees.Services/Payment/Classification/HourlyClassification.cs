﻿using EmloyeePayments.Infrastructure.Domains;
using System;
using System.Collections;

namespace EmloyeePayments.Infrastructure.Payment.Classification
{
    public class HourlyClassification : IPaymentClassification
    {
        public double HourlyRate;
        private readonly Hashtable _timeCardsTable = new Hashtable();
        public HourlyClassification(double hourlyRate)
        {
            HourlyRate = hourlyRate;
        }
        public void AddTimeCard(TimeCard timeCard)
        {
            _timeCardsTable.Add(timeCard.Date,timeCard);
        }

        public double CalculatePay(PayCheck pc)
        {
            double totalPay = 0.0;
            foreach (TimeCard tc in _timeCardsTable.Values)
            {
                if (IsInPayPeriod(tc, pc.PayDate))
                {
                    totalPay += CalculatePayForTimeCard(tc);
                }
            }
            return totalPay;
        }

        private double CalculatePayForTimeCard(TimeCard tc)
        {
            var overTimeHours = Math.Max(0.0, tc.Hours - 8);
            var normalHours = tc.Hours - overTimeHours;
            return HourlyRate * normalHours + HourlyRate * overTimeHours * 1.5;
        }

        private bool IsInPayPeriod(TimeCard tc, DateTime payDate)
        {
            var payPeriodEndDate = payDate;
            var payPeriodStartDate = payDate.AddDays(-5);
            return tc.Date <= payPeriodEndDate && tc.Date >= payPeriodStartDate;
        }

        public TimeCard GetTimeCard(DateTime date)
        {
            return _timeCardsTable[date] as TimeCard;
        }
    }
}