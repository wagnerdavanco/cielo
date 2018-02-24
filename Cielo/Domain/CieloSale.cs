using Cielo.Validators;
using System;
using System.Collections.Generic;

namespace Cielo.Domain
{

    public enum EnumBrand
    {
        Visa,
        Master,
        Amex,
        Elo,
        Aura,
        JCB,
        Diners,
        Discover,
        Hipercard
    }

    public enum EnumStatus
    {

        NotFinished = 0,
        Authorized = 1,
        PaymentConfirmed = 2,
        Denied = 3,
        Voided = 10,
        Refunded = 11,
        Pending = 12,
        Aborted = 13,
        Scheduled = 20
    }


    public class Customer
    {
        public Customer()
        {

        }

        public Customer(string name)
        {
            AlterarCustomer(name);
        }

        private void AlterarCustomer(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("customer name is required");
            }

            this.Name = name;
        }

        public string Name { get; protected set; }
    }

    public class CreditCard
    {

        public CreditCard()
        {

        }

        public CreditCard(string cardNumber,
                          string holder,
                          int yearExpiration,
                          int monthExpiration,
                          string securityCode,
                          EnumBrand Brand)
        {

            AlterarBrand(Brand);
            AlterarCardNumber(cardNumber, Brand);
            AlterarSecurityCode(securityCode);
            AlterarHolder(holder);
            AlterarDateExpiration(yearExpiration, monthExpiration);
        }

        private void AlterarBrand(EnumBrand brand)
        {
            this.Brand = brand.ToString();
        }

        private void AlterarSecurityCode(string securityCode)
        {
            if (string.IsNullOrEmpty(securityCode))
            {
                throw new ArgumentException("securityCode name is required");
            }

            this.SecurityCode = securityCode;
        }

        private void AlterarDateExpiration(int yearExpiration, int monthExpiration)
        {
            if (yearExpiration <= 0 && monthExpiration <= 0)
            {
                throw new ArgumentException("DateExpiration name is required");
            }

            this.ExpirationDate = string.Format("{0}/{1}", monthExpiration, yearExpiration);
        }

        private void AlterarHolder(string holder)
        {
            if (string.IsNullOrEmpty(holder))
            {
                throw new ArgumentException("Holder name is required");
            }

            this.Holder = holder;
        }

        private void AlterarCardNumber(string cardNumber, EnumBrand brand)
        {
            if (string.IsNullOrEmpty(cardNumber))
            {
                throw new ArgumentException("CardNumber name is required");
            }

            if (!CredcardValidator.IsValid(cardNumber, brand))
            {
                throw new ArgumentException("Número de cartão é invalido");
            }

            this.CardNumber = cardNumber;
        }

        /// <summary>
        /// Número do Cartão do Comprador.
        /// </summary>
        public string CardNumber { get; protected set; }
        /// <summary>
        /// Nome do Comprador impresso no cartão.
        /// </summary>
        public string Holder { get; protected set; }
        /// <summary>
        /// Data de validade impresso no cartão
        /// </summary>
        public string ExpirationDate { get; protected set; }
        /// <summary>
        /// Código de segurança impresso no verso do cartão
        /// </summary>
        public string SecurityCode { get; protected set; }
        /// <summary>
        /// Bandeira do cartão
        /// </summary>
        public string Brand { get; protected set; }
    }

    public class Link
    {
        public string Method { get; protected set; }
        public string Rel { get; protected set; }
        public string Href { get; protected set; }
    }

    public class Payment
    {
        public Payment(string holder,
                       string cardNumber,
                       int yearExpiration,
                       int monthExpiration,
                       string securityCode,
                       EnumBrand brand,
                       decimal amount,
                       int installments,
                       string softDescriptor)
        {

            AlterarAmount(amount);
            AlterarInstallments(installments);
            AlterarSoftDescriptor(softDescriptor);
            AlterarCreditCard(cardNumber, holder, yearExpiration, monthExpiration, securityCode, brand);
        }

        private void AlterarCreditCard(string cardNumber, string holder, int yearExpiration, int monthExpiration, string securityCode, EnumBrand brand)
        {
            this.CreditCard = new CreditCard(cardNumber, holder, yearExpiration, monthExpiration, securityCode, brand);
        }

        private void AlterarSoftDescriptor(string softDescriptor)
        {
            if (string.IsNullOrEmpty(softDescriptor))
            {
                throw new ArgumentException("softDescriptor name is required");
            }

            this.SoftDescriptor = Util.RemoveSpecialChars(softDescriptor);
        }

        private void AlterarInstallments(int installments)
        {
            if (installments <= 0)
            {
                throw new ArgumentException("installments name is required");
            }

            this.Installments = installments;
        }

        private void AlterarAmount(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("amount name is required");
            }

            this.Amount = int.Parse((amount * 100).ToString());
        }

        public int ServiceTaxAmount { get; protected set; }
        public int Installments { get; protected set; }
        public string Interest { get; protected set; }
        public bool Capture { get; protected set; }
        public bool Authenticate { get; protected set; }
        public string SoftDescriptor { get; protected set; }
        public CreditCard CreditCard { get; protected set; }
        public string ProofOfSale { get; protected set; }
        public string Tid { get; set; }
        public string AuthorizationCode { get; protected set; }
        public string PaymentId { get; protected set; }
        public string Type { get; protected set; }
        public int Amount { get; protected set; }
        public string Currency { get; protected set; }
        public string Country { get; protected set; }
        public EnumStatus Status { get; protected set; }
        public string ReturnCode { get; protected set; }
        public string ReturnMessage { get; protected set; }
        public List<Link> Links { get; protected set; }
    }

    public class CieloSale
    {
        /// <summary>
        /// Numero de identificação do Pedido.
        /// </summary>
        public string MerchantOrderId { get; protected set; }

        /// <summary>
        /// Comprador
        /// </summary>
        public Customer Customer { get; protected set; }

        /// <summary>
        /// Pagamento
        /// </summary>
        public Payment Payment { get; protected set; }


        public CieloSale()
        {

        }

        public CieloSale(string merchantOrderId,
                         string customerName,
                         string cardNumber,
                         string holder,
                         int yearExpiration,
                         int monthExpiration,
                         string securityCode,
                         EnumBrand brand,
                         decimal amount,
                         int installments,
                         string softDescriptor)
        {

            AlterarMerchantOderId(merchantOrderId);
            AlterarCustomer(customerName);
            AlterarPayment(holder, cardNumber, yearExpiration, monthExpiration, securityCode, brand, amount, installments, softDescriptor);

        }

        private void AlterarPayment(string holder,
                                    string cardNumber,
                                    int yearExpiration,
                                    int monthExpiration,
                                    string securityCode,
                                    EnumBrand brand,
                                    decimal amount,
                                    int installments,
                                    string softDescriptor)
        {


            this.Payment = new Payment(holder,
                                       cardNumber,
                                       yearExpiration,
                                       monthExpiration,
                                       securityCode,
                                       brand,
                                       amount,
                                       installments,
                                       softDescriptor);

        }

        private void AlterarCustomer(string customerName)
        {
            this.Customer = new Customer(customerName);
        }

        private void AlterarMerchantOderId(string merchantOrderId)
        {
            if (string.IsNullOrEmpty(merchantOrderId))
            {
                throw new ArgumentException("MerchantOderId name is required");
            }

            this.MerchantOrderId = merchantOrderId;
        }
    }
}
