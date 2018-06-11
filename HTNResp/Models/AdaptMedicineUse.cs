using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTNResp.Models
{
    public class AdaptMedicineUse
    {
        public int ID { get; set; }
        public string GroupName { get; set; }
        public string GroupNameDetail { get; set; }
        public string Symptom { get; set; }
        public string SymptomType { get; set; }
    }
}