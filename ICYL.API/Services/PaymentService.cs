﻿using AuthorizeNet.Api.Contracts.V1;
using ICYL.API.Data;
using ICYL.API.Entity;

namespace ICYL.API.Services
{
    public class PaymentService
    {
        Payment payment = new Payment();
        public TransactionResponse InsertPaymentDetail(PaymentModel paymentDetail)
        {
            TransactionResponse response = new TransactionResponse();
            response = payment.InsertPaymentDetail(paymentDetail);
            return response;
        }
        public ANetApiResponse AddPayment()
        {
            var response = payment.AddPayment();
            return response;

        }
        public dynamic Donate(PaymentConfig model)
        {
            var response = payment.Donate(model);
            return response;

        }
        public dynamic GetAllTransaction(TransDailyReport report)
        {
            var response = payment.GetAllTransaction(report);
            return response;
        }
        //public string GetPaypalPaymentUrl(OrdersModel orders)
        //{

        //    var response = payment.GetPaypalPaymentUrl(orders);
        //    return response;
        //}
    }
}
