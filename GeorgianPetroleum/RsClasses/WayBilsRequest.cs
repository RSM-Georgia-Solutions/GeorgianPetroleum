using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeorgianPetroleum.RsClasses
{
    public class WayBilsRequest
    {
        public string itypes;
        public string sellerTin;
        public string buyerTin;
        public string statuses;
        public string carNumber;
        public DateTime? beginDateS;
        public DateTime? beginDateE;
        public DateTime? createDateS;
        public DateTime? createDateE;
        public string driverTin;
        public DateTime? delivaryDateS;
        public DateTime? deliveryDateE;
        public decimal? fullAmount;
        public string waybillNumber;
        public DateTime? closeDateS;
        public DateTime? closeDateE;
        public string sUserIds;
        public string comment;

        public WayBilsRequest(DateTime? start, DateTime? end, string iTypes, string statusesr = null)
        {
            beginDateS = start;
            beginDateE = end;
            createDateS = null;
            createDateE = null;
            delivaryDateS = null;
            deliveryDateE = null;
            closeDateS = null;
            closeDateE = null;
            fullAmount = null;
            itypes = iTypes;
            sellerTin = null;
            statuses = statusesr;
            carNumber = null;
            driverTin = null;
            sUserIds = null;
            comment = null;
            waybillNumber = null;
        }
    }
}